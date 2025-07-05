using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TennisLodge.Data;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels;
using static TennisLodge.GCommon.ValidationConstatnts;

namespace TennisLodge.Services.Core
{
    public class TournamentService : ITournamentService
    {
        private readonly TennisLodgeDbContext dbContext;
        public TournamentService(TennisLodgeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllTournamentsIndexViewModel>> GetAllTournamentsAsync()
        {
            IEnumerable<AllTournamentsIndexViewModel> allTournaments = await this.dbContext
                .Tournaments
                .AsNoTracking()
                .Select(t => new AllTournamentsIndexViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Location = t.Location,
                    Surface = t.Surface,
                    ImageUrl = t.ImageUrl,
                    StartDate = t.StartDate.ToString(DateFormat),
                    EndDate = t.EndDate.ToString(DateFormat),
                    CategoryName = t.Category.Name,
                    Organizer = t.Organizer
                })
                .ToListAsync();

            return allTournaments;
        }
    }
}
