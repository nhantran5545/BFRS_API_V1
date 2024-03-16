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
    public class BirdSpeciesService : IBirdSpeciesService
    {
        private readonly IBirdSpeciesRepository _birdSpeciesRepository;
        private readonly IMapper _mapper;

        public BirdSpeciesService(IBirdSpeciesRepository birdSpeciesRepository, IMapper mapper)
        {
            _birdSpeciesRepository = birdSpeciesRepository;
            _mapper = mapper;
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

        public async Task<IEnumerable<BirdSpeciesResponse>> GetBirdSpeciesAsync()
        {
            var birdSpecies = await _birdSpeciesRepository.GetAllAsync();
            return birdSpecies.Select(bs => _mapper.Map<BirdSpeciesResponse>(bs));
        }

        public async Task<BirdSpeciesDetailResponse?> GetBirdSpeciesByIdAsync(object BirdSpecyId)
        {
            var birdSpecies = await _birdSpeciesRepository.GetByIdAsync(BirdSpecyId);
            return _mapper.Map<BirdSpeciesDetailResponse>(birdSpecies);
        }

        public void UpdateBirdSpecies(BirdSpecy birdSpecy)
        {
            throw new NotImplementedException();
        }
    }
}
