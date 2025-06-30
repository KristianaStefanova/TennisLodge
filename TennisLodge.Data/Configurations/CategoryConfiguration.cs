using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisLodge.Data.Models;
using static TennisLodge.GCommon.ValidationConstatnts.Category;

namespace TennisLodge.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity
                .HasKey(c => c.Id);


            entity
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);


            entity.
                HasData(this.GenerateSeedCategories());
        }

        private List<Category> GenerateSeedCategories()
        {
            return new List<Category>
            {
                new Category { Id = 1, Name = "ATP 250" },
                new Category { Id = 2, Name = "Challenger" },
                new Category { Id = 3, Name = "ITF Futures" },
                new Category { Id = 4, Name = "National - Under 10" },
                new Category { Id = 5, Name = "National - Under 12" },
                new Category { Id = 6, Name = "National - Under 14" },
                new Category { Id = 7, Name = "Tennis Europe - Under 12" },
                new Category { Id = 8, Name = "Tennis Europe - Under 14" },
                new Category { Id = 9, Name = "Tennis Europe - Under 16" },
                new Category { Id = 10, Name = "ITF Juniors" }
            };
        }
    }
}

