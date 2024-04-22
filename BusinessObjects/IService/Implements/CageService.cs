using AutoMapper;
using BusinessObjects.RequestModels.CageReqModels;
using BusinessObjects.ResponseModels.CageResModels;
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
    public class CageService : ICageService
    {
        private readonly ICageRepository _cageRepository;
        private readonly IBirdRepository _birdRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public CageService(ICageRepository cageRepository, IBirdRepository birdRepository, IMapper mapper, IAreaRepository areaRepository , IAccountRepository accountRepository)
        {
            _cageRepository = cageRepository;
            _birdRepository = birdRepository;
            _areaRepository = areaRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public async Task CreateCageAsync(CageAddRequest request)
        {
            var area = await _areaRepository.GetByIdAsync(request.AreaId);
            if (area == null)
            {
                throw new Exception("Invalid area or Area is not exist");
            }

            var account = await _accountRepository.GetByIdAsync(request.AccountId);
            if (account == null)
            {
                throw new Exception("Invalid account or Account is not exist");
            }

            var cage = _mapper.Map<Cage>(request);

            if (area.Status == "For Nourishing")
            {
                cage.Status = "Nourishing";
            }
            else if (area.Status == "For Breeding")
            {
                cage.Status = "Standby";
            }

            await _cageRepository.AddAsync(cage);
            _cageRepository.SaveChanges();
        }


        public async Task<IEnumerable<CageResponse>> GetAllCagesAsync()
        {
            var cages = await _cageRepository.GetAllCagesAsync();
            return cages.Select(c => _mapper.Map<CageResponse>(c));
        }

        public async Task<CageDetailResponse?> GetCageByIdAsync(object cageId)
        {
            var cage = await _cageRepository.GetByIdAsync(cageId);
            return _mapper.Map<CageDetailResponse>(cage);
        }

        public async Task<IEnumerable<CageResponse>> GetCagesByStaffIdAsync(int staffAccountId)
        {
            var cages = await _cageRepository.GetCagesManagedByStaffAsync(staffAccountId);
            return _mapper.Map<IEnumerable<CageResponse>>(cages);
        }

        public async Task<IEnumerable<CageResponse>> GetCagesByFarmIdAsync(int farmId)
        {
            var cages = await _cageRepository.GetCagesByFarmIdAsync(farmId);
            return _mapper.Map<IEnumerable<CageResponse>>(cages);
        }

        public async Task<int> GetTotalCagesByStaffIdAsync(int accountId)
        {
            var cages = await _cageRepository.GetCagesManagedByStaffAsync(accountId);
            return cages.Count();
        }

        public async Task<int> GetTotalCagesStatusByFarmIdAsync(string status, int farmId)
        {
            return await _cageRepository.GetTotalCageStatusByFarmIdAsync(farmId, status);
        }

        public Dictionary<int, int> GetCageCountByAreaAndFarm(int farmId)
        {
            return _cageRepository.GetCageCountByAreaAndFarm(farmId);
        }

        public async Task<IEnumerable<CageResponse>> GetCagesForBreeding(int fatherBirdId, int motherBirdId, int farmId)
        {
            List<Cage> cages = new List<Cage>();
            var fatherBird = await _birdRepository.GetByIdAsync(fatherBirdId);
            if (fatherBird != null && fatherBird.CageId != null)
            {
                var fatherCage = await _cageRepository.GetByIdAsync(fatherBird.CageId);
                if(fatherCage != null)
                    cages.Add(fatherCage);
            }
            var motherBird = await _birdRepository.GetByIdAsync(motherBirdId);
            if (motherBird != null && motherBird.CageId != null)
            {
                var motherCage = await _cageRepository.GetByIdAsync(motherBird.CageId);
                if (motherCage != null)
                    cages.Add(motherCage);
            }
            var emptyCages = await _cageRepository.GetStandbyCagesByFarmId(farmId);
            if(emptyCages.Any())
            {
                cages.AddRange(emptyCages);
            }
            return cages.Select(c => _mapper.Map<CageResponse>(c));
        }

        public async Task<IEnumerable<CageResponse>> GetCagesForBreeding(int farmId)
        {
            var cages = await _cageRepository.GetStandbyCagesByFarmId(farmId);
            return cages.Select(c => _mapper.Map<CageResponse>(c));
        }

        public async Task<bool> UpdateCageAsync(int cageId, CageUpdateRequest request)
        {
            var cage = await _cageRepository.GetByIdAsync(cageId);
            if (cage == null)
            {
                 return false;
            }

            var currentArea = await _areaRepository.GetByIdAsync(cage.AreaId);
            if (currentArea == null)
            {
                throw new ArgumentException("Current area not found");
            }

            var targetArea = await _areaRepository.GetByIdAsync(request.AreaId);
            if (targetArea == null)
            {
                throw new ArgumentException("Target area not found");
            }

            // Check if the cage has birds
            var birdsInCage = await _birdRepository.GetBirdsByCageIdAsync(cageId);

            if (currentArea.Status == "For Nourishing" && targetArea.Status == "For Breeding" && birdsInCage.Any())
            {
                throw new ArgumentException("Please move birds to another cage before transferring the cage to a breeding area");
            }

            if (currentArea.Status == "For Nourishing" && targetArea.Status == "For Breeding" && !birdsInCage.Any())
            {
                cage.Status = "Standby";
            }

            if (currentArea.Status == "For Breeding" && targetArea.Status == "For Nourishing" && cage.Status != "Standby")
            {
                throw new ArgumentException("You can only transfer a cage with standby status from breeding area to nourishing area");
            } 
            cage.ManufacturedDate = request.ManufacturedDate;
            cage.ManufacturedAt = request.ManufacturedAt;
            cage.PurchasedDate = request.PurchasedDate;
            cage.AreaId = request.AreaId;
            cage.AccountId = request.AccountId;

            _cageRepository.Update(cage);
            var result = _cageRepository.SaveChanges();
            if (result < 1)
            {
                return false;
            }
            return true;
        }

        public List<Dictionary<string, object>> GetTotalCageByFarm()
        {
            return _cageRepository.GetTotalCageByFarm();
        }

    }
}
