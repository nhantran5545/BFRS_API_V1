﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Enums
{
    internal enum BirdStatus
    {
        InRestPeriod,
        InReproductionPeriod,
        Paired,
        Unavailable,
        Deleted = -1
    }
}
