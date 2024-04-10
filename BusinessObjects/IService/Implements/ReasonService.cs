using AutoMapper;
using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    public class ReasonService : IReasonService
    {
        private readonly IBreedingReasonRepository _breedingReasonRepository;
        private readonly IMapper _mapper;

        public ReasonService(IBreedingReasonRepository breedingReasonRepository, IMapper mapper)
        {
            _breedingReasonRepository = breedingReasonRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReasonResponse>> GetReasonsByBreedingId(object breedingId)
        {
            var breedingReasons = await _breedingReasonRepository.GetReasonsByBreedingIdAsync(breedingId);
            return breedingReasons.Select(reason => _mapper.Map<ReasonResponse>(reason));
        }
    }
}
