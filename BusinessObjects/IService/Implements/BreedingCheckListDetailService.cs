using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    public class BreedingCheckListDetailService : IBreedingCheckListDetailService
    {
        private readonly IBreedingCheckListDetailRepository _breedingCheckListDetailRepository;

        public BreedingCheckListDetailService(IBreedingCheckListDetailRepository breedingCheckListDetailRepository)
        {
            _breedingCheckListDetailRepository = breedingCheckListDetailRepository ?? throw new ArgumentNullException(nameof(breedingCheckListDetailRepository));
        }

        public async Task<List<CheckListDetailResponse>> GetCheckListDetailsByBreedingId(int breedingId)
        {
            if (breedingId <= 0)
            {
                throw new ArgumentException("Invalid BreedingId. BreedingId must be greater than 0.", nameof(breedingId));
            }

            var breedingCheckListDetails = await _breedingCheckListDetailRepository.GetCheckListDetailsByBreedingId(breedingId);

            if (breedingCheckListDetails == null || !breedingCheckListDetails.Any())
            {
                throw new Exception($"No CheckListDetails found for BreedingId: {breedingId}");
            }

            return breedingCheckListDetails.Select(c => new CheckListDetailResponse
            {
                QuestionName = c.CheckListDetail.QuestionName,
                Compulsory = c.CheckListDetail.Compulsory ?? false,
                Positive = c.CheckListDetail.Positive ?? false,
                Priority = c.CheckListDetail.Priority ?? 0,
                Status = c.CheckListDetail.Status
            }).ToList();
        }
    }
}
