﻿using AutoMapper;
using Azure.Core;
using Azure;
using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using DataAccess.IRepositories.Implements;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.RequestModels.AreaReqModels;

namespace BusinessObjects.IService.Implements
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IAccountRepository _accountRepository; 
        private readonly IMapper _mapper;

        public AreaService(IAreaRepository areaRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _areaRepository = areaRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateAreaAsync(AreaAddRequest areaAddRequest)
        {
            if (areaAddRequest == null)
            {
                return -1;
            }

            var area = _mapper.Map<Area>(areaAddRequest);
            await _areaRepository.AddAsync(area);
            var result = _areaRepository.SaveChanges();
            if (result < 1)
            {
                return result;
            }

            return area.AreaId;
        }

        public async Task<IEnumerable<AreaResponse>> GetAllAreaAsync()
        {
            var area = await _areaRepository.GetAllAsync();
            return area.Select(b => _mapper.Map<AreaResponse>(b));
        }

        public IEnumerable<AreaResponse> GetAreaByManagerId(int managerId)
        {

            int farmId = GetFarmIdByAccountId(managerId);

            var areas = _areaRepository.GetAreasByFarmId(farmId);

            var areaDTOs = areas.Select(a => new AreaResponse
            {
                AreaId = a.AreaId,
                AreaName = a.AreaName,
                Description = a.Description,
                FarmId = a.FarmId, 
                Status = a.Status 
            });
            return areaDTOs;
        }



        private int GetFarmIdByAccountId(int accountId)
        {
            var account = _accountRepository.GetAccountById(accountId);
            if (account == null || account.FarmId == null)
            {
                throw new InvalidOperationException("FarmId is not assigned to the account.");
            }
            return account.FarmId.Value;
        }

        public async Task<AreaResponse?> GetAreaByIdAsync(object areaId)
        {
            var area = await _areaRepository.GetByIdAsync(areaId);
            return _mapper.Map<AreaResponse>(area);
        }

        public async Task<bool> UpdateAreaAsync(int areaId, AreaUpdateRequest areaUpdateRequest)
        {
            var area = await _areaRepository.GetByIdAsync(areaId);
            if (area == null)
            {
                return false;
            }
            area.AreaName = areaUpdateRequest.AreaName;
            area.Description = areaUpdateRequest.Description;

            _areaRepository.Update(area);
            var result = _areaRepository.SaveChanges();
            if (result < 1)
            {
                return false;
            }
            return true;
        }
    }
}
