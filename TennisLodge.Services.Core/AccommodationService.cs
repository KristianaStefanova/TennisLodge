using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Data;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Accommodation;

namespace TennisLodge.Services.Core
{
    public class AccommodationService : IAccommodationService
    {
        private readonly TennisLodgeDbContext dbContext;

        public AccommodationService(TennisLodgeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AccommodationViewModel>> GetAvailableAccommodationsAsync()
        {
            IEnumerable<AccommodationViewModel> accommodations =  await this.dbContext
                .Accommodations
                .Where(a => a.IsAvailable)
                .Select(a => new AccommodationViewModel
                {
                    Id = a.Id,
                    City = a.City,
                    AvailableFrom = a.AvailableFrom,
                    AvailableTo = a.AvailableTo,
                    HostFullName = a.HostUser.FirstName + " " + a.HostUser.LastName
                })
                .ToListAsync();

            return accommodations;
        }

        public AccommodationCreateInputModel GetCreateModel()
        {
            return new AccommodationCreateInputModel();
           
        }
    }
}
