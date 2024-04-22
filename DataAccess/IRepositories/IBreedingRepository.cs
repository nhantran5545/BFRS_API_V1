using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IBreedingRepository : IGenericRepository<Breeding>
    {
        Task<IEnumerable<Breeding>> GetAllBreedingsByManagerId(object managerId);
        Task<IEnumerable<Breeding>> GetAllBreedingsByStaff(object accountId);
        Task<int> GetTotalStatusBreedingsByStaff(int accountId, string statusName);
        Task<int> GetTotalStatusBreedingsByManagerId(int accountId, string statusName);
        Task<List<Breeding>> GetBreedingByAccountIdAsync(int accountId);
        Task<IEnumerable<Breeding>> GetAllBreedingsStatusByManagerId(int managerId, string statusName);
        Dictionary<string, int> GetTotalBreedingByFarm();
    }
}
