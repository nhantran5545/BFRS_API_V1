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
        Task<List<Breeding>> GetBreedingByAccountIdAsync(int accountId);
    }
}
