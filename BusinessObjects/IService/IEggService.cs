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
        void UpdateEgg(Egg checkList);
        void DeleteEgg(Egg checkList);
        void DeleteEggById(object checkListId);
        Task<IEnumerable<Egg>> GetAllEggsAsync();
        Task<IEnumerable<Egg>> GetAllEggsByClutchIdAsync(object clutchId);
        Task<Egg?> GetEggByIdAsync(object eggId);
        Task<Egg?> GetEggByBirdIdAsync(object birdId);
    }
}
