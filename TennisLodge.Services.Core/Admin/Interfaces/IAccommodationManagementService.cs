using TennisLodge.Web.ViewModels.Admin.AccommodationManagement;

namespace TennisLodge.Services.Core.Admin.Interfaces
{
    public interface IAccommodationManagementService
    {
        Task<IEnumerable<AccommodationAdminListViewModel>> GetAllAccommodationsForAdminAsync();

        Task<bool> RestoreAccommodationAsync(string? id);

        Task<bool> ActivateAccommodationAsync(string? id);

        Task<bool> DeactivateAccommodationAsync(string? id);
    }
}
