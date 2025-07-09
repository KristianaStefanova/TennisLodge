

using TennisLodge.Web.ViewModels.Favorite;

namespace TennisLodge.Services.Core.Interfaces
{
    public interface IFavoriteService
    {
        Task<IEnumerable<FavoriteTournamentViewModel>> GetAllFavoriteTournamentsAsync(string userId);

        Task<bool> AddTournamentToFavoriteAsync(string? movieId, string? userId);
    }
}
