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
            if(birdSpecies == null)
            {
                return null;
            }
            return ConvertToResponse(birdSpecies);
        }

        public void UpdateBirdSpecies(BirdSpecy birdSpecy)
        {
            throw new NotImplementedException();
        }

        private BirdSpeciesDetailResponse ConvertToResponse(BirdSpecy birdSpecies)
        {
            var birdSpeciesResponse = _mapper.Map<BirdSpeciesDetailResponse>(birdSpecies);
            if(birdSpecies.SpeciesMutations.Any())
            {
                List<IndividualMutation> mutations = new List<IndividualMutation>();
                foreach (var item in birdSpecies.SpeciesMutations)
                {
                    mutations.Add(_mapper.Map<IndividualMutation>(item.Mutation));
                }
                birdSpeciesResponse.IndividualMutations = mutations;
            }
            return birdSpeciesResponse;
        }
    }
}
