using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IEggRepository : IGenericRepository<Egg>
    {
        Task<IEnumerable<Egg>> GetEggsByClutchIdAsync(object clutchId);
        Task<Egg?> GetEggByBirdIdAsync(object birdId);
        Task<Egg?> GetEggDetailsAsync(object eggId);
        Task<IEnumerable<Egg>> GetEggsByStaffId(int staffId);
        Task<int> GetEggCountByStatusNameAndManagedByStaff(string statusName, int staffId);
        Task<int> GetTotalEggsCountByAccountId(int accountId);
    }
}
