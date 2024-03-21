using AutoMapper;
using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using DataAccess.Models;
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
        private readonly IMapper _mapper;
        public CageService(ICageRepository cageRepository, IBirdRepository birdRepository, IMapper mapper)
        {
            _cageRepository = cageRepository;
            _birdRepository = birdRepository;
            _mapper = mapper;
        }

        public async Task CreateCageAsync(Cage cage)
        {
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
            var emptyCages = await _cageRepository.GetEmptyCagesByFarmId(farmId);
            if(emptyCages.Any())
            {
                cages.AddRange(emptyCages);
            }
            return cages.Select(c => _mapper.Map<CageResponse>(c));
        }

        public void UpdateCage(Cage cage)
        {
            throw new NotImplementedException();
        }
    }
}
