using AutoMapper;
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
        private readonly IMapper _mapper;

        public FarmService(IFarmRepository farmRepository, IMapper mapper)
        {
            _farmRepository = farmRepository;
            _mapper = mapper;
        }

        public async Task CreateFarmAsync(Farm farm)
        {
            await _farmRepository.AddAsync(farm);
            _farmRepository.SaveChanges();
        }

        public void DeleteFarm(Farm farm)
        {
            throw new NotImplementedException();
        }

        public void DeleteFarmById(object farmId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Farm>> GetAllFarmsAsync()
        {
            return await _farmRepository.GetAllAsync();
        }

        public async Task<Farm?> GetFarmByIdAsync(object farmId)
        {
            return await _farmRepository.GetByIdAsync(farmId);
        }

        public void UpdateFarm(Farm farm)
        {
            throw new NotImplementedException();
        }
    }
}
