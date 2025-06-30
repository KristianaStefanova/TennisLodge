using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TennisLodge.Data.Models
{
    [Comment("Category of tournaments")]
    public class Category
    {
        [Comment("Primary key identifier of the category")]
        public int Id { get; set; }


        [Comment("Name of the category")]
        public string Name { get; set; } = null!;


        [Comment("Collection of tournaments that belong to this category")]
        public virtual ICollection<Tournament> Tournaments { get; set; }
            = new HashSet<Tournament>();
    }
}
