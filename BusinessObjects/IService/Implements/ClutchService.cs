using AutoMapper;
using BusinessObjects.RequestModels.ClutchReqModels;
using BusinessObjects.ResponseModels;
using BusinessObjects.ResponseModels.ClutchResModels;
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
        private readonly IClutchStatusChangeRepository _clutchStatusChangeRepository;
        private readonly IBreedingStatusChangeRepository _breedingStatusChangeRepository;
        private readonly IMapper _mapper;

        public ClutchService(IClutchRepository clutchRepository, IBreedingRepository breedingRepository, 
            IClutchStatusChangeRepository clutchStatusChangeRepository, IBreedingStatusChangeRepository breedingStatusChangeRepository,
            IMapper mapper)
        {
            _clutchRepository = clutchRepository;
            _breedingRepository = breedingRepository;
            _clutchStatusChangeRepository = clutchStatusChangeRepository;
            _breedingStatusChangeRepository = breedingStatusChangeRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateClutchAsync(ClutchAddRequest clutchAddRequest, int accountId)
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
                        breeding.Phase = 0;
                        _breedingRepository.SaveChanges();

                        await AddBreedingChangeStatus(breeding.BreedingId, null, accountId, "Mating", "InProgress");
                    }

                    clutch.Status = "Created";
                    clutch.Phase = 2;
                    clutch.CreatedBy = accountId;
                    clutch.CreatedDate = DateTime.Now;
                    await _clutchRepository.AddAsync(clutch);
                    _clutchRepository.SaveChanges();

                    await AddClutchChangeStatus(clutch.ClutchId, null, accountId, null, clutch.Status);

                    transaction.Commit();
                    return clutch.ClutchId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    transaction.Rollback();
                    return -1;
                }
            }
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

        public async Task<bool> UpdateClutch(int clutchId, ClutchUpdateRequest clutchUpdateRequest, int accountId)
        {
            var clutch = await _clutchRepository.GetByIdAsync(clutchId);
            if (clutch == null)
            {
                return false;
            }

            clutch.BroodStartDate = clutchUpdateRequest.BroodStartDate;
            clutch.BroodEndDate = clutchUpdateRequest.BroodEndDate;
            clutch.UpdatedBy = accountId;
            clutch.UpdatedDate = DateTime.Now;
            var result = _clutchRepository.SaveChanges();
            if(result < 1)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CloseClutch(int clutchId, ClutchCloseRequest clutchCloseRequest, int accountId)
        {
            var clutch = await _clutchRepository.GetByIdAsync(clutchId);
            if(clutch == null)
            {
                return false;
            }

            var oldStatus = clutch.Status;
            clutch.Status = clutchCloseRequest.Status;
            clutch.Phase = 0;
            clutch.UpdatedBy = accountId;
            clutch.UpdatedDate = DateTime.Now;
            var result = _clutchRepository.SaveChanges();
            if (result < 1)
            {
                return false;
            }

            await AddClutchChangeStatus(clutch.ClutchId, clutchCloseRequest.Status, accountId, oldStatus, clutchCloseRequest.Status);
            return true;
        }

        private async Task AddClutchChangeStatus(int clutchId, string? reason, int changedBy, string? oldStatus, string newStatus)
        {
            var clutchReason = new ClutchStatusChange()
            {
                ClutchId = clutchId,
                Description = reason,
                ChangedDate = DateTime.Now,
                ChangedBy = changedBy,
                OldStatus = oldStatus,
                NewStatus = newStatus
            };
            await _clutchStatusChangeRepository.AddAsync(clutchReason);
            _clutchStatusChangeRepository.SaveChanges();
        }

        private async Task AddBreedingChangeStatus(int breedingId, string? reason, int changedBy, string? oldStatus, string newStatus)
        {
            var breedingStatus = new BreedingStatusChange()
            {
                BreedingId = breedingId,
                Description = reason,
                ChangedDate = DateTime.Now,
                ChangedBy = changedBy,
                OldStatus = oldStatus,
                NewStatus = newStatus
            };
            await _breedingStatusChangeRepository.AddAsync(breedingStatus);
            _breedingStatusChangeRepository.SaveChanges();
        }
    }
}
