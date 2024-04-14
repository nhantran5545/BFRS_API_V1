using AutoMapper;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using DataAccess.IRepositories.Implements;
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

        public async Task<int> CreateBirdSpeciesAsync(BirdSpeciesRequest birdSpecy)
        {
            var species = _mapper.Map<BirdSpecy>(birdSpecy);
            species.Status = "Active";

            await _birdSpeciesRepository.AddAsync(species);
            var result = _birdSpeciesRepository.SaveChanges();
            if (result < 1)
            {
                return result;
            }
            return species.BirdSpeciesId;
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

        public async Task<bool> UpdateSpeciesAsync(int BirdSpecyId, BirdSpeciesRequest request)
        {
            var species = await _birdSpeciesRepository.GetByIdAsync(BirdSpecyId);
            if (species == null)
            {
                return false;
            }
            species.BirdSpeciesName = request.BirdSpeciesName;
            species.Description = request.Description;
            species.BirdTypeId = request.BirdTypeId;
            species.HatchingPhaseFrom = request.HatchingPhaseFrom;
            species.HatchingPhaseTo = request.HatchingPhaseTo;
            species.NestlingPhaseFrom = request.NestlingPhaseFrom;
            species.NestlingPhaseTo = request.NestlingPhaseTo;
            species.ChickPhaseFrom = request.ChickPhaseFrom;
            species.ChickPhaseTo = request.ChickPhaseTo;
            species.FledgingPhaseFrom = request.FledgingPhaseFrom;
            species.FledgingPhaseTo = request.FledgingPhaseTo;
            species.JuvenilePhaseFrom = request.JuvenilePhaseFrom;
            species.JuvenilePhaseTo = request.JuvenilePhaseTo;
            species.ImmaturePhaseFrom = request.ImmaturePhaseFrom;
            species.ImmaturePhaseTo = request.ImmaturePhaseTo;
            species.AdultPhaseFrom = request.AdultPhaseFrom;
            species.AdultPhaseTo = request.AdultPhaseTo;
            species.Status = request.Status;

            var result = _birdSpeciesRepository.SaveChanges();
            if (result < 1)
            {
                return false;
            }
            return true;
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
