﻿using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class BirdSpeciesRepository : GenericRepository<BirdSpecy>, IBirdSpeciesRepository
    {
        public BirdSpeciesRepository(BFRS_DBContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<BirdSpecy>> GetAllAsync()
        {
            return await _context.BirdSpecies
                .Include(b => b.BirdType)
                .ToListAsync();
        }

        public override async Task<BirdSpecy?> GetByIdAsync(object id)
        {
            return await _context.BirdSpecies
                .Include(b => b.BirdType)
                .Include(b => b.SpeciesMutations)
                .ThenInclude(sm => sm.Mutation)
                .Where(b => b.BirdSpeciesId.Equals(id))
                .FirstOrDefaultAsync();
        }
    }
}
