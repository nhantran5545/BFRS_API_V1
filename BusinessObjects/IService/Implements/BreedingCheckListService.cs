using AutoMapper;
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
    public class BreedingCheckListService : IBreedingCheckListService
    {
        private readonly IBreedingCheckListRepository _repository;
        private readonly IMapper _mapper;   

        public BreedingCheckListService(IBreedingCheckListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /*public BreedingCheckListResponse GetBreedingCheckList(int breedingId, int phase)
        {
            var breedingCheckList = _repository.GetBreedingCheckList(breedingId, phase);
            var breedingCheckListDTO = new BreedingCheckListResponse
            {
                BreedingCheckListId = breedingCheckList.BreedingCheckListId,
                BreedingId = breedingCheckList.BreedingId,
                Phase = breedingCheckList.Phase,
                CheckListDetails = breedingCheckList.CheckList.CheckListDetails.Select(cd => new CheckListDetailResponse
                {
                    CheckListDetailId = cd.CheckListDetailId,
                    QuestionName = cd.QuestionName,
                    Compulsory = cd.Compulsory,
                    Positive = cd.Positive,
                    Priority = cd.Priority,
                    CheckValue = breedingCheckList.BreedingCheckListDetails.FirstOrDefault(bcd => bcd.CheckListDetailId == cd.CheckListDetailId)?.CheckValue
                }).ToList()
            };
            return breedingCheckListDTO;
        }*/

        public async Task<IEnumerable<BreedingCheckListResponse>> GetBreedingCheckListsAsync()
        {
            var breedingCheckLists = await _repository.GetAllAsync();
            if(breedingCheckLists == null)
            {
                return Enumerable.Empty<BreedingCheckListResponse>();
            }
            return breedingCheckLists.Select(bc => ConvertToResponse(bc));
        }

        public async Task<IEnumerable<BreedingCheckListResponse>> GetBreedingCheckListsByBreedingId(int breedingId)
        {
            var breedingCheckLists = await _repository.GetBreedingCheckListsByBreedingId(breedingId);
            if (breedingCheckLists == null)
            {
                return Enumerable.Empty<BreedingCheckListResponse>();
            }
            return breedingCheckLists.Select(bc => ConvertToResponse(bc));
        }

        public async Task<IEnumerable<BreedingCheckListResponse>> GetBreedingCheckListsByBreedingIdAndPhase(int breedingId, int phase)
        {
            var breedingCheckLists = await _repository.GetBreedingCheckListsByBreedingIdAndPhase(breedingId, phase);
            if (breedingCheckLists == null)
            {
                return Enumerable.Empty<BreedingCheckListResponse>();
            }
            return breedingCheckLists.Select(bc => ConvertToResponse(bc));
        }

        public async Task<IEnumerable<BreedingCheckListResponse>> GetBreedingCheckListsByClutchIdAndPhase(int clutchId, int phase)
        {
            var breedingCheckLists = await _repository.GetBreedingCheckListsByClutchIdAndPhase(clutchId, phase);
            if (breedingCheckLists == null)
            {
                return Enumerable.Empty<BreedingCheckListResponse>();
            }
            return breedingCheckLists.Select(bc => ConvertToResponse(bc));
        }

        public async Task<BreedingCheckListResponse?> GetBreedingCheckListDetail(int breedingCheckListId)
        {
            var breedingCheckList = await _repository.GetByIdAsync(breedingCheckListId);
            if(breedingCheckList == null)
            {
                return null;
            }

            return ConvertToResponse(breedingCheckList);
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