using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class EggStatusChangeRepository : GenericRepository<EggStatusChange>, IEggStatusChangeRepository
    {
        public EggStatusChangeRepository(BFRS_DBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<EggStatusChange>> GetTimelineByEggIdAsync(object eggId)
        {
            return await _context.EggStatusChanges
                .Include(br => br.ChangedBy)
                .Where(br => br.EggId.Equals(eggId))
                .ToListAsync();
        }
    }
}
