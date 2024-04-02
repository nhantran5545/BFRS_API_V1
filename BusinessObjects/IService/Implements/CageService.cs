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
    public class CageService : ICageService
    {
        private readonly ICageRepository _cageRepository;
        private readonly IBirdRepository _birdRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IMapper _mapper;
        public CageService(ICageRepository cageRepository, IBirdRepository birdRepository, IMapper mapper, IAreaRepository areaRepository)
        {
            _cageRepository = cageRepository;
            _birdRepository = birdRepository;
            _areaRepository = areaRepository;
            _mapper = mapper;
        }
        public async Task CreateCageAsync(CageAddRequest request)
        {
            var area = await _areaRepository.GetByIdAsync(request.AreaId);

            if (area == null)
            {
                throw new Exception("Invalid area");
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
        public void DeleteCage(Cage cage)
        {
            throw new NotImplementedException();
        }

        public void DeleteCageById(object cageId)
        {
            throw new NotImplementedException();
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
                throw new Exception("Cage not found");
            }

            var currentArea = await _areaRepository.GetByIdAsync(cage.AreaId);
            if (currentArea == null)
            {
                throw new Exception("Current area not found");
            }

            var targetArea = await _areaRepository.GetByIdAsync(request.AreaId);
            if (targetArea == null)
            {
                throw new Exception("Target area not found");
            }

            // Check if the cage has birds
            var birdsInCage = await _birdRepository.GetBirdsByCageIdAsync(cageId);

            if (currentArea.Status == "For Nourishing" && targetArea.Status == "For Breeding" && birdsInCage.Any())
            {
                throw new Exception("Please move birds to another cage before transferring the cage to a breeding area");
            }

            if (currentArea.Status == "For Nourishing" && targetArea.Status == "For Breeding" && !birdsInCage.Any())
            {
                cage.Status = "Standby";
            }

            if (currentArea.Status == "For Breeding" && targetArea.Status == "For Nourishing" && cage.Status != "Standby")
            {
                throw new Exception("You can only transfer a cage with standby status from breeding area to nourishing area");
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

    }
}
