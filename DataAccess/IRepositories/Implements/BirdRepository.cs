using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class BirdRepository : GenericRepository<Bird>, IBirdRepository
    {
        public BirdRepository(BFRS_DBContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Bird>> GetAllAsync()
        {
            return await _context.Birds
                .Include(b => b.BirdSpecies)
                .Include(b => b.Cage)
                .Include(b => b.Farm)
                .ToListAsync();
        }

        public async Task<IEnumerable<Bird>> GetBirdsByFarmId(object farmId)
        {
            return await _context.Birds
                .Include(b => b.BirdSpecies)
                .Include(b => b.Cage)
                .Include(b => b.Farm)
                .Where(b => b.FarmId.Equals(farmId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Bird>> GetBirdsBySpeciesId(object SpeciesId)
        {
            return await _context.Birds
                .Include(b => b.BirdSpecies)
                .Include(b => b.Cage)
                .Include(b => b.Farm)
                .Where(b => b.BirdSpeciesId.Equals(SpeciesId))
                .ToListAsync();
        }

        public async Task<List<Bird>> GetBirdsByCageIdAsync(int cageId)
        {
            return await _context.Birds
                .Where(b => b.CageId == cageId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Bird>> GetInRestBirdsBySpeciesIdAndFarmId(object SpeciesId, object FarmId)
        {
            return await _context.Birds
                .Where(b => b.BirdSpeciesId.Equals(SpeciesId) && b.FarmId.Equals(FarmId) 
                            && b.Status != null && b.Status.Equals("InRestPeriod"))
                .ToListAsync();
        }

        public async Task<IEnumerable<Bird>> GetBirdsByStaffId(object staffId)
        {
            return await _context.Birds
                .Include(b => b.Cage)
                .Include(b => b.BirdSpecies)
                .Where(b => b.Cage.AccountId.Equals(staffId))
                .ToListAsync();
        }

        public async Task<int> GetTotalEggsAndClutchesCount()
        {
            return await _context.Clutches.SumAsync(c => c.Eggs.Count()) + await _context.Clutches.CountAsync();
        }


        public override async Task<Bird?> GetByIdAsync(object id)
        {
            return await _context.Birds
                .Include(b => b.BirdSpecies)
                .Include(b => b.EggBirds)
                .Include(b => b.Cage)
                .Include(b => b.BirdMutations)
                .ThenInclude(bm => bm.Mutation)
                .Where(b => b.BirdId.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<Dictionary<string, int>> GetBirdCountBySpeciesAndFarm(int farmId)
        {
            var result = await _context.Birds
                .Where(b => b.FarmId == farmId)
                .GroupBy(b => b.BirdSpecies.BirdSpeciesName)
                .Select(g => new { Species = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Species, x => x.Count);

            return result;
        }

        public async Task<Dictionary<string, int>> GetTotalBirdsByGenderAndFarmId(int farmId)
        {
            var birds = await _context.Birds
                .Where(b => b.FarmId == farmId) 
                .ToListAsync();

            var totalBirdsByGender = birds
                .GroupBy(b => b.Gender)
                .ToDictionary(g => g.Key ?? "Unknown", g => g.Count());

            return totalBirdsByGender;
        }


        public async Task<int> GetTotalBirdsByStatusAndFarmId(string status, int farmId)
        {
            return await _context.Birds
            .Where(b => b.FarmId == farmId && b.Status == status)
            .CountAsync();
        }
    }
}
