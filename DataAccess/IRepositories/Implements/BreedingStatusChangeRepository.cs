using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class BreedingStatusChangeRepository : GenericRepository<BreedingStatusChange>, IBreedingStatusChangeRepository
    {
        public BreedingStatusChangeRepository(BFRS_DBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BreedingStatusChange>> GetTimelineByBreedingIdAsync(object breedingId)
        {
            return await _context.BreedingStatusChanges
                .Include(br => br.ChangedByNavigation)
                .Where(br => br.BreedingId.Equals(breedingId))
                .ToListAsync();
        }
    }
}
