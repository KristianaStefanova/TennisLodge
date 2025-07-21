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
        Task<IEnumerable<AccommodationViewModel>> GetAvailableAccommodationsAsync();

        AccommodationCreateInputModel GetCreateModel();
      
    }
}
