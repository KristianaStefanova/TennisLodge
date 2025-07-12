using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Data.Models;

namespace TennisLodge.Data.Repository.Interfaces
{
    public interface IFavoriteRepository 
        : IRepository<UserTournament,object>, IAsyncRepository<UserTournament, object>
    {
        UserTournament? GetByCompositeKey(string userId, string tournamentId);

        Task<UserTournament?> GetByCompositeKeyAsync(string userId, string tournamentId);

        bool Exists(string userId, string tournamentId);

        Task<bool> ExistsAsync(string userId, string tournamentId);
    }
}
