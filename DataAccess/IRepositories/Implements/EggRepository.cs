using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class EggRepository : GenericRepository<Egg>, IEggRepository
    {
        public EggRepository(BFRS_DBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Egg>> GetEggsByClutchIdAsync(object clutchId)
        {
            return await _context.Eggs
                .Include(e => e.EggBirds)
                .ThenInclude(eb => eb.Bird)
                .Where(e => e.ClutchId.Equals(clutchId))
                .ToListAsync();
        }

        public async Task<Egg?> GetEggByBirdIdAsync(object birdId)
        {
            return await _context.Eggs
                .Where(e => e.EggBirds.Contains(birdId))
                .FirstOrDefaultAsync();
        }
    }
}
