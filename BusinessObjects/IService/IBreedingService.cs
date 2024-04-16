using BusinessObjects.RequestModels.BreedingReqModels;
using BusinessObjects.ResponseModels.BreedingResModels;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IBreedingService
    {
        Task<double> CalculateInbreedingPercentage(int fatherBirdId, int motherBirdId);
        Task<int> CreateBreeding(BreedingAddRequest breedingAddRequest, int managerId);
        Task<bool> CloseBreeding(BreedingCloseRequest breedingCloseRequest, int managerId);
        Task<bool> CancelBreeding(BreedingUpdateRequest breedingUpdateRequest, int managerId);
        Task<IEnumerable<BreedingResponse>> GetAllBreedings();
        Task<IEnumerable<BreedingResponse>> GetAllBreedingsByManagerId(object managerId);
        Task<IEnumerable<BreedingResponse>> GetBreedingsByStaffIdAsync(int staffId);
        Task<int> GetBreedingCountByStatusNameAndManagedByStaff(int staffId, string status);
        Task<BreedingDetailResponse?> GetBreedingById(object breedingId);
    }
}
