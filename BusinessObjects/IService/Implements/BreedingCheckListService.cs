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

        public BreedingCheckListService(IBreedingCheckListRepository repository)
        {
            _repository = repository;
        }

        public BreedingCheckListResponse GetBreedingCheckList(int breedingId, int phase)
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
        }

    }

}
