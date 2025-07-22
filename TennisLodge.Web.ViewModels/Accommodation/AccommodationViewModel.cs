using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Web.ViewModels.Accommodation
{
    public class AccommodationViewModel
    {
        public int Id { get; set; }

        public string City { get; set; } = null!;

        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableTo { get; set; }

        public string HostFullName { get; set; } = null!;

        public string HostUserId { get; set; } = null!;

        public bool IsOwner { get; set; }

        public string Address { get; set; } = null!;
    }
}
