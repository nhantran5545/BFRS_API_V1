using DataAccess.IRepositories;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    public class FarmService : IFarmService
    {
        private readonly IFarmRepository _farmRepository;

        public FarmService(IFarmRepository farmRepository)
        {
            _farmRepository = farmRepository;
        }

        public Task CreateFarmAsync(Farm farm)
        {
            throw new NotImplementedException();
        }

        public void DeleteFarm(Farm farm)
        {
            throw new NotImplementedException();
        }

        public void DeleteFarmById(object farmId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Farm>> GetAllFarmsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Farm?> GetFarmByIdAsync(object farmId)
        {
            throw new NotImplementedException();
        }

        public void UpdateFarm(Farm farm)
        {
            throw new NotImplementedException();
        }
    }
}
