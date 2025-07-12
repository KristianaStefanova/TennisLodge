using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Data.Common
{
    public static class ExceptionMessages
    {
        public const string SoftDeleteOnNonSoftDeletableEntity =
            "Soft delete operation is not supported on non-soft-deletable entities.";
    }
}
