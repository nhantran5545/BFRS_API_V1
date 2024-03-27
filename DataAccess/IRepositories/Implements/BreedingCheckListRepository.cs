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

        public BreedingCheckList GetBreedingCheckList(int breedingId, int phase)
        {
            return _context.BreedingCheckLists
                .Include(b => b.Breeding)
                .Include(b => b.CheckList)
                .ThenInclude(cl => cl.CheckListDetails)
                .FirstOrDefault(b => b.BreedingId == breedingId && b.Phase == phase);
        }
    }
}
