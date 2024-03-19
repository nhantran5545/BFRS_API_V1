using AutoMapper;
using BusinessObjects.RequestModels;
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
    public class BreedingService : IBreedingService
    {
        private readonly IBreedingRepository _breedingRepository;
        private readonly IBirdRepository _birdRepository;
        private readonly IMapper _mapper;

        public BreedingService(IBreedingRepository breedingRepository, IBirdRepository birdRepository, IMapper mapper)
        {
            _breedingRepository = breedingRepository;
            _birdRepository = birdRepository;
            _mapper = mapper;
        }

        public async Task<double> CalculateInbreedingPercentage(Guid fatherBirdId, Guid motherBirdId)
        {
            var birdAlgorithmService = new BirdAlgorithmService(_birdRepository);
            var InbreedingPercentage = await birdAlgorithmService.GetInbreedingCoefficientOfParentsAsync(fatherBirdId, motherBirdId);
            return InbreedingPercentage;
        }

        public async Task<(int, Guid?)> CreateBreeding(BreedingAddRequest breedingRequest)
        {
            var breeding = _mapper.Map<Breeding>(breedingRequest);
            await _breedingRepository.AddAsync(breeding);
            var result = _breedingRepository.SaveChanges();
            if(result == -1)
            {
                return (result, null);
            }
            return (result, breeding.BreedingId);
        }

        public void DeleteBreeding(BreedingAddRequest breeding)
        {
            throw new NotImplementedException();
        }

        public void DeleteBreedingById(object breedingId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BreedingResponse>> GetAllBreedings()
        {
            var breedings = await _breedingRepository.GetAllAsync();
            return breedings.Select(br => _mapper.Map<BreedingResponse>(br));
        }

        public async Task<IEnumerable<BreedingResponse>> GetAllBreedingsByManagerId(object managerId)
        {
            var breedings = await _breedingRepository.GetAllBreedingsByManagerId(managerId);
            return breedings.Select(br => _mapper.Map<BreedingResponse>(br));
        }

        public async Task<BreedingDetailResponse?> GetBreedingById(object breedingId)
        {
            var breeding = await _breedingRepository.GetByIdAsync(breedingId);
            return _mapper.Map<BreedingDetailResponse>(breeding);
        }

        public void UpdateBreeding(BreedingAddRequest breeding)
        {
            throw new NotImplementedException();
        }
    }
}
