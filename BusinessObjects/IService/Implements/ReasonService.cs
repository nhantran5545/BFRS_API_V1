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
        private readonly IBreedingRepository _breedingRepository;
        private readonly IClutchRepository _clutchRepository;
        private readonly IEggRepository _eggRepository;
        private readonly IMapper _mapper;

        public ReasonService(IBreedingStatusChangeRepository breedingStatusRepository,
            IClutchStatusChangeRepository clutchStatusRepository,
            IEggStatusChangeRepository eggStatusRepository, IBreedingRepository breedingRepository,
            IEggRepository eggRepository, IClutchRepository clutchRepository, 
            IMapper mapper)
        {
            _breedingStatusRepository = breedingStatusRepository;
            _clutchStatusRepository = clutchStatusRepository;
            _eggStatusRepository = eggStatusRepository;
            _breedingRepository = breedingRepository;
            _clutchRepository = clutchRepository;
            _eggRepository = eggRepository;
            _mapper = mapper;
        }

        public async Task<BreedingTimeline?> GetTimelineByBreedingId(int breedingId)
        {
            var breeding = await _breedingRepository.GetByIdAsync(breedingId);
            if(breeding == null)
            {
                return null;
            }

            var breedingTimeline = new BreedingTimeline();
            breedingTimeline.BreedingId = breedingId;
            var breedingStatuses = await _breedingStatusRepository.GetTimelineByBreedingIdAsync(breedingId);
            if (breedingStatuses.Any())
            {
                var breedingStatusesResponse = breedingStatuses.Select(bs => _mapper.Map<StatusChangeResponse>(bs));
                breedingTimeline.BreedingStatuses = breedingStatusesResponse.ToList();
                var clutches = await _clutchRepository.GetClutchsByBreedingId(breedingId);
                if (clutches.Any())
                {
                    var clutchTimelines = new List<ClutchTimeline>();
                    foreach (var item in clutches)
                    {
                        var clutchTimeline = new ClutchTimeline();
                        clutchTimeline.ClutchId = item.ClutchId;
                        var clutchStatuses = await _clutchStatusRepository.GetTimelineByClutchIdAsync(item.ClutchId);
                        if (clutchStatuses.Any())
                        {
                            var clutchStatusesResponse = clutchStatuses.Select(cs => _mapper.Map<StatusChangeResponse>(cs));
                            clutchTimeline.ClutchStatuses = clutchStatusesResponse.ToList();
                        }

                        var eggs = await _eggRepository.GetEggsByClutchIdAsync(item.ClutchId);
                        if (eggs.Any())
                        {
                            var eggTimelines = new List<EggTimeline>();
                            foreach (var item1 in eggs)
                            {
                                var eggTimeline = new EggTimeline();
                                eggTimeline.EggId = item1.EggId;
                                var eggStatuses = await _eggStatusRepository.GetTimelineByEggIdAsync(item1.EggId);
                                if (eggStatuses.Any())
                                {
                                    var eggStatusesResponse = eggStatuses.Select(es => _mapper.Map<StatusChangeResponse>(es));
                                    eggTimeline.EggStatuses = eggStatusesResponse.ToList();
                                }
                                eggTimelines.Add(eggTimeline);
                            }
                            clutchTimeline.EggTimelines = eggTimelines;
                        }
                        clutchTimelines.Add(clutchTimeline);
                    }
                    breedingTimeline.ClutchTimelines = clutchTimelines;
                }
            }

            return breedingTimeline;
        }
    }
}
