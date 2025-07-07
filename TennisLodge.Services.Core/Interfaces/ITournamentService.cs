using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Web.ViewModels.Tournament;

namespace TennisLodge.Services.Core.Interfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<AllTournamentsIndexViewModel>> GetAllTournamentsAsync();

        Task<bool> AddTournamentAsync(string userId, TournamentFormInputModel inputModel);

        Task<TournamentDetailsViewModel?> GetTournamentDetailsByIdAsync(string? id);

        Task<TournamentFormInputModel?> GetEditableTournamentByIdAsync(string? id);

        Task<bool> EditTournamentAsync(TournamentFormInputModel inputModel);
    }
}
