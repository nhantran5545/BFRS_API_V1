using AutoMapper;
using BusinessObjects.RequestModels;
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
            // You can add more conditions for other statuses if needed

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
            var cages = await _cageRepository.GetAllAsync();
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

        public void UpdateCage(Cage cage)
        {
            throw new NotImplementedException();
        }
    }
}
