using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IFarmService
    {
        Task CreateFarmAsync(Farm farm);
        void UpdateFarm(Farm farm);
        void DeleteFarm(Farm farm);
        void DeleteFarmById(object farmId);
        Task<IEnumerable<Farm>> GetAllFarmsAsync();
        Task<Farm?> GetFarmByIdAsync(object farmId);
    }
}
