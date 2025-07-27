using Microsoft.EntityFrameworkCore;
using TennisLodge.Data;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Favorite;
using static TennisLodge.GCommon.ApplicationConstants;

namespace TennisLodge.Services.Core
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository, TennisLodgeDbContext dbContext)
        {
            this.favoriteRepository = favoriteRepository;
        }


        public async Task<IEnumerable<FavoriteTournamentViewModel>> GetAllFavoriteTournamentsAsync(string userId)
        {
            IEnumerable<FavoriteTournamentViewModel> favoriteTournaments = await this.favoriteRepository
                .GetAllAttached()
                .Include(f => f.Tournament)
                .ThenInclude(t => t.Category)
                .AsNoTracking()
                .Where(f => f.UserId.ToLower() == userId.ToLower())
                .Select(f => new FavoriteTournamentViewModel()
                {
                    TournamentId = f.Tournament.Id.ToString(),
                    TournamentName = f.Tournament.Name,
                    Location = f.Tournament.Location,
                    CategoryName = f.Tournament.Category.Name,
                    ImageUrl = f.Tournament.ImageUrl ?? $"/images/{NoImageUrl}",
                    StartDate = f.Tournament.StartDate.ToString(AppDateFormat),
                    EndDate = f.Tournament.EndDate.ToString(AppDateFormat),
                })
                .ToArrayAsync();

            return favoriteTournaments;
        }


        public async Task<bool> AddTournamentToFavoriteAsync(string? movieId, string? userId)
        {
            bool result = false;

            if (movieId != null && userId != null)
            {
                bool idTournamentIdValid = Guid.TryParse(movieId, out Guid tournamentGuid);

                if (idTournamentIdValid)
                {
                    UserTournament? userTournamentEntry = await this.favoriteRepository
                        .GetAllAttached()
                        .IgnoreQueryFilters()
                        .SingleOrDefaultAsync(ut => ut.TournamentId.ToString() == tournamentGuid.ToString() &&
                                              ut.UserId.ToLower() == userId);

                    if (userTournamentEntry != null)
                    {
                        userTournamentEntry.IsDeleted = false;
                        result = await this.favoriteRepository.UpdateAsync(userTournamentEntry);
                    }
                    else
                    {
                        userTournamentEntry = new UserTournament()
                        {
                            TournamentId = tournamentGuid,
                            UserId = userId
                        };

                        await this.favoriteRepository.AddAsync(userTournamentEntry);
                        result = true;
                    }
                }
            }

            return result;
        }

        public async Task<bool> RemoveTournamentFromFavoriteAsync(string? tournamentId, string? userId)
        {
            bool result = false;

            if (tournamentId != null && userId != null)
            {
                bool idTournamentIdValid = Guid.TryParse(tournamentId, out Guid tournamentGuid);

                if (idTournamentIdValid)
                {
                    UserTournament? userTournamentEntry = await this.favoriteRepository
                        .SingleOrDefaultAsync(ut => ut.TournamentId.ToString() == tournamentGuid.ToString() &&
                                              ut.UserId.ToLower() == userId);

                    if (userTournamentEntry != null)
                    {
                        result = await this.favoriteRepository
                            .DeleteAsync(userTournamentEntry);
                    }
                }
            }

            return result;
        }

        public async Task<bool> IsTournamentInFavoritesAsync(Guid? tournamentId, string? userId)
        {
            if (tournamentId == null || string.IsNullOrEmpty(userId))
            {
                return false;
            }

            UserTournament? userTournamentEntry = await this.favoriteRepository
                .SingleOrDefaultAsync(ut => ut.TournamentId == tournamentId && ut.UserId.ToLower() == userId.ToLower());

            return userTournamentEntry != null;
        }
    }
}
