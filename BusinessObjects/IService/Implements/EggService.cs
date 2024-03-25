using AutoMapper;
using BusinessObjects.RequestModels;
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
        private readonly IClutchRepository _clutchRepository;
        private readonly IMapper _mapper;

        public EggService(IEggRepository eggRepository,  IMapper mapper, IClutchRepository clutchRepository)
        {
            _eggRepository = eggRepository;
            _mapper = mapper;
            _clutchRepository = clutchRepository;
        }

        public async Task<int> CreateEggAsync(EggAddRequest eggAddRequest)
        {
            var egg = _mapper.Map<Egg>(eggAddRequest);
            if(egg == null)
            {
                return -1;
            }

            var clutch = await _clutchRepository.GetByIdAsync(eggAddRequest.ClutchId);
            if(clutch == null)
            {
                return -1;
            }
            if(egg.Status == "InDevelopment")
            {
                clutch.Status = "Hatched";
            }

            egg.CreatedDate = DateTime.Now;
            await _eggRepository.AddAsync(egg);
            var result = _eggRepository.SaveChanges();
            if(result < 1)
            {
                return result;
            }
            return egg.EggId;
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

        public async Task<IEnumerable<EggResponse>> GetEggsByClutchIdAsync(object clutchId)
        {
            var eggs = await _eggRepository.GetEggsByClutchIdAsync(clutchId);
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

        public async Task<int> UpdateEgg(EggUpdateRequest eggUpdateRequest)
        {
            var egg = await _eggRepository.GetByIdAsync(eggUpdateRequest.EggId);
            if (egg == null)
            {
                return -1;
            }

            egg.Status = eggUpdateRequest.Status;
            egg.UpdatedBy = eggUpdateRequest.UpdatedBy;
            egg.UpdatedDate = DateTime.Now;

            var result = _eggRepository.SaveChanges();
            if (result < 1)
            {
                return result;
            }
            return egg.EggId;
        }

        public async Task<int> EggHatched(EggUpdateRequest eggUpdateRequest)
        {
            var egg = await _eggRepository.GetByIdAsync(eggUpdateRequest.EggId);
            if (egg == null || egg.ClutchId == null)
            {
                return -1;
            }

            var clutch = await _clutchRepository.GetByIdAsync(egg.ClutchId);
            if (clutch == null)
            {
                return -1;
            }
            else
            {
                clutch.Status = "Banding";
            }

            if (eggUpdateRequest.HatchedDate != null)
            {
                egg.HatchedDate = eggUpdateRequest.HatchedDate;
            }

            egg.Status = "Hatched";
            egg.UpdatedBy = eggUpdateRequest.UpdatedBy;
            egg.UpdatedDate = DateTime.Now;

            var result = _eggRepository.SaveChanges();
            if (result < 1)
            {
                return result;
            }
            return egg.EggId;
        }
    }
}
