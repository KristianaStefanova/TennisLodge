

using TennisLodge.Web.ViewModels.Favorite;

namespace TennisLodge.Services.Core.Interfaces
{
    public interface IFavoriteService
    {
        Task<IEnumerable<FavoriteTournamentViewModel>> GetAllFavoriteTournamentsAsync(string userId);

        Task<bool> AddTournamentToFavoriteAsync(string? tournamentId, string? userId);

        Task<bool> RemoveTournamentFromFavoriteAsync(string? tournamentId, string? userId);

        Task<bool> IsTournamentInFavoritesAsync(Guid? tournamentId, string? userId);
    }
}
