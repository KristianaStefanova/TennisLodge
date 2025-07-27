using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Accommodation;

namespace TennisLodge.Services.Core
{
    public class AccommodationRequestService : IAccommodationRequestService
    {
        private readonly IAccommodationRequestRepository accommodationRequestRepository;

        public AccommodationRequestService(Data.Repository.Interfaces.IAccommodationRequestRepository accommodationRequestRepository)
        {
            this.accommodationRequestRepository = accommodationRequestRepository;
        }
        public async Task CreateAccommodationRequestAsync(string guestUserId, AccommodationRequestInputModel inputModel)
        {
            if (!Guid.TryParse(inputModel.TournamentId, out Guid tournamentId))
            {
                throw new ArgumentException("Invalid tournament ID.");
            }


            AccommodationRequest accommodationRequest = new AccommodationRequest
            {
                GuestUserId = guestUserId,
                TournamentId = tournamentId,
                NumberOfGuests = inputModel.NumberOfGuests,
                Notes = inputModel.Notes,
                CreatedOn = DateTime.UtcNow
            };

            await accommodationRequestRepository.AddAsync(accommodationRequest);
            await accommodationRequestRepository.SaveChangesAsync();
        }
    }
}
