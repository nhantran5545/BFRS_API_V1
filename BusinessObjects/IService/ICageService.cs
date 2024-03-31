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
    public interface ICageService
    {
        void DeleteCage(Cage cage);
        void DeleteCageById(object cageId);
        Task CreateCageAsync(CageAddRequest request);
        Task<bool> UpdateCageAsync(int cageId, CageUpdateRequest request);
        Task<IEnumerable<CageResponse>> GetAllCagesAsync();
        Task<IEnumerable<CageResponse>> GetCagesForBreeding(int fatherBirdId, int motherBirdId, int farmId);
        Task<IEnumerable<CageResponse>> GetCagesForBreeding(int farmId);
        Task<CageDetailResponse?> GetCageByIdAsync(object cageId);

    }
}
