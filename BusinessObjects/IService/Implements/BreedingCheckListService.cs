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
    public class BreedingCheckListService : IBreedingCheckListService
    {
        private readonly IBreedingCheckListRepository _breedingCheckListRepository;
        private readonly IMapper _mapper;

        public BreedingCheckListService(IBreedingCheckListRepository breedingCheckListRepository, IMapper mapper)
        {
            _breedingCheckListRepository = breedingCheckListRepository;
            _mapper = mapper;
        }

        public async Task<List<BreedingCheckListResponse>> GetBreedingCheckListDetailsByBreedingId(int breedingId)
        {
            var breedingCheckListDetails = await _breedingCheckListRepository.GetBreedingCheckListDetailsByBreedingId(breedingId);

            return breedingCheckListDetails.Select(bcl => new BreedingCheckListResponse
            {
                CheckListName = bcl.CheckList.CheckListName,
                Phase = bcl.Phase,
                QuestionName = bcl.BreedingCheckListDetails.Select(detail => detail.CheckListDetail.QuestionName).FirstOrDefault(),
                Compulsory = bcl.BreedingCheckListDetails.Select(detail => detail.CheckListDetail.Compulsory ?? false).FirstOrDefault(),
                Positive = bcl.BreedingCheckListDetails.Select(detail => detail.CheckListDetail.Positive ?? false).FirstOrDefault(),
                Priority = bcl.BreedingCheckListDetails.Select(detail => detail.CheckListDetail.Priority ?? 0).FirstOrDefault()
            }).ToList();
        }


    }
}
