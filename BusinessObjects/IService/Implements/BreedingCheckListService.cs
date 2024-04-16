using AutoMapper;
using BusinessObjects.RequestModels.ChecklistReqModels;
using BusinessObjects.ResponseModels.BreedingChecklistResModels;
using BusinessObjects.ResponseModels.ChecklistResModels;
using DataAccess.IRepositories;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    public class BreedingCheckListService : IBreedingCheckListService
    {
        private readonly IBreedingCheckListRepository _breedingCheckListRepository;
        private readonly IBreedingCheckListDetailRepository _breedingCheckListDetailRepository;
        private readonly ICheckListRepository _checkListRepository;
        private readonly ICheckListDetailRepository _checkListDetailRepository;
        private readonly IMapper _mapper;   

        public BreedingCheckListService(IBreedingCheckListRepository breedingCheckListRepository, IBreedingCheckListDetailRepository breedingCheckListDetailRepository,
            ICheckListRepository checkListRepository, ICheckListDetailRepository checkListDetailRepository, IMapper mapper)
        {
            _breedingCheckListRepository = breedingCheckListRepository;
            _breedingCheckListDetailRepository = breedingCheckListDetailRepository;
            _checkListRepository = checkListRepository;
            _checkListDetailRepository = checkListDetailRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BreedingCheckListResponse>> GetBreedingCheckListsAsync()
        {
            var breedingCheckLists = await _breedingCheckListRepository.GetAllAsync();
            if(breedingCheckLists == null)
            {
                return Enumerable.Empty<BreedingCheckListResponse>();
            }
            return breedingCheckLists.Select(bc => ConvertToResponse(bc));
        }

        public async Task<IEnumerable<BreedingCheckListResponse>> GetBreedingCheckListsByBreedingId(int breedingId)
        {
            var breedingCheckLists = await _breedingCheckListRepository.GetBreedingCheckListsByBreedingId(breedingId);
            if (breedingCheckLists == null)
            {
                return Enumerable.Empty<BreedingCheckListResponse>();
            }
            return breedingCheckLists.Select(bc => ConvertToResponse(bc));
        }

        public async Task<IEnumerable<BreedingCheckListResponse>> GetBreedingCheckListsByBreedingIdAndPhase(int breedingId, int phase)
        {
            var breedingCheckLists = await _breedingCheckListRepository.GetBreedingCheckListsByBreedingIdAndPhase(breedingId, phase);
            if (breedingCheckLists == null)
            {
                return Enumerable.Empty<BreedingCheckListResponse>();
            }
            return breedingCheckLists.Select(bc => ConvertToResponse(bc));
        }

        public async Task<IEnumerable<BreedingCheckListResponse>> GetBreedingCheckListsByClutchIdAndPhase(int clutchId, int phase)
        {
            var breedingCheckLists = await _breedingCheckListRepository.GetBreedingCheckListsByClutchIdAndPhase(clutchId, phase);
            if (breedingCheckLists == null)
            {
                return Enumerable.Empty<BreedingCheckListResponse>();
            }
            return breedingCheckLists.Select(bc => ConvertToResponse(bc));
        }

        public async Task<BreedingCheckListResponse?> GetBreedingCheckListDetail(int breedingCheckListId)
        {
            var breedingCheckList = await _breedingCheckListRepository.GetByIdAsync(breedingCheckListId);
            if(breedingCheckList == null)
            {
                return null;
            }

            return ConvertToResponse(breedingCheckList);
        }

        public async Task<BreedingCheckListResponse?> GetTodayBreedingCheckListDetail(int breedingId, int phase)
        {
            var breedingCheckList = await _breedingCheckListRepository.GetTodayCheckListByBreedingIdAndPhase(breedingId, phase);
            if (breedingCheckList == null)
            {
                var breedingCheckListResponse = new BreedingCheckListResponse();
                var checkList = await _checkListRepository.GetCheckListByPhase(phase);
                if (checkList == null)
                {
                    return null;
                }
                breedingCheckListResponse.CheckListId = checkList.CheckListId;
                breedingCheckListResponse.BreedingId = breedingId;
                breedingCheckListResponse.Phase = phase;

                List<BreedingCheckListDetailResponse> breedingCheckListDetails = new List<BreedingCheckListDetailResponse>();
                foreach (var item in checkList.CheckListDetails)
                {
                    BreedingCheckListDetailResponse breedingCheckListDetailResponse = new BreedingCheckListDetailResponse();
                    //breedingCheckListDetailResponse.BreedingCheckListId = 0;
                    breedingCheckListDetailResponse.CheckListDetailResponse = _mapper.Map<CheckListDetailResponse>(item);
                    breedingCheckListDetails.Add(breedingCheckListDetailResponse);
                }

                breedingCheckListResponse.BreedingCheckListDetails = breedingCheckListDetails;
                return breedingCheckListResponse;
            }

            return ConvertToResponse(breedingCheckList);
        }

        public async Task<int> CreateBreedingCheckList(BreedingCheckListAddRequest breedingCheckListAddRequest, int phase)
        {
            using(var transaction = _breedingCheckListRepository.BeginTransaction())
            {
                try
                {
                    var breedingCheckList = await _breedingCheckListRepository
                        .GetTodayCheckListByBreedingIdAndPhase(breedingCheckListAddRequest.BreedingId, phase);
                    if(breedingCheckList == null)
                    {
                        breedingCheckList = _mapper.Map<BreedingCheckList>(breedingCheckListAddRequest);
                        if (breedingCheckList == null)
                        {
                            return -1;
                        }

                        breedingCheckList.CreateDate = DateTime.Today;
                        breedingCheckList.Phase = phase;
                        await _breedingCheckListRepository.AddAsync(breedingCheckList);
                        _breedingCheckListRepository.SaveChanges();

                        var checkListDetails = await _checkListDetailRepository.GetCheckListDetailByCheckListId(breedingCheckListAddRequest.CheckListId);
                        if(!checkListDetails.Any())
                        {
                            return -1;
                        }

                        foreach (var item in checkListDetails)
                        {
                            var breedingCheckListDetail = new BreedingCheckListDetail()
                            {
                                BreedingCheckListId = breedingCheckList.BreedingCheckListId,
                                CheckListDetailId = item.CheckListDetailId,
                                CheckValue = 0
                            };
                            var breedingCheckListAddRequestDetail = breedingCheckListAddRequest.BreedingCheckListAddRequestDetails
                                .Where(bca => bca.CheckListDetailId == item.CheckListDetailId).FirstOrDefault();
                            if(breedingCheckListAddRequestDetail != null)
                            {
                                breedingCheckListDetail.CheckValue = breedingCheckListAddRequestDetail.CheckValue;
                            }
                            await _breedingCheckListDetailRepository.AddAsync(breedingCheckListDetail);
                        }
                    }
                    else
                    {
                        foreach (var item in breedingCheckListAddRequest.BreedingCheckListAddRequestDetails)
                        {
                            var breedingCheckListDetail = await _breedingCheckListDetailRepository
                                .GetBreedingCheckListDetailByBreedingCheckListIdAndCheckListDetailId
                                (breedingCheckList.BreedingCheckListId, item.CheckListDetailId);
                            if(breedingCheckListDetail != null)
                            {
                                breedingCheckListDetail.CheckValue = item.CheckValue;
                            }
                        }
                    }

                    _breedingCheckListDetailRepository.SaveChanges();
                    transaction.Commit();
                    return breedingCheckList.BreedingCheckListId;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                    return -1;
                }
            }
        }

        public async Task<BreedingCheckListResponse?> GetTodayClutchCheckListDetail(int clutchId, int phase)
        {
            var breedingCheckList = await _breedingCheckListRepository.GetTodayCheckListByClutchIdAndPhase(clutchId, phase);
            if (breedingCheckList == null)
            {
                var breedingCheckListResponse = new BreedingCheckListResponse();
                var checkList = await _checkListRepository.GetCheckListByPhase(phase);
                if (checkList == null)
                {
                    return null;
                }
                breedingCheckListResponse.CheckListId = checkList.CheckListId;
                breedingCheckListResponse.ClutchId = clutchId;
                breedingCheckListResponse.Phase = phase;

                List<BreedingCheckListDetailResponse> breedingCheckListDetails = new List<BreedingCheckListDetailResponse>();
                foreach (var item in checkList.CheckListDetails)
                {
                    BreedingCheckListDetailResponse breedingCheckListDetailResponse = new BreedingCheckListDetailResponse();
                    //breedingCheckListDetailResponse.BreedingCheckListId = 0;
                    breedingCheckListDetailResponse.CheckListDetailResponse = _mapper.Map<CheckListDetailResponse>(item);
                    breedingCheckListDetails.Add(breedingCheckListDetailResponse);
                }

                breedingCheckListResponse.BreedingCheckListDetails = breedingCheckListDetails;
                return breedingCheckListResponse;
            }

            return ConvertToResponse(breedingCheckList);
        }

        public async Task<int> CreateClutchCheckList(ClutchCheckListAddRequest clutchCheckListAddRequest, int phase, int breedingId)
        {
            using (var transaction = _breedingCheckListRepository.BeginTransaction())
            {
                try
                {
                    var breedingCheckList = await _breedingCheckListRepository
                        .GetTodayCheckListByClutchIdAndPhase(clutchCheckListAddRequest.ClutchId, phase);
                    if (breedingCheckList == null)
                    {
                        breedingCheckList = _mapper.Map<BreedingCheckList>(clutchCheckListAddRequest);
                        if (breedingCheckList == null)
                        {
                            return -1;
                        }

                        breedingCheckList.BreedingId = breedingId;
                        breedingCheckList.CreateDate = DateTime.Today;
                        breedingCheckList.Phase = phase;
                        await _breedingCheckListRepository.AddAsync(breedingCheckList);
                        _breedingCheckListRepository.SaveChanges();

                        var checkListDetails = await _checkListDetailRepository.GetCheckListDetailByCheckListId(clutchCheckListAddRequest.CheckListId);
                        if (!checkListDetails.Any())
                        {
                            return -1;
                        }

                        foreach (var item in checkListDetails)
                        {
                            var breedingCheckListDetail = new BreedingCheckListDetail()
                            {
                                BreedingCheckListId = breedingCheckList.BreedingCheckListId,
                                CheckListDetailId = item.CheckListDetailId,
                                CheckValue = 0
                            };
                            var breedingCheckListAddRequestDetail = clutchCheckListAddRequest.BreedingCheckListAddRequestDetails
                                .Where(bca => bca.CheckListDetailId == item.CheckListDetailId).FirstOrDefault();
                            if (breedingCheckListAddRequestDetail != null)
                            {
                                breedingCheckListDetail.CheckValue = breedingCheckListAddRequestDetail.CheckValue;
                            }
                            await _breedingCheckListDetailRepository.AddAsync(breedingCheckListDetail);
                        }
                    }
                    else
                    {
                        foreach (var item in clutchCheckListAddRequest.BreedingCheckListAddRequestDetails)
                        {
                            var breedingCheckListDetail = await _breedingCheckListDetailRepository
                                .GetBreedingCheckListDetailByBreedingCheckListIdAndCheckListDetailId
                                (breedingCheckList.BreedingCheckListId, item.CheckListDetailId);
                            if (breedingCheckListDetail != null)
                            {
                                breedingCheckListDetail.CheckValue = item.CheckValue;
                            }
                        }
                    }

                    _breedingCheckListDetailRepository.SaveChanges();
                    transaction.Commit();
                    return breedingCheckList.BreedingCheckListId;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                    return -1;
                }
            }
        }

        private BreedingCheckListResponse ConvertToResponse(BreedingCheckList breedingCheckList)
        {
            var breedingCheckListResponse = _mapper.Map<BreedingCheckListResponse>(breedingCheckList);
            List<BreedingCheckListDetailResponse> list = new List<BreedingCheckListDetailResponse>();
            foreach (var item in breedingCheckList.BreedingCheckListDetails)
            {
                var breedingCheckListDetailResponse = _mapper.Map<BreedingCheckListDetailResponse>(item);
                breedingCheckListDetailResponse.CheckListDetailResponse = _mapper.Map<CheckListDetailResponse>(item.CheckListDetail);
                list.Add(breedingCheckListDetailResponse);
            }

            breedingCheckListResponse.BreedingCheckListDetails = list;
            return breedingCheckListResponse;
        }
    }
}