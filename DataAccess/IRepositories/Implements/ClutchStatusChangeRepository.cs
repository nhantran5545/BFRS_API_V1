using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class ClutchStatusChangeRepository : GenericRepository<ClutchStatusChange>, IClutchStatusChangeRepository
    {
        public ClutchStatusChangeRepository(BFRS_DBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ClutchStatusChange>> GetTimelineByClutchIdAsync(object clutchId)
        {
            return await _context.ClutchStatusChanges
                .Include(br => br.ChangedByNavigation)
                .Where(br => br.ClutchId.Equals(clutchId))
                .ToListAsync();
        }
    }
}
