using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class SpeciesMutationRepository : GenericRepository<SpeciesMutation>, ISpeciesMutationRepository
    {
        public SpeciesMutationRepository(BFRS_DBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SpeciesMutation>> GetBySpeciesIdAsync(object speciesId)
        {
            return await _context.SpeciesMutations
                .Include(sm => sm.Mutation)
                .Where(sm => sm.BirdSpeciesId.Equals(speciesId))
                .ToListAsync();
        }
    }
}
