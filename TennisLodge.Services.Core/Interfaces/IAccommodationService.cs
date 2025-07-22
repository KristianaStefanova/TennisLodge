using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Web.ViewModels.Accommodation;

namespace TennisLodge.Services.Core.Interfaces
{
    public interface IAccommodationService
    {
        Task<IEnumerable<AccommodationViewModel>> GetAllAccommodationsAsync();

        AccommodationCreateInputModel GetCreateModel();

        Task<bool> AddAccommodationAsync(string userId, AccommodationCreateInputModel model);

        Task<bool> IsAccommodationAddedFromUserAsync(string? accommodationId, string? userId);

        Task<AccommodationCreateInputModel?> GetEditableAccommodationByIdAsync(string? id);

        Task<bool> EditAccomodationAsync(AccommodationCreateInputModel inputModel);



    }
}
