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
        public BreedingCheckListRepository(BFRS_DBContext context) : base(context)
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

        public async Task<IEnumerable<BreedingCheckList>> GetBreedingCheckListsByBreedingIdAndPhase(int breedingId, int phase)
        {
            return await _context.BreedingCheckLists
                .Include(bc => bc.CheckList)
                .Include(bc => bc.BreedingCheckListDetails)
                .ThenInclude(bcd => bcd.CheckListDetail)
                .Where(bc => bc.BreedingId == breedingId && bc.Phase == phase)
                .ToListAsync();
        }

        public async Task<IEnumerable<BreedingCheckList>> GetBreedingCheckListsByClutchIdAndPhase(int clutchId, int phase)
        {
            return await _context.BreedingCheckLists
                .Include(bc => bc.CheckList)
                .Include(bc => bc.BreedingCheckListDetails)
                .ThenInclude(bcd => bcd.CheckListDetail)
                .Where(bc => bc.ClutchId == clutchId && bc.Phase == phase)
                .ToListAsync();
        }

        public async Task<BreedingCheckList?> GetTodayCheckListByBreedingId(object breedingId)
        {
            return await _context.BreedingCheckLists
                .Include(bc => bc.CheckList)
                .Include(bc => bc.BreedingCheckListDetails)
                .ThenInclude(bcd => bcd.CheckListDetail)
                .Where(bc => bc.BreedingId.Equals(breedingId) && bc.CreateDate != null
                                && bc.CreateDate == DateTime.Today)
                .FirstOrDefaultAsync();
        }

        public async Task<BreedingCheckList?> GetTodayCheckListByBreedingIdAndPhase(object breedingId, int phase)
        {
            return await _context.BreedingCheckLists
                .Include(bc => bc.CheckList)
                .Include(bc => bc.BreedingCheckListDetails)
                .ThenInclude(bcd => bcd.CheckListDetail)
                .Where(bc => bc.BreedingId.Equals(breedingId) && bc.CreateDate != null
                                && bc.CreateDate == DateTime.Today && bc.Phase == phase)
                .FirstOrDefaultAsync();
        }
    }
}
