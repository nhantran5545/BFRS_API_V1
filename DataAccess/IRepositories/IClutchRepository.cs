using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IClutchRepository : IGenericRepository<Clutch>
    {
        Task<IEnumerable<Clutch>> GetClutchsByBreedingId(object breedingId);
        Task<IEnumerable<Clutch>> GetClutchsByCreatedById(object createdById);
        Task<int> GetTotalClutchesCountByAccountId(int accountId);
    }
}
