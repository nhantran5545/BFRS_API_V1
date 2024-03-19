using DataAccess.Models;
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
        Task<IEnumerable<Bird>> GetInReproductionBirdsBySpeciesIdAndFarmId(object SpeciesId, object FarmId);
    }
}
