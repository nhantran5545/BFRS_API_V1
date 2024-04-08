using AutoMapper;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new ArgumentNullException(nameof(areaAddRequest), "Data cannot be null");
            }
            if (areaAddRequest.Status != "For Nourishing" && areaAddRequest.Status != "For Breeding")
            {
                throw new ArgumentException("Status must be 'For Nourishing' or 'For Breeding'", nameof(areaAddRequest.Status));
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
            if (!IsManager(managerId))
            {
                throw new UnauthorizedAccessException("User is not authorized to access this resource.");
            }

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

        private bool IsManager(int accountId)
        {
            var account = _accountRepository.GetAccountById(accountId);
            return account != null && account.Role == "Manager";
        }
    }
}
