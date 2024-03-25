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
    public interface IEggService
    {
        Task<int> CreateEggAsync(EggAddRequest eggAddRequest);
        Task<bool> UpdateEgg(EggUpdateRequest eggUpdateRequest);
        Task<bool> EggHatched(EggHatchRequest eggHatchRequest);
        void DeleteEgg(Egg egg);
        void DeleteEggById(object eggId);
        Task<IEnumerable<EggResponse>> GetAllEggsAsync();
        Task<IEnumerable<EggResponse>> GetEggsByClutchIdAsync(object clutchId);
        Task<EggResponse?> GetEggByIdAsync(object eggId);
        Task<EggResponse?> GetEggByBirdIdAsync(object birdId);
    }
}
