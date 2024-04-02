using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class CageRepository : GenericRepository<Cage>, ICageRepository
    {
        public CageRepository(BFRS_dbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cage>> GetAllCagesAsync()
        {
            return await _context.Cages
                .Include(c => c.Area)
                .Include(c => c.Account)
                .Include(c => c.Birds)
                .ToListAsync();
        }

        public override async Task<Cage?> GetByIdAsync(object cageId)
        {
            return await _context.Cages
                .Include(br => br.Area)
                .Include(c => c.Account)
                .Include(c => c.Birds)
                .Where(br => br.CageId.Equals(cageId))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Cage>> GetStandbyCagesByFarmId(int farmId)
        {
            return await _context.Cages
                .Include(c => c.Area)
                .Include(c => c.Account)
                .Where(c => (c.Status == null || c.Status.Equals("Standby"))
                            && c.Area != null && c.Area.FarmId.Equals(farmId))
                .ToListAsync();
        }


    }
}
