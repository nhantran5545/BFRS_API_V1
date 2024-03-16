using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class BreedingRepository : GenericRepository<Breeding>, IBreedingRepository
    {
        public BreedingRepository(BFRS_dbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Breeding>> GetAllBreedingsByManagerId(object managerId)
        {
            return await _context.Breedings
                .Where(br => br.CreatedBy.Equals(managerId))
                .ToListAsync();
        }
    }
}
