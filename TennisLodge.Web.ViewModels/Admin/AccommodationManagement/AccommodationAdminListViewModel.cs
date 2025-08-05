using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Web.ViewModels.Admin.AccommodationManagement
{
    public class AccommodationAdminListViewModel
    {
        public int Id { get; set; }
        public string HostUsername { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string MaxGuests { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public string AvailableFrom { get; set; } = null!;
        public string AvailableTo { get; set; } = null!;
    }
}
