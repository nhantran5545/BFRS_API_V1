using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class IssueRepository : GenericRepository<Issue>, IIssueRepository
    {
        public IssueRepository(BFRS_DBContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Issue>> GetAllAsync()
        {
            return await _context.Issues
                //.Include(c => c.Breeding)
                .Include(c => c.IssueType)
                .Include(c => c.CreatedByNavigation)
                .Include(c => c.UpdatedByNavigation)
                .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetIssueByStaffId(int staffId)
        {
            return await _context.Issues
                .Include(e => e.IssueType)
                .Where(e => e.CreatedBy == staffId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetIssuesByBreedingAsync(int breedingId)
        {
            return await _context.Issues
                .Where(issue => issue.BreedingId == breedingId)
                .Include(c => c.IssueType)
                .Include(c => c.CreatedByNavigation)
                .Include(c => c.UpdatedByNavigation)
                .ToListAsync();
        }

        public override async Task<Issue?> GetByIdAsync(object id)
        {
            return await _context.Issues
               //.Include(c => c.Breeding)
               .Include(c => c.IssueType)
               .Include(c => c.CreatedByNavigation)
               .Include(c => c.UpdatedByNavigation)
               .Where(c => c.IssueId.Equals(id))
               .FirstOrDefaultAsync();
        }
    }
}
