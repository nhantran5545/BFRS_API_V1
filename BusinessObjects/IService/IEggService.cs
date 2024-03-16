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
        Task CreateEggAsync(Egg egg);
        void UpdateEgg(Egg egg);
        void DeleteEgg(Egg egg);
        void DeleteEggById(object eggId);
        Task<IEnumerable<EggResponse>> GetAllEggsAsync();
        Task<IEnumerable<EggResponse>> GetAllEggsByClutchIdAsync(object clutchId);
        Task<EggResponse?> GetEggByIdAsync(object eggId);
        Task<EggResponse?> GetEggByBirdIdAsync(object birdId);
    }
}
