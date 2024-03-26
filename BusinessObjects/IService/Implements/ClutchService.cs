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
        private readonly IBreedingRepository _breedingRepository;
        private readonly IMapper _mapper;

        public ClutchService(IClutchRepository clutchRepository, IBreedingRepository breedingRepository, IMapper mapper)
        {
            _clutchRepository = clutchRepository;
            _breedingRepository = breedingRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateClutchAsync(ClutchAddRequest clutchAddRequest)
        {
            using(var transaction = _clutchRepository.BeginTransaction())
            {
                try
                {
                    var clutch = _mapper.Map<Clutch>(clutchAddRequest);
                    if (clutch == null)
                    {
                        return -1;
                    }

                    var breeding = await _breedingRepository.GetByIdAsync(clutchAddRequest.BreedingId);
                    if (breeding == null)
                    {
                        return -1;
                    }
                    if (breeding.Status != "InProgress")
                    {
                        breeding.Status = "InProgress";
                        _breedingRepository.SaveChanges();
                    }

                    clutch.Status = "Created";
                    clutch.CreatedDate = DateTime.Now;
                    await _clutchRepository.AddAsync(clutch);
                    _clutchRepository.SaveChanges();
                    transaction.Commit();
                    return clutch.ClutchId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return -1;
                }
            }
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
            if(clutch == null)
            {
                return null;
            }

            var clutchResponse = _mapper.Map<ClutchDetailResponse>(clutch);
            clutchResponse.EggResponses = clutch.Eggs.Select(e => _mapper.Map<EggResponse>(e)).ToList();
            return clutchResponse;
        }

        public void UpdateClutch(Clutch clutch)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CloseClutch(ClutchUpdateRequest clutchUpdateRequest)
        {
            var clutch = await _clutchRepository.GetByIdAsync(clutchUpdateRequest.ClutchId);
            if(clutch == null)
            {
                return false;
            }

            clutch.Status = "Closed";
            clutch.UpdatedBy = clutchUpdateRequest.UpdatedBy;
            clutch.UpdatedDate = DateTime.Now;
            var result = _clutchRepository.SaveChanges();
            if (result < 1)
            {
                return false;
            }
            return true;
        }
    }
}
