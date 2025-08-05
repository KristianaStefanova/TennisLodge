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
            if (inputModel != null)
            {
                Tournament? newTournament = new Tournament()
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
    }
}
