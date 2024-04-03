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
        public CheckListDetailRepository(BFRS_DBContext context) : base(context)
        {
        }

        public async Task<List<CheckListDetail>> GetCheckListDetailByCheckListId(int CheckListId)
        {
            return await _context.CheckListDetails
                .Where(c => c.CheckListId == CheckListId)
                .ToListAsync();
        }


    }
}
