using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TennisLodge.Data;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Tournament;

namespace TennisLodge.Services.Core
{
    public class CategoryService : ICategoryService
    {
        private readonly TennisLodgeDbContext dbContext;

        public CategoryService(TennisLodgeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            IEnumerable<CategoryViewModel> categoryAsDropDown =  await dbContext
            .Categories
            .AsNoTracking()
            .Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();

            return categoryAsDropDown;
        }
    }
}
