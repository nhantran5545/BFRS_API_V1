using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
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
        Task<int> CreateFarmAsync(FarmRequest farmRequest);
        Task<bool> UpdateFarmAsync(int farmId, FarmUpdateRequest request);
        void DeleteFarm(Farm farm);
        void DeleteFarmById(object farmId);
        Task<IEnumerable<FarmResponse>> GetAllFarmsAsync();
        Task<FarmResponse?> GetFarmByIdAsync(int farmId);
    }
}
