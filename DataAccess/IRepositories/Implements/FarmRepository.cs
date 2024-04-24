using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class FarmRepository : GenericRepository<Farm>, IFarmRepository
    {
        public FarmRepository(BFRS_DBContext context) : base(context)
        {
        }
        public async Task<List<object>> GetTotalAccountByFarm()
        {
            var farms = await _context.Farms
                .Include(f => f.Areas)
                    .ThenInclude(a => a.Cages)
                        .ThenInclude(c => c.Account)
                .ToListAsync();

            return farms.Select(farm => new
            {
                FarmName = farm.FarmName,
                TotalAccountOfFarm = farm.Areas
                    .SelectMany(area => area.Cages)
                    .Count(cage => cage.Account != null),
                TotalAccountByRole = farm.Areas
                    .SelectMany(area => area.Cages)
                    .Where(cage => cage.Account != null)
                    .GroupBy(cage => cage.Account.Role)
                    .ToDictionary(group => group.Key, group => group.Count())
            }).ToList<object>();
        }
    }
}
