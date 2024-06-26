﻿using AutoMapper;
using Azure;
using Azure.Core;
using BusinessObjects.RequestModels.FarmReqModels;
using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using DataAccess.IRepositories.Implements;
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

        public async Task<int> CreateFarmAsync(FarmRequest farmRequest)
        {
           
            var farm = _mapper.Map<Farm>(farmRequest);
            farm.Status = "Active";

            await _farmRepository.AddAsync(farm);
            var result = _farmRepository.SaveChanges();
            if (result < 1)
            {
                return result;
            }
            return farm.FarmId;
        }

        public void DeleteFarm(Farm farm)
        {
            throw new NotImplementedException();
        }

        public void DeleteFarmById(object farmId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FarmResponse>> GetAllFarmsAsync()
        {
            var farm = await _farmRepository.GetAllAsync();
            return farm.Select(b => _mapper.Map<FarmResponse>(b));
        }

        public async Task<FarmResponse?> GetFarmByIdAsync(int farmId)
        {
            var farm = await _farmRepository.GetByIdAsync(farmId);
            return _mapper.Map<FarmResponse>(farm);
        }

        public async Task<bool> UpdateFarmAsync(int farmId, FarmUpdateRequest request)
        {
            var farm = await _farmRepository.GetByIdAsync(farmId);
            if (farm == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(request.FarmName))
            {
                farm.FarmName = request.FarmName;
            }
            if (!string.IsNullOrEmpty(request.Address))
            {
                farm.Address = request.Address;
            }
            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                farm.PhoneNumber = request.PhoneNumber;
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                farm.Description = request.Description;
            }
            if (request.Image != null) 
            {
                farm.Image = request.Image;
            }

            _farmRepository.Update(farm);
            var result = _farmRepository.SaveChanges();
            if (result < 1)
            {
                return false;
            }
            return true;
        }


        public async Task<bool> ChangStatusFarmById(int farmId)
        {

            var farm = await _farmRepository.GetByIdAsync(farmId);
            if (farm == null)
            {
                return false;
            }

            farm.Status = farm.Status == "Active" ? "InActive" : "Active";

            var result = _farmRepository.SaveChanges();
            if (result < 1)
            {
                return false;
            }
            return true;
        }

        public async Task<List<object>> GetTotalAccountByFarm()
        {
            return await _farmRepository.GetTotalAccountByFarm();
        }
    }
}
