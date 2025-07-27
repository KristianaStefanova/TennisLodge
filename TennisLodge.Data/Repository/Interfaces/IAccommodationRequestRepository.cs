using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Data.Models;

namespace TennisLodge.Data.Repository.Interfaces
{
    public interface IAccommodationRequestRepository
        : IRepository<AccommodationRequest, int>, IAsyncRepository<AccommodationRequest, int>
    {
    }
}
