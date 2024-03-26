using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
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
        Task<int> CreateBreeding(BreedingAddRequest breedingAddRequest);
        void UpdateBreeding(BreedingAddRequest breedingAddRequest);
        Task<bool> PutBirdsToBreeding(BreedingUpdateRequest breedingUpdateRequest);
        Task<bool> BreedingInProgress(BreedingUpdateRequest breedingUpdateRequest);
        Task<bool> CloseBreeding(BreedingCloseRequest breedingCloseRequest);
        void DeleteBreeding(BreedingAddRequest breeding);
        void DeleteBreedingById(object breedingId);
        Task<IEnumerable<BreedingResponse>> GetAllBreedings();
        Task<IEnumerable<BreedingResponse>> GetAllBreedingsByManagerId(object managerId);
        Task<BreedingDetailResponse?> GetBreedingById(object breedingId);
    }
}
