using Microsoft.AspNetCore.Mvc.Rendering;
using TennisLodge.Web.ViewModels.Admin.TournamentManagement;
using TennisLodge.Web.ViewModels.Tournament;

namespace TennisLodge.Services.Core.Interfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<AllTournamentsIndexViewModel>> GetAllTournamentsAsync();

        Task<bool> AddTournamentAsync(string userId, TournamentFormInputModel inputModel);

        Task<TournamentDetailsViewModel?> GetTournamentDetailsByIdAsync(string? id, string? userId = null);

        Task<TournamentFormInputModel?> GetEditableTournamentByIdAsync(string? id);

        Task<bool> EditTournamentAsync(TournamentFormInputModel inputModel);

        Task<IEnumerable<SelectListItem>> GetAllAsSelectList();

    }
}
