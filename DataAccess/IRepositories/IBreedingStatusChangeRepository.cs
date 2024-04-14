﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IBreedingStatusChangeRepository : IGenericRepository<BreedingStatusChange>

    {
        Task<IEnumerable<BreedingStatusChange>> GetReasonsByBreedingIdAsync(object breedingId);
    }
}