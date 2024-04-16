using BusinessObjects.RequestModels.EggReqModels;
using BusinessObjects.ResponseModels;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IEggService
    {
        Task<int> CreateEggAsync(EggAddRequest eggAddRequest, int accountId);
        Task<bool> UpdateEgg(EggUpdateRequest eggUpdateRequest, int accountId);
        Task<bool> EggHatched(EggHatchRequest eggHatchRequest, int accountId);
        Task<IEnumerable<EggResponse>> GetAllEggsAsync();
        Task<IEnumerable<EggResponse>> GetEggsByClutchIdAsync(object clutchId);
        Task<IEnumerable<EggResponse>> GetEggsByBreedingIdAsync(object breedingId);
        Task<EggResponse?> GetEggByIdAsync(object eggId);
        Task<EggResponse?> GetEggByBirdIdAsync(object birdId);
        Task<int> GetTotalEggCountByStaffId(int staffId);
        Task<int> GetEggCountByStatusNameAndManagedByStaff(string status, int staffId);
    }
}
