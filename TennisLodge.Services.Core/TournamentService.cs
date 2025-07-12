using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisLodge.Data;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Tournament;
using static TennisLodge.GCommon.ApplicationConstants;

namespace TennisLodge.Services.Core
{
    public class TournamentService : ITournamentService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ITournamentRepository tournamentRepository;    
        private readonly UserManager<ApplicationUser> userManager;
        

        public TournamentService(ICategoryRepository categoryRepository, 
            ITournamentRepository tournamentRepository, UserManager<ApplicationUser> userManager)
        {
            this.categoryRepository = categoryRepository;
            this.tournamentRepository = tournamentRepository;
            this.userManager = userManager;
        }



        public async Task<bool> AddTournamentAsync(string userId, TournamentFormInputModel inputModel)
        {
            bool operationResult = false;

            ApplicationUser? user = await this.userManager
                .FindByIdAsync(userId);

            Category? categoryRef = await this.categoryRepository
                .GetByIdAsync(inputModel.CategoryId);


            if (user != null && categoryRef != null)
            {
                Tournament newTournament = new Tournament()
                {
                    Name = inputModel.Name,
                    Description = inputModel.Description,
                    Location = inputModel.Location,
                    Surface = inputModel.Surface,
                    CategoryId = inputModel.CategoryId,
                    Organizer = inputModel.Organizer,
                    ImageUrl = inputModel.ImageUrl,
                    StartDate = DateOnly.ParseExact(inputModel.StartDate, AppDateFormat, CultureInfo.InvariantCulture,
                       DateTimeStyles.None),
                    EndDate = DateOnly.ParseExact(inputModel.EndDate, AppDateFormat, CultureInfo.InvariantCulture,
                       DateTimeStyles.None)
                };

                await this.tournamentRepository.AddAsync(newTournament);
                await this.tournamentRepository.SaveChangesAsync();

                operationResult = true;
            }

            return operationResult;
        }

        public async Task<IEnumerable<AllTournamentsIndexViewModel>> GetAllTournamentsAsync()
        {
            IEnumerable<AllTournamentsIndexViewModel> allTournaments = await this.tournamentRepository
                .GetAllAttached()
                .AsNoTracking()
                .Select(t => new AllTournamentsIndexViewModel()
                {
                    Id = t.Id.ToString(),
                    Name = t.Name,
                    Location = t.Location,
                    Surface = t.Surface,
                    ImageUrl = t.ImageUrl,
                    StartDate = t.StartDate.ToString(AppDateFormat),
                    EndDate = t.EndDate.ToString(AppDateFormat),
                    CategoryName = t.Category.Name,
                    Organizer = t.Organizer
                })
                .ToListAsync();

            foreach (var tournament in allTournaments)
            {
                if (String.IsNullOrEmpty(tournament.ImageUrl))
                {
                    tournament.ImageUrl = $"/images/{NoImageUrl}.jpg";
                }
            }

            return allTournaments;
        }


        public async Task<TournamentDetailsViewModel?> GetTournamentDetailsByIdAsync(string? tournamentId)
        {
            TournamentDetailsViewModel? tournamentDetails = null;

            bool isIdValidGuid = Guid.TryParse(tournamentId, out Guid tournamentGuid);

            if (isIdValidGuid)
            {
                tournamentDetails = await this.tournamentRepository
                    .GetAllAttached()
                    .AsNoTracking()
                    .Where(t => t.Id == tournamentGuid)
                    .Select(t => new TournamentDetailsViewModel()
                    {
                        Id = t.Id.ToString(),
                        Name = t.Name,
                        ImageUrl = t.ImageUrl ?? $"/images/{NoImageUrl}.jpg",
                        Description = t.Description,
                        Location = t.Location,
                        Surface = t.Surface,
                        StartDate = t.StartDate,
                        EndDate = t.EndDate,
                        CategoryName = t.Category.Name,
                        Organizer = t.Organizer
                    })
                    .SingleOrDefaultAsync();
            }

            return tournamentDetails;
        }


        public async Task<TournamentFormInputModel?> GetEditableTournamentByIdAsync(string? id)
        {
            TournamentFormInputModel? tournamentDetails = null;

            bool isIdValidGuid = Guid.TryParse(id, out Guid tournamentGuid);

            if (isIdValidGuid)
            {
                tournamentDetails = await this.tournamentRepository
                    .GetAllAttached()
                    .AsNoTracking()
                    .Where(t => t.Id == tournamentGuid)
                    .Select(t => new TournamentFormInputModel()
                    {
                        Id = t.Id.ToString(),
                        Name = t.Name,
                        ImageUrl = t.ImageUrl ?? $"/images/{NoImageUrl}.jpg",
                        Description = t.Description,
                        Location = t.Location,
                        Surface = t.Surface,
                        StartDate = t.StartDate.ToString(AppDateFormat),
                        EndDate = t.EndDate.ToString(AppDateFormat),
                        CategoryId = t.Category.Id,
                        Organizer = t.Organizer
                    })
                    .SingleOrDefaultAsync();
            }

            return tournamentDetails;
        }

        public async Task<bool> EditTournamentAsync(TournamentFormInputModel inputModel)
        {
            bool result = false;

            Tournament? tournamentToEdit = await this.FindTournamentByStringId(inputModel.Id);

            if (tournamentToEdit == null)
            {
                return result;
            }

            DateOnly startDate = DateOnly.ParseExact(inputModel.StartDate, AppDateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None);

            DateOnly endDate = DateOnly.ParseExact(inputModel.EndDate, AppDateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None);

            tournamentToEdit.Name = inputModel.Name;
            tournamentToEdit.Description = inputModel.Description;
            tournamentToEdit.Location = inputModel.Location;
            tournamentToEdit.Surface = inputModel.Surface;
            tournamentToEdit.ImageUrl = inputModel.ImageUrl ?? $"/images/{NoImageUrl}.jpg";
            tournamentToEdit.CategoryId = inputModel.CategoryId;
            tournamentToEdit.Organizer = inputModel.Organizer;
            tournamentToEdit.StartDate = startDate;
            tournamentToEdit.EndDate = endDate;

            result = await this.tournamentRepository.UpdateAsync(tournamentToEdit);

            return result;
        }

        public async Task<bool> SoftDeleteTournamentAsync(string? id)
        {
            try
            {
                Tournament? tournamentToDelete = await this.FindTournamentByStringId(id);

                if (tournamentToDelete == null)
                {
                    return false;
                }

                tournamentToDelete.IsDeleted = true;
                

                await this.tournamentRepository.UpdateAsync(tournamentToDelete);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }



        private async Task<Tournament?> FindTournamentByStringId(string? id)
        {
            Tournament? tournament = null;

            if (!string.IsNullOrWhiteSpace(id))
            {
                bool isGuidValid = Guid.TryParse(id, out Guid tournamentGuid);
                if (isGuidValid)
                {
                    tournament = await this.tournamentRepository
                        .GetByIdAsync(tournamentGuid);
                }
            }

            return tournament;
        }
    }
}
