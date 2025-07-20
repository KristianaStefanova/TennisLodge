using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;

namespace TennisLodge.Data.Repository
{
    public class AdminRepository
        : BaseRepository<Admin, string>, IAdminRepository
    {
        public AdminRepository(TennisLodgeDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
