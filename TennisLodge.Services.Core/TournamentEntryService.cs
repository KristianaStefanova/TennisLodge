using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.TournamentEntry;

namespace TennisLodge.Services.Core
{
    public class TournamentEntryService : ITournamentEntryService
    {

        private readonly ITournamentEntryRepository entryRepository;

        public TournamentEntryService(ITournamentEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<bool> JoinTournamentAsync(string playerId, Guid tournamentId)
        {
            bool alreadyJoined = await this.entryRepository.GetAllAttached()
                .AnyAsync(e => e.PlayerId == playerId && e.TournamentId == tournamentId);

            if (alreadyJoined)
            {
                return false;
            }

            TournamentEntry entry = new TournamentEntry
            {
                PlayerId = playerId,
                TournamentId = tournamentId,
                RegisteredOn = DateTime.UtcNow
            };

            await this.entryRepository.AddAsync(entry);
            return true;
        }

        public async Task<IEnumerable<MyTournamentEntryViewModel>> GetMyTournamentsAsync(string playerId)
        {
            return await this.entryRepository.GetAllAttached()
                .Include(e => e.Tournament)
                .Where(e => e.PlayerId == playerId && e.TournamentId.HasValue)
                .Select(e => new MyTournamentEntryViewModel
                {
                    EntryId = e.Id,
                    TournamentName = e.Tournament.Name,
                    TournamentLocation = e.Tournament.Location,
                    TournamentStartDate = e.Tournament.StartDate,
                    RegisteredOn = e.RegisteredOn
                })
                .ToListAsync();
        }


        public async Task<bool> CancelEntryAsync(int entryId, string playerId)
        {
            TournamentEntry? entry = await this.entryRepository
                .GetByIdAsync(entryId);

            if (entry == null || entry.PlayerId != playerId)
            {
                return false;
            }

            await this.entryRepository.DeleteAsync(entry);
            return true;
        }

        public async Task<IEnumerable<Guid>> GetMyTournamentIdsAsync(string playerId)
        {
            return await this.entryRepository
                .GetAllAttached()
                .Where(e => e.PlayerId == playerId && e.TournamentId.HasValue)
                .Select(e => e.TournamentId!.Value)
                .Distinct()
                .ToListAsync();
        }
    }
}
