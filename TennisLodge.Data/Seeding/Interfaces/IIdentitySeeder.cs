﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Data.Seeding.Interfaces
{
    public interface IIdentitySeeder
    {
        Task SeedIdentityAsync();
    }
}
