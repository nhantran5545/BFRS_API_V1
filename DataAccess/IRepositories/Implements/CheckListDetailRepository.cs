using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class CheckListDetailRepository : GenericRepository<CheckListDetail>, ICheckListDetailRepository
    {
        public CheckListDetailRepository(BFRS_dbContext context) : base(context)
        {
        }

        public async Task<List<CheckListDetail>> GetCheckListDetailsByCheckListId(int checkListId)
        {
            return await _context.CheckListDetails
                .Where(c => c.CheckListId == checkListId)
                .ToListAsync();
        }
    }
}
