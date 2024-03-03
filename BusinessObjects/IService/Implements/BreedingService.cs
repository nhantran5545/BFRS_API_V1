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

        public BreedingService(IBreedingRepository breedingRepository)
        {
            _breedingRepository = breedingRepository;
        }

        public Task<float> CalculateInbreedingPercentage(Bird FatherBird, Bird MotherBird)
        {
            throw new NotImplementedException();
        }

        public Task CreateBreeding(Breeding breeding)
        {
            throw new NotImplementedException();
        }

        public void DeleteBreeding(Breeding breeding)
        {
            throw new NotImplementedException();
        }

        public void DeleteBreedingById(object breedingId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Breeding>> GetAllBreedings()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Breeding>> GetAllBreedingsByManagerId(object managerId)
        {
            throw new NotImplementedException();
        }

        public Task<Breeding?> GetBreedingById(object breedingId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBreeding(Breeding breeding)
        {
            throw new NotImplementedException();
        }
    }
}
