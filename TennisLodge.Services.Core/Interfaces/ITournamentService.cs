using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Web.ViewModels;

namespace TennisLodge.Services.Core.Interfaces
{
    public interface ITournamentService
    {
        Task<IEnumerable<AllTournamentsIndexViewModel>> GetAllTournamentsAsync();
    }
}
