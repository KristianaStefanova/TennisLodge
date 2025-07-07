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
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Tournament;
using static TennisLodge.GCommon.ApplicationConstants;

namespace TennisLodge.Services.Core
{
    public class TournamentService : ITournamentService
    {
        private readonly TennisLodgeDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public TournamentService(TennisLodgeDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;

        }

        public async Task<bool> AddTournamentAsync(string userId, TournamentFormInputModel inputModel)
        {
            bool operationResult = false;    

            ApplicationUser? user = await this.userManager.FindByIdAsync(userId);

            Category? categoryRef = await this.dbContext
               .Categories.FindAsync(inputModel.CategoryId);

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

                await this.dbContext.Tournaments.AddAsync(newTournament);
                await this.dbContext.SaveChangesAsync();

                operationResult = true;
            }

            return operationResult;
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
    }
}
