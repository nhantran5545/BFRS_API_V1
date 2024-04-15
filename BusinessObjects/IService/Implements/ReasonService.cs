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
        private readonly IBreedingStatusChangeRepository _breedingStatusRepository;
        private readonly IClutchStatusChangeRepository _clutchStatusRepository;
        private readonly IEggStatusChangeRepository _eggStatusRepository;
        private readonly IMapper _mapper;

        public ReasonService(IBreedingStatusChangeRepository breedingStatusRepository,
            IClutchStatusChangeRepository clutchStatusRepository,
            IEggStatusChangeRepository eggStatusRepository, IMapper mapper)
        {
            _breedingStatusRepository = breedingStatusRepository;
            _clutchStatusRepository = clutchStatusRepository;
            _eggStatusRepository = eggStatusRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReasonResponse>> GetStatusByBreedingId(object breedingId)
        {
            var breedingReasons = await _breedingStatusRepository.GetReasonsByBreedingIdAsync(breedingId);
            return breedingReasons.Select(reason => _mapper.Map<ReasonResponse>(reason));
        }
    }
}
