using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class BreedingRepository : GenericRepository<Breeding>, IBreedingRepository
    {
        public BreedingRepository(BFRS_DBContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Breeding>> GetAllAsync()
        {
            return await _context.Breedings
                .Include(br => br.FatherBird)
                .Include(br => br.MotherBird)
                .Include(br => br.Clutches)
                .ToListAsync();
        }

        public override async Task<Breeding?> GetByIdAsync(object id)
        {
            return await _context.Breedings
                .Include(br => br.FatherBird)
                .Include(br => br.MotherBird)
                .Include(br => br.Clutches)
                .Where(br => br.BreedingId.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Breeding>> GetAllBreedingsByManagerId(object managerId)
        {
            return await _context.Breedings
                .Include(br => br.FatherBird)
                .ThenInclude(fb => fb.BirdSpecies)
                .Include(br => br.MotherBird)
                .Include(br => br.Clutches)
                .Where(br => br.CreatedBy.Equals(managerId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Breeding>> GetAllBreedingsByStaff(object accountId)
        {
            return await _context.Breedings
                .Include(br => br.FatherBird)
                .Include(br => br.MotherBird)
                .Include(br => br.Clutches)
                .Include(br => br.Cage)
                .Where(c => c.Cage.AccountId.Equals(accountId))
                .ToListAsync();
        }

        public async Task<int> GetTotalStatusBreedingsByStaff(int accountId ,string statusName)
        {
            return await _context.Breedings
                .Where(c => c.Status == statusName && c.Cage.AccountId.Equals(accountId))
                .CountAsync();
        }

        public async Task<IEnumerable<Breeding>> GetAllBreedingsStatusByManagerId(int managerId, string statusName)
        {
            return await _context.Breedings
                .Where(br => br.Status == statusName && br.CreatedBy.Equals(managerId))
                .ToListAsync();
        }

        public async Task<int> GetTotalEggsAndClutchesCount()
        {
            return await _context.Clutches.SumAsync(c => c.Eggs.Count()) + await _context.Clutches.CountAsync();
        }

        public async Task<int> GetTotalStatusBreedingsByManagerId(int accountId, string statusName)
        {
            return await _context.Breedings
                .Where(br =>br.Status == statusName && br.CreatedBy.Equals(accountId))
                .CountAsync();
        }

        public async Task<List<Breeding>> GetBreedingByAccountIdAsync(int accountId)
        {
            if (accountId <= 0)
            {
                throw new ArgumentException("AccountId must be greater than zero.");
            }

            return await _context.Breedings
                .Include(br => br.FatherBird)
                .Include(br => br.MotherBird)
                .Include(br => br.Clutches)
                .Where(b => b.Cage.AccountId == accountId)
                .ToListAsync();
        }


    }
}
