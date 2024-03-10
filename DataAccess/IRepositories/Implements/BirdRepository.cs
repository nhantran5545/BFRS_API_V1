﻿using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class BirdRepository : GenericRepository<Bird>, IBirdRepository
    {
        public BirdRepository(BFRS_dbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Bird>> GetAllBirdsByFarmId(object farmId)
        {
            return await _context.Birds
                .Include(b => b.BirdSpecies)
                .Include(b => b.Farm)
                .Where(b => b.FarmId.Equals(farmId))
                .ToListAsync();
        }
    }
}
