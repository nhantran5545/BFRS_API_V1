﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IBreedingReasonRepository : IGenericRepository<BreedingReason>
    {
        Task<IEnumerable<BreedingReason>> GetReasonsByBreedingIdAsync(object breedingId);
    }
}
