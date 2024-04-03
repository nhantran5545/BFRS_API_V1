using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class CheckListRepository : GenericRepository<CheckList>, ICheckListRepository
    {
        public CheckListRepository(BFRS_DBContext context) : base(context)
        {
        }

        public async Task<CheckList?> GetCheckListByPhase(int phase)
        {
            return await _context.CheckLists
                .Include(cl => cl.CheckListDetails)
                .Where(cl => cl.Phase == phase)
                .FirstOrDefaultAsync();
        }
    }
}
