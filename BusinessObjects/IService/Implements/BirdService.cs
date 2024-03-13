using DataAccess.IRepositories;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    public class BirdService : IBirdService
    {
        private readonly IBirdRepository _birdRepository;

        public BirdService(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public async Task<int> CreateBirdAsync(Bird bird)
        {
            await _birdRepository.AddAsync(bird);
            return _birdRepository.SaveChanges();
        }

        public void DeleteBird(Bird bird)
        {
            throw new NotImplementedException();
        }

        public void DeleteBirdById(object birdId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bird>> GetAllBirdsAsync()
        {
            return await _birdRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Bird>> GetAllBirdsByFarmId(object farmId)
        {
            return await _birdRepository.GetAllBirdsByFarmId(farmId);
        }

        public async Task<Bird?> GetBirdByIdAsync(object birdId)
        {
            return await _birdRepository.GetByIdAsync(birdId);
        }

        public void UpdateBird(Bird bird)
        {
            throw new NotImplementedException();
        }
    }
}
