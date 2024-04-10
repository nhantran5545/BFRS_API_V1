using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class BreedingReasonRepository : GenericRepository<BreedingReason>, IBreedingReasonRepository
    {
        public BreedingReasonRepository(BFRS_DBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BreedingReason>> GetReasonsByBreedingIdAsync(object breedingId)
        {
            return await _context.BreedingReasons
                .Include(br => br.CreatedBy)
                .Where(br => br.BreedingId.Equals(breedingId))
                .ToListAsync();
        }
    }
}
