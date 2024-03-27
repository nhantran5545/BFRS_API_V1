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

        public override async Task<IEnumerable<BreedingCheckList>> GetAllAsync()
        {
            return await _context.BreedingCheckLists
                .Include(bc => bc.CheckList)
                .Include(bc => bc.BreedingCheckListDetails)
                .ThenInclude(bcd => bcd.CheckListDetail)
                .ToListAsync();
        }

        public async override Task<BreedingCheckList?> GetByIdAsync(object id)
        {
            return await _context.BreedingCheckLists
                .Include(bc => bc.CheckList)
                .Include(bc => bc.BreedingCheckListDetails)
                .ThenInclude(bcd => bcd.CheckListDetail)
                .Where(bc => bc.BreedingCheckListId.Equals(id))
                .FirstOrDefaultAsync();
        }

        public BreedingCheckList GetBreedingCheckList(int breedingId, int phase)
        {
            return _context.BreedingCheckLists
                .Include(b => b.Breeding)
                .Include(b => b.BreedingCheckListDetails)
                .ThenInclude(cl => cl.CheckListDetail)
                .FirstOrDefault(b => b.BreedingId == breedingId && b.Phase == phase);
        }

        public async Task<IEnumerable<BreedingCheckList>> GetBreedingCheckListsByBreedingId(int breedingId)
        {
            return await _context.BreedingCheckLists
                .Include(bc => bc.CheckList)
                .Include(bc => bc.BreedingCheckListDetails)
                .ThenInclude(bcd => bcd.CheckListDetail)
                .Where(bc => bc.BreedingId == breedingId)
                .ToListAsync();
        }
    }
}
