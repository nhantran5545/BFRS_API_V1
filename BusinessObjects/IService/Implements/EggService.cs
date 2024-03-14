using DataAccess.IRepositories;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    public class EggService : IEggService
    {
        private readonly IEggRepository _eggRepository;

        public EggService(IEggRepository eggRepository)
        {
            _eggRepository = eggRepository;
        }

        public async Task CreateEggAsync(Egg egg)
        {
            await _eggRepository.AddAsync(egg);
        }

        public void DeleteEgg(CheckList checkList)
        {
            throw new NotImplementedException();
        }

        public void DeleteEgg(Egg egg)
        {
            throw new NotImplementedException();
        }

        public void DeleteEggById(object checkListId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Egg>> GetAllEggsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Egg>> GetAllEggsByClutchIdAsync(object clutchId)
        {
            throw new NotImplementedException();
        }

        public Task<Egg?> GetEggByBirdIdAsync(object birdId)
        {
            throw new NotImplementedException();
        }

        public Task<Egg?> GetEggByIdAsync(object checkListId)
        {
            throw new NotImplementedException();
        }

        public void UpdateEgg(Egg egg)
        {
            throw new NotImplementedException();
        }
    }
}
