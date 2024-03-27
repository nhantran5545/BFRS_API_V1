using AutoMapper;
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

namespace BusinessObjects.IService.Implements
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _repository;
        private readonly IAccountRepository _accountRepository; 
        private readonly IMapper _mapper;

        public AreaService(IAreaRepository repository, IAccountRepository accountRepository, IMapper mapper)
        {
            _repository = repository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AreaResponse>> GetAllAreaAsync()
        {
            var area = await _repository.GetAllAsync();
            return area.Select(b => _mapper.Map<AreaResponse>(b));
        }

        public IEnumerable<AreaResponse> GetAreaByManagerId(int managerId)
        {
            if (!IsManager(managerId))
            {
                throw new UnauthorizedAccessException("User is not authorized to access this resource.");
            }

            int farmId = GetFarmIdByAccountId(managerId);

            var areas = _repository.GetAreasByFarmId(farmId);

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

        private bool IsManager(int accountId)
        {
            var account = _accountRepository.GetAccountById(accountId);
            return account != null && account.Role == "Manager";
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
            var area = await _repository.GetByIdAsync(areaId);
            return _mapper.Map<AreaResponse>(area);
        }
    }
}
