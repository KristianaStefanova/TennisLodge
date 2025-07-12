using Microsoft.EntityFrameworkCore;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;

namespace TennisLodge.Data.Repository
{
    public class FavoriteRepository : BaseRepository<UserTournament, object>, IFavoriteRepository
    {
        public FavoriteRepository(TennisLodgeDbContext dbContext) 
            : base(dbContext)
        {
        }

        public bool Exists(string userId, string tournamentId)
        {
            return this
               .GetAllAttached()
               .Any(ut => ut.UserId.ToLower() == userId.ToLower() &&
                          ut.TournamentId.ToString().ToLower() == tournamentId.ToLower());
        }

        public Task<bool> ExistsAsync(string userId, string tournamentId)
        {
            return this
               .GetAllAttached()
               .AnyAsync(ut => ut.UserId.ToLower() == userId.ToLower() &&
                               ut.TournamentId.ToString().ToLower() == tournamentId.ToLower());
        }

        public UserTournament? GetByCompositeKey(string userId, string tournamentId)
        {
            return this
                .GetAllAttached()
                .SingleOrDefault(ut => ut.UserId.ToLower() == userId.ToLower() &&
                                       ut.TournamentId.ToString().ToLower() == tournamentId.ToLower());
        }

        public Task<UserTournament?> GetByCompositeKeyAsync(string userId, string tournamentId)
        {
            return this
               .GetAllAttached()
               .SingleOrDefaultAsync(ut => ut.UserId.ToLower() == userId.ToLower() &&
                                           ut.TournamentId.ToString().ToLower() == tournamentId.ToLower());
        }
    }
}
