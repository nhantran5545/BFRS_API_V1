using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class AreaRepository : GenericRepository<Area>, IAreaRepository
    {
        public AreaRepository(BFRS_dbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Area>> GetAllAsync()
        {
            return await _context.Areas
                .Include(b => b.Farm)
                .ToListAsync();
        }

        public IEnumerable<Area> GetAreasByFarmId(int farmId)
        {
            return _context.Areas
            .Where(a => a.FarmId == farmId)
            .ToList();
        }

        public override async Task<Area?> GetByIdAsync(object id)
        {
            return await _context.Areas
                .Include(b => b.Farm)
                .Where(b => b.AreaId.Equals(id))
                .FirstOrDefaultAsync();
        }
    }
}
