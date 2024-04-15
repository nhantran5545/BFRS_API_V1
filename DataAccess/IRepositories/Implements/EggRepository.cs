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

        public override async Task<IEnumerable<Egg>> GetAllAsync()
        {
            return await _context.Eggs
                .Include(e => e.EggBirds)
                .ThenInclude(eb => eb.Bird)
                .ToListAsync();
        }

        public override async Task<Egg?> GetByIdAsync(object id)
        {
            return await _context.Eggs
                .Include(e => e.EggBirds)
                .ThenInclude(eb => eb.Bird)
                .FirstOrDefaultAsync(e => e.EggId.Equals(id));
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

        public async Task<IEnumerable<Egg>> GetEggsByStaffId(int staffId)
        {
            return await _context.Eggs
                .Where(e => e.CreatedBy == staffId || e.UpdatedBy == staffId)
                .ToListAsync();
        }

        public async Task<int> GetEggCountByStatusNameAndManagedByStaff(string statusName, int staffId)
        {
            return await _context.Eggs
                .Where(e => e.Status == statusName && (e.CreatedBy == staffId || e.UpdatedBy == staffId))
                .CountAsync();
        }

        public async Task<Egg?> GetEggDetailsAsync(object eggId)
        {
            return await _context.Eggs
                .Include(e => e.Clutch)
                .ThenInclude(c => c.Breeding)
                .ThenInclude(b => b.FatherBird)
                .FirstOrDefaultAsync(e => e.EggId.Equals(eggId));
        }
    }
}
