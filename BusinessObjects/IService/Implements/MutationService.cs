using AutoMapper;
using BusinessObjects.RequestModels;
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
    public class MutationService : IMutationService
    {
        private readonly IMutationRepository _mutationRepository;
        private readonly ISpeciesMutationRepository _speciesMutationRepository;
        private readonly IMapper _mapper;

        public MutationService(IMutationRepository mutationRepository, ISpeciesMutationRepository speciesMutationRepository, IMapper mapper)
        {
            _mutationRepository = mutationRepository;
            _speciesMutationRepository = speciesMutationRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateMutationAsync(MutationRequest mutationRequest)
        {
            var mutation = _mapper.Map<Mutation>(mutationRequest);
            if(mutation == null)
            {
                return -1;
            }
            await _mutationRepository.AddAsync(mutation);
            _mutationRepository.SaveChanges();
            return mutation.MutationId;
        }

        public async Task<IEnumerable<IndividualMutation>> GetAllMutationsAsync()
        {
            var mutations = await _mutationRepository.GetAllAsync();
            return mutations.Select(mutation => _mapper.Map<IndividualMutation>(mutation));
        }

        public async Task<IEnumerable<IndividualMutation>> GetMutationsBySpeciesIdAsync(int speciesId)
        {
            var mutations = await _speciesMutationRepository.GetBySpeciesIdAsync(speciesId);
            return mutations.Select(mutation => _mapper.Map<IndividualMutation>(mutation));
        }

        public async Task<IndividualMutation?> GetMutationByIdAsync(object mutationId)
        {
            var mutation = await _mutationRepository.GetByIdAsync(mutationId);
            return _mapper.Map<IndividualMutation>(mutation);
        }
    }
}
