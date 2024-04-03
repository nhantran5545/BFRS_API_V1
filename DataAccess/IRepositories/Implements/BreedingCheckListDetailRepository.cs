using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class BreedingCheckListDetailRepository : GenericRepository<BreedingCheckListDetail>, IBreedingCheckListDetailRepository
    {
        public BreedingCheckListDetailRepository(BFRS_DBContext context) : base(context)
        {
        }

        public async Task<BreedingCheckListDetail?> GetBreedingCheckListDetailByBreedingCheckListIdAndCheckListDetailId(object breedingCheckListId, object checkListDetailId)
        {
            return await _context.BreedingCheckListDetails
                .Where(bcd => bcd.BreedingCheckListId.Equals(breedingCheckListId) && bcd.CheckListDetailId.Equals(checkListDetailId))
                .FirstOrDefaultAsync();
        }
    }
}
