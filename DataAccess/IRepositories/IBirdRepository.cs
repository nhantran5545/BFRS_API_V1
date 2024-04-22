using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IBirdRepository : IGenericRepository<Bird>
    {
        Task<IEnumerable<Bird>> GetBirdsByFarmId(object farmId);
        Task<IEnumerable<Bird>> GetBirdsBySpeciesId(object SpeciesId);
        Task<IEnumerable<Bird>> GetInRestBirdsBySpeciesIdAndFarmId(object SpeciesId, object FarmId);
        Task<IEnumerable<Bird>> GetBirdsByStaffId(object staffId);
        Task<int> GetTotalBirdsByStatusAndFarmId(string status, int farmId);
        Task<List<Bird>> GetBirdsByCageIdAsync(int cageId);
        Task<Dictionary<string, int>> GetBirdCountBySpeciesAndFarm(int farmId);
        Task<Dictionary<string, int>> GetTotalBirdsByGenderAndFarmId(int farmId);
    }
}
