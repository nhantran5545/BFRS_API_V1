using DataAccess.IRepositories;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    public class BirdSpeciesService : IBirdSpeciesService
    {
        private readonly IBirdSpeciesRepository _birdSpeciesRepository;

        public BirdSpeciesService(IBirdSpeciesRepository birdSpeciesRepository)
        {
            _birdSpeciesRepository = birdSpeciesRepository;
        }

        public Task CreateBirdSpeciesAsync(BirdSpecy birdSpecy)
        {
            throw new NotImplementedException();
        }

        public void DeleteBirdSpecies(BirdSpecy birdSpecy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BirdSpecy>> GetBirdSpeciesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BirdSpecy> GetBirdSpeciesByIdAsync(object BirdSpecyId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBirdSpecies(BirdSpecy birdSpecy)
        {
            throw new NotImplementedException();
        }
    }
}
