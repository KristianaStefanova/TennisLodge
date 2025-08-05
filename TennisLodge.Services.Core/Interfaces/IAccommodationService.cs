using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Web.ViewModels.Accommodation;
using TennisLodge.Web.ViewModels.Admin.AccommodationManagement;

namespace TennisLodge.Services.Core.Interfaces
{
    public interface IAccommodationService
    {
        Task<IEnumerable<AccommodationViewModel>> GetAllAccommodationsAsync();

        AccommodationCreateInputModel GetCreateModel();

        Task<bool> AddAccommodationAsync(string userId, AccommodationCreateInputModel model);

        Task<bool> IsAccommodationAddedFromUserAsync(string? accommodationId, string? userId);

        Task<AccommodationCreateInputModel?> GetEditableAccommodationByIdAsync(string? id);

        Task<bool> EditAccommodationAsync(AccommodationCreateInputModel inputModel);

        Task<AccommodationViewModel?> GetAccomodationDeleteDetailsByIdAsync(string? id);

        Task<bool> SoftDeleteAccommodationAsync(string? id);

        // Admin methods
        Task<IEnumerable<AccommodationAdminListViewModel>> GetAllAccommodationsForAdminAsync();
        
        Task<bool> RestoreAccommodationAsync(string? id);
        
        Task<bool> ActivateAccommodationAsync(string? id);
        
        Task<bool> DeactivateAccommodationAsync(string? id);
    }
}
