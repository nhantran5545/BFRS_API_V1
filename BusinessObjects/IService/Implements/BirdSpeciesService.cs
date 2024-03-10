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

        public async Task CreateBirdSpeciesAsync(BirdSpecy birdSpecy)
        {
            await _birdSpeciesRepository.AddAsync(birdSpecy);
            _birdSpeciesRepository.SaveChanges();
        }

        public void DeleteBirdSpecies(BirdSpecy birdSpecy)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BirdSpecy>> GetBirdSpeciesAsync()
        {
            return await _birdSpeciesRepository.GetAllAsync();
        }

        public async Task<BirdSpecy?> GetBirdSpeciesByIdAsync(object BirdSpecyId)
        {
            return await _birdSpeciesRepository.GetByIdAsync(BirdSpecyId);
        }

        public void UpdateBirdSpecies(BirdSpecy birdSpecy)
        {
            throw new NotImplementedException();
        }
    }
}
