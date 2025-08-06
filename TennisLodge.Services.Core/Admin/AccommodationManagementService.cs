using Microsoft.EntityFrameworkCore;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core.Admin.Interfaces;
using TennisLodge.Web.ViewModels.Admin.AccommodationManagement;
using static TennisLodge.GCommon.ApplicationConstants;

namespace TennisLodge.Services.Core.Admin
{
    public class AccommodationManagementService : IAccommodationManagementService
    {
        private readonly IAccommodationRepository accommodationRepository;
        
        public AccommodationManagementService(IAccommodationRepository accommodationRepository)
        {
            this.accommodationRepository = accommodationRepository;
        }
    
        public async Task<IEnumerable<AccommodationAdminListViewModel>> GetAllAccommodationsForAdminAsync()
        {
            IEnumerable<AccommodationAdminListViewModel> accommodations = await this.accommodationRepository
                .GetAllAttached()
                .AsNoTracking()
                .Include(a => a.HostUser)
                .Select(a => new AccommodationAdminListViewModel
                {
                    Id = a.Id,
                    HostUsername = a.HostUser.UserName,
                    City = a.City,
                    Address = a.Address,
                    MaxGuests = a.MaxGuests.ToString(),
                    IsAvailable = a.IsAvailable,
                    IsDeleted = a.IsDeleted,
                    AvailableFrom = a.AvailableFrom.HasValue ? a.AvailableFrom.Value.ToString(AppDateFormat) : "N/A",
                    AvailableTo = a.AvailableTo.HasValue ? a.AvailableTo.Value.ToString(AppDateFormat) : "N/A"
                })
                .ToListAsync();

            return accommodations;
        }

        public async Task<bool> RestoreAccommodationAsync(string? id)
        {
            bool result = false;
            Accommodation? accommodationToRestore = await this.FindAccommodationByStringId(id);

            if (accommodationToRestore != null)
            {
                accommodationToRestore.IsDeleted = false;
                accommodationToRestore.DeletedOn = null;
                result = await this.accommodationRepository.UpdateAsync(accommodationToRestore);
            }

            return result;
        }

        public async Task<bool> ActivateAccommodationAsync(string? id)
        {
            bool result = false;
            Accommodation? accommodationToActivate = await this.FindAccommodationByStringId(id);

            if (accommodationToActivate != null)
            {
                accommodationToActivate.IsAvailable = true;
                result = await this.accommodationRepository.UpdateAsync(accommodationToActivate);
            }

            return result;
        }

        public async Task<bool> DeactivateAccommodationAsync(string? id)
        {
            bool result = false;
            Accommodation? accommodationToDeactivate = await this.FindAccommodationByStringId(id);

            if (accommodationToDeactivate != null)
            {
                accommodationToDeactivate.IsAvailable = false;
                result = await this.accommodationRepository.UpdateAsync(accommodationToDeactivate);
            }

            return result;
        }

        private async Task<Accommodation?> FindAccommodationByStringId(string? id)
        {
            Accommodation? accommodation = null;

            if (!string.IsNullOrWhiteSpace(id))
            {
                bool isIntValid = int.TryParse(id, out int accommodationInt);
                if (isIntValid)
                {
                    accommodation = await this.accommodationRepository
                        .GetByIdAsync(accommodationInt);
                }
            }

            return accommodation;
        }
    }
}
