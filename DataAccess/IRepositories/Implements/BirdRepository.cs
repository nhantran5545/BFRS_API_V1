using DataAccess.Models;
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

        public override async Task<IEnumerable<Bird>> GetAllAsync()
        {
            return await _context.Birds
                .Include(b => b.BirdSpecies)
                .Include(b => b.Cage)
                .Include(b => b.Farm)
                .ToListAsync();
        }

        public async Task<IEnumerable<Bird>> GetBirdsByFarmId(object farmId)
        {
            return await _context.Birds
                .Include(b => b.BirdSpecies)
                .Include(b => b.Cage)
                .Include(b => b.Farm)
                .Where(b => b.FarmId.Equals(farmId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Bird>> GetBirdsBySpeciesId(object SpeciesId)
        {
            return await _context.Birds
                .Include(b => b.BirdSpecies)
                .Include(b => b.Cage)
                .Include(b => b.Farm)
                .Where(b => b.BirdSpeciesId.Equals(SpeciesId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Bird>> GetInRestBirdsBySpeciesIdAndFarmId(object SpeciesId, object FarmId)
        {
            return await _context.Birds
                .Where(b => b.BirdSpeciesId.Equals(SpeciesId) && b.FarmId.Equals(FarmId) 
                            && b.Status != null && b.Status.Equals("InRestPeriod"))
                .ToListAsync();
        }

        public override async Task<Bird?> GetByIdAsync(object id)
        {
            return await _context.Birds
                .Include(b => b.BirdSpecies)
                .Include(b => b.Cage)
                .Include(b => b.BirdMutations)
                .Where(b => b.BirdId.Equals(id))
                .FirstOrDefaultAsync();
        }
    }
}
