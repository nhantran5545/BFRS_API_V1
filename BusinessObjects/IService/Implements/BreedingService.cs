using AutoMapper;
using BusinessObjects.RequestModels;
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

        public async Task CreateBreeding(BreedingAddRequest breedingRequest)
        {
            var breeding = _mapper.Map<Breeding>(breedingRequest);
            await _breedingRepository.AddAsync(breeding);
            _breedingRepository.SaveChanges();
        }

        public void DeleteBreeding(BreedingAddRequest breeding)
        {
            throw new NotImplementedException();
        }

        public void DeleteBreedingById(object breedingId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Breeding>> GetAllBreedings()
        {
            return await _breedingRepository.GetAllAsync();
        }

        public Task<IEnumerable<Breeding>> GetAllBreedingsByManagerId(object managerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Breeding?> GetBreedingById(object breedingId)
        {
            return await _breedingRepository.GetByIdAsync(breedingId);
        }

        public void UpdateBreeding(BreedingAddRequest breeding)
        {
            throw new NotImplementedException();
        }
    }
}
