using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Data.Models
{
    [Comment("Admin user in the system")]
    public class Admin
    {
        [Comment("Admin identifier")]
        public string Id { get; set; } = null!;

        public bool IsDeleted { get; set; }

        [Comment("Admin's user entity")]
        public string UserId { get; set; } = null!;

        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<Tournament> ManagedTournaments { get; set; } 
            = new HashSet<Tournament>();
    }
}
