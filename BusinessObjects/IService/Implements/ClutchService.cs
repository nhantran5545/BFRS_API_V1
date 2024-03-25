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
    public class ClutchService : IClutchService
    {
        private readonly IClutchRepository _clutchRepository;
        private readonly IBreedingService _breedingService;
        private readonly IMapper _mapper;

        public ClutchService(IClutchRepository clutchRepository,  IMapper mapper, IBreedingService breedingService)
        {
            _clutchRepository = clutchRepository;
            _mapper = mapper;
            _breedingService = breedingService;
        }

        public async Task<int> CreateClutchAsync(ClutchAddRequest clutchAddRequest)
        {
            var clutch = _mapper.Map<Clutch>(clutchAddRequest);
            if(clutch == null)
            {
                return -1;
            }

            var breeding = await _breedingService.GetBreedingById(clutchAddRequest.BreedingId);
            if(breeding == null)
            {
                return -1;
            }
            if(breeding.Status != "InProgress")
            {
                breeding.Status = "InProgress";
            }

            clutch.Status = "Created";
            clutch.CreatedDate = DateTime.Now;
            await _clutchRepository.AddAsync(clutch);
            var result = _clutchRepository.SaveChanges();
            if(result < 1)
            {
                return result;
            }
            return clutch.ClutchId;
        }

        public void DeleteClutch(Clutch clutch)
        {
            throw new NotImplementedException();
        }

        public void DeleteClutchtById(object clutchId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClutchResponse>> GetAllClutchsAsync()
        {
            var clutchs = await _clutchRepository.GetAllAsync();
            return clutchs.Select(c => _mapper.Map<ClutchResponse>(c));
        }

        public async Task<IEnumerable<ClutchResponse>> GetClutchsByBreedingId(object breedingId)
        {
            var clutchs = await _clutchRepository.GetClutchsByBreedingId(breedingId);
            return clutchs.Select(c => _mapper.Map<ClutchResponse>(c));
        }

        public async Task<IEnumerable<ClutchResponse>> GetClutchsByCreatedById(object CreatedById)
        {
            var clutchs = await _clutchRepository.GetClutchsByCreatedById(CreatedById);
            return clutchs.Select(c => _mapper.Map<ClutchResponse>(c));
        }

        public async Task<ClutchDetailResponse?> GetClutchByIdAsync(object clutchId)
        {
            var clutch = await _clutchRepository.GetByIdAsync(clutchId);
            return _mapper.Map<ClutchDetailResponse>(clutch);
        }

        public void UpdateClutch(Clutch clutch)
        {
            throw new NotImplementedException();
        }
    }
}
