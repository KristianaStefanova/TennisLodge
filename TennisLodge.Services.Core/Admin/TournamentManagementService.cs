using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
