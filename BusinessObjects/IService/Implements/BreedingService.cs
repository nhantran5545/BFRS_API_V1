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
        private readonly BirdAlgorithmService birdAlgorithmService;

        public BreedingService(IBreedingRepository breedingRepository, IBirdRepository birdRepository)
        {
            _breedingRepository = breedingRepository;
            _birdRepository = birdRepository;
            birdAlgorithmService = new BirdAlgorithmService(birdRepository);
        }

        public Task<float> CalculateInbreedingPercentage(Bird FatherBird, Bird MotherBird)
        {
            throw new NotImplementedException();
        }

        public async Task CreateBreeding(Breeding breeding)
        {
            await _breedingRepository.AddAsync(breeding);
            _breedingRepository.SaveChanges();
        }

        public void DeleteBreeding(Breeding breeding)
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

        public void UpdateBreeding(Breeding breeding)
        {
            throw new NotImplementedException();
        }
    }
}
