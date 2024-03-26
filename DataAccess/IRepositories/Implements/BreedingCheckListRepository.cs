using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class BreedingCheckListRepository : GenericRepository<BreedingCheckList> , IBreedingCheckListRepository
    {
        public BreedingCheckListRepository(BFRS_dbContext context) : base(context)
        {
        }
        public async Task<List<BreedingCheckList>> GetBreedingCheckListDetailsByBreedingId(int breedingId)
        {
            return await _context.BreedingCheckLists
                .Include(bcl => bcl.CheckList)
                .Include(bcl => bcl.Breeding)
                .Include(bcl => bcl.BreedingCheckListDetails)
                    .ThenInclude(bclDetail => bclDetail.CheckListDetail)
                .Where(bcl => bcl.BreedingId == breedingId)
                .ToListAsync();
        }

    }
}
