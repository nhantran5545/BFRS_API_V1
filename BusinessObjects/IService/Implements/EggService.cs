using AutoMapper;
using BusinessObjects.ResponseModels;
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
        private readonly IMapper _mapper;

        public EggService(IEggRepository eggRepository, IMapper mapper)
        {
            _eggRepository = eggRepository;
            _mapper = mapper;
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

        public async Task<IEnumerable<EggResponse>> GetAllEggsAsync()
        {
            var eggs = await _eggRepository.GetAllAsync();
            return eggs.Select(e => _mapper.Map<EggResponse>(e));
        }

        public async Task<IEnumerable<EggResponse>> GetAllEggsByClutchIdAsync(object clutchId)
        {
            var eggs = await _eggRepository.GetAllEggsByClutchIdAsync(clutchId);
            return eggs.Select(e => _mapper.Map<EggResponse>(e));
        }

        public async Task<EggResponse?> GetEggByBirdIdAsync(object birdId)
        {
            var egg = await _eggRepository.GetByIdAsync(birdId);
            return _mapper.Map<EggResponse>(egg);
        }

        public async Task<EggResponse?> GetEggByIdAsync(object eggId)
        {
            var egg = await _eggRepository.GetByIdAsync(eggId);
            return _mapper.Map<EggResponse>(egg);
        }

        public void UpdateEgg(Egg egg)
        {
            throw new NotImplementedException();
        }
    }
}
