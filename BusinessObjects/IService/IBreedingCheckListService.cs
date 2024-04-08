using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IBreedingCheckListService
    {
        //BreedingCheckListResponse GetBreedingCheckList(int breedingId, int phase);
        Task<IEnumerable<BreedingCheckListResponse>> GetBreedingCheckListsAsync();
        Task<IEnumerable<BreedingCheckListResponse>> GetBreedingCheckListsByBreedingId(int breedingId);
        Task<IEnumerable<BreedingCheckListResponse>> GetBreedingCheckListsByBreedingIdAndPhase(int breedingId, int phase);
        Task<IEnumerable<BreedingCheckListResponse>> GetBreedingCheckListsByClutchIdAndPhase(int clutchId, int phase);
        Task<BreedingCheckListResponse?> GetBreedingCheckListDetail(int breedingCheckListId);
        Task<BreedingCheckListResponse?> GetTodayBreedingCheckListDetail(int breedingId, int phase);
        Task<BreedingCheckListResponse?> GetTodayClutchCheckListDetail(int clutchId, int phase);
        Task<int> CreateBreedingCheckList(BreedingCheckListAddRequest breedingCheckListAddRequest, int phase);
        Task<int> CreateClutchCheckList(ClutchCheckListAddRequest clutchCheckListAddRequest, int phase, int breedingId);
    }

}
