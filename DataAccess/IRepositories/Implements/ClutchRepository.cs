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
        public ClutchRepository(BFRS_DBContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Clutch>> GetAllAsync()
        {
            return await _context.Clutches
                //.Include(c => c.Breeding)
                .Include(c => c.Eggs)
                .Include(c => c.CreatedByNavigation)
                .ToListAsync();
        }

        public override async Task<Clutch?> GetByIdAsync(object id)
        {
            return await _context.Clutches
               //.Include(c => c.Breeding)
               .Include(c => c.Eggs)
               .Include(c => c.CreatedByNavigation)
               .Include(c => c.UpdatedByNavigation)
               .Where(c => c.ClutchId.Equals(id))
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Clutch>> GetClutchsByBreedingId(object breedingId)
        {
            return await _context.Clutches
                //.Include(c => c.Breeding)
                .Include(c => c.Eggs)
                .Include(c => c.CreatedByNavigation)
                .Where(c => c.BreedingId.Equals(breedingId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Clutch>> GetClutchsByCreatedById(object createdById)
        {
            return await _context.Clutches
                //.Include(c => c.Breeding)
                .Include(c => c.Eggs)
                .Include(c => c.CreatedByNavigation)
                .Where(c => c.CreatedBy.Equals(createdById))
                .ToListAsync();
        }
    }
}
