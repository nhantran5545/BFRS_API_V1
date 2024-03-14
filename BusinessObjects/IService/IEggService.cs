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
        Task<IEnumerable<Egg>> GetAllEggsAsync();
        Task<IEnumerable<Egg>> GetAllEggsByClutchIdAsync(object clutchId);
        Task<Egg?> GetEggByIdAsync(object eggId);
        Task<Egg?> GetEggByBirdIdAsync(object birdId);
    }
}
