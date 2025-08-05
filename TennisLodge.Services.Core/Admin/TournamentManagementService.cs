using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core.Admin.Interfaces;
using TennisLodge.Web.ViewModels.Admin.TournamentManagement;
using static TennisLodge.GCommon.ApplicationConstants;

namespace TennisLodge.Services.Core.Admin
{
    public class TournamentManagementService : ITournamentManagementService
    {
        private readonly ITournamentRepository tournamentRepository;

        public TournamentManagementService(ITournamentRepository tournamentRepository)
        {
            this.tournamentRepository = tournamentRepository;
        }

        public async Task AddTournamentAsync(TournamentManagementAddFormModel? inputModel, string userId)
        {
            if (inputModel != null && !string.IsNullOrWhiteSpace(userId))
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
                       DateTimeStyles.None),
                    PublisherId = userId
                };

                await this.tournamentRepository.AddAsync(newTournament);
            }
        }

        public async Task<IEnumerable<TournamentManagementIndexViewModel>> GetTournamentManagementBoardDataAsync()
        {
            IEnumerable<TournamentManagementIndexViewModel> allTournaments = await tournamentRepository
                .GetAllAttached()
                .IgnoreQueryFilters()
                .Select(t => new TournamentManagementIndexViewModel()
                {
                    Id = t.Id.ToString(),
                    Name = t.Name,
                    Location = t.Location,
                    Surface = t.Surface,
                    StartDate = t.StartDate.ToString(AppDateFormat),
                    EndDate = t.EndDate.ToString(AppDateFormat),
                    CategoryName = t.Category.Name,
                    Organizer = t.Organizer,
                    IsDeleted = t.IsDeleted,
                    AdminUsername = t.Publisher.UserName ?? "",
                    RegisteredUsersCount = t.UserTournaments.Count().ToString(),
                    AccommodationRequestsCount = t.AccommodationRequests.Count().ToString()
                })
                .ToArrayAsync();

            return allTournaments;
        }

        public async Task<TournamentManagementEditFormModel?> GetTournamentEditFormModelAsync(string? id)
        {
            TournamentManagementEditFormModel? formModel = null;

            if (!String.IsNullOrEmpty(id))
            {
                Tournament? tournamentToEdit = await this.tournamentRepository
                    .GetAllAttached()
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(t => t.Id.ToString().ToLower() == id.ToLower());

                if (tournamentToEdit != null)
                {
                    formModel = new TournamentManagementEditFormModel()
                    {
                        Id = tournamentToEdit.Id.ToString(),
                        Name = tournamentToEdit.Name,
                        Description = tournamentToEdit.Description,
                        Location = tournamentToEdit.Location,
                        Surface = tournamentToEdit.Surface,
                        CategoryId = tournamentToEdit.CategoryId,
                        Organizer = tournamentToEdit.Organizer,
                        ImageUrl = tournamentToEdit.ImageUrl,
                        StartDate = tournamentToEdit.StartDate.ToString(AppDateFormat),
                        EndDate = tournamentToEdit.EndDate.ToString(AppDateFormat)
                    };
                }

            }
            return formModel;
        }

        public async Task<bool> EditTournamentAsync(TournamentManagementEditFormModel? inputModel, string userId)
        {
            bool result = false;    
            if (inputModel != null && !string.IsNullOrWhiteSpace(userId))
            {
                Tournament? tournamentToEdit = await this.tournamentRepository
                    .SingleOrDefaultAsync(t => t.Id.ToString().ToLower() == inputModel.Id.ToLower());

                if (tournamentToEdit != null)
                {
                    tournamentToEdit.Name = inputModel.Name;
                    tournamentToEdit.Description = inputModel.Description;
                    tournamentToEdit.Location = inputModel.Location;
                    tournamentToEdit.Surface = inputModel.Surface;
                    tournamentToEdit.CategoryId = inputModel.CategoryId;
                    tournamentToEdit.Organizer = inputModel.Organizer;
                    tournamentToEdit.ImageUrl = inputModel.ImageUrl;
                    tournamentToEdit.StartDate = DateOnly.ParseExact(inputModel.StartDate, AppDateFormat, CultureInfo.InvariantCulture,
                        DateTimeStyles.None);
                    tournamentToEdit.EndDate = DateOnly.ParseExact(inputModel.EndDate, AppDateFormat, CultureInfo.InvariantCulture,
                        DateTimeStyles.None);

                    result = await tournamentRepository.UpdateAsync(tournamentToEdit);
                }
            }
            return result;
        }

        public async Task<Tuple<bool, bool>> DeleteOrRestoreTournamentAsync(string? id)
        {
            bool result = false;    
            bool isRestored = false;    
            if (!String.IsNullOrWhiteSpace(id))
            {
                Tournament? tournamentToDeleteOrRestore = await this.tournamentRepository
                    .GetAllAttached()
                    .IgnoreQueryFilters()
                    .SingleOrDefaultAsync(t => t.Id.ToString().ToLower() == id.ToLower());
                if (tournamentToDeleteOrRestore != null)
                {
                    if(tournamentToDeleteOrRestore.IsDeleted)
                    {
                        isRestored = true;
                    }
                   
                    tournamentToDeleteOrRestore.IsDeleted = !tournamentToDeleteOrRestore.IsDeleted;
                    result = await this.tournamentRepository
                        .UpdateAsync(tournamentToDeleteOrRestore);
                }
            }
            return new Tuple<bool, bool>(result, isRestored);
        }
    }
}
