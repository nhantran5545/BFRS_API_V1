using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class ClutchRepository : GenericRepository<Clutch>, IClutchRepository
    {
        public ClutchRepository(BFRS_dbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Clutch>> GetClutchsByBreedingId(object breedingId)
        {
            return await _context.Clutches
                .Include(c => c.Breeding)
                .Include(c => c.Cage)
                .Include(c => c.Eggs)
                .Where(c => c.BreedingId.Equals(breedingId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Clutch>> GetClutchsByCreatedById(object createdById)
        {
            return await _context.Clutches
                .Include(c => c.Breeding)
                .Include(c => c.Cage)
                .Include(c => c.Eggs)
                .Where(c => c.CreatedBy.Equals(createdById))
                .ToListAsync();
        }
    }
}
