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

        public Task CreateBirdAsync(Bird bird)
        {
            throw new NotImplementedException();
        }

        public void DeleteBird(Bird bird)
        {
            throw new NotImplementedException();
        }

        public void DeleteBirdById(object birdId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bird>> GetAllBirdsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bird>> GetAllBirdsByFarmId(object farmId)
        {
            throw new NotImplementedException();
        }

        public Task<Bird?> GetBirdByIdAsync(object birdId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBird(Bird bird)
        {
            throw new NotImplementedException();
        }
    }
}
