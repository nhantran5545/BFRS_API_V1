using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Enums
{
    internal enum BreedingStatus
    {
        Opened,
        Mating,
        InProgress,
        Closed,
        Failed,
        Cancelled,
        Deleted = -1
    }
}
