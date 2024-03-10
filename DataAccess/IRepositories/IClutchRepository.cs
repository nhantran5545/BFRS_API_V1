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
        Task<IEnumerable<Clutch>> GetAllClutchsByBreedingId(object breedingId);
        Task<IEnumerable<Clutch>> GetAllClutchsByCreatedById(object createdById);
    }
}
