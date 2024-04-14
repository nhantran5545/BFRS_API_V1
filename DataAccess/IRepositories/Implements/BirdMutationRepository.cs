using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class BirdMutationRepository : GenericRepository<BirdMutation>, IBirdMutationRepository
    {
        public BirdMutationRepository(BFRS_DBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BirdMutation>> GetByBirdId(object birdId)
        {
            return await _context.BirdMutations
                .Include(bm => bm.Mutation)
                .Where(bm => bm.BirdId.Equals(birdId))
                .ToListAsync();
        }
    }
}
