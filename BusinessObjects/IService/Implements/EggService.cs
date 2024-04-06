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
        private readonly IBreedingRepository _breedingRepository;
        private readonly IMapper _mapper;

        public EggService(IEggRepository eggRepository,  IMapper mapper, IClutchRepository clutchRepository, IBreedingRepository breedingRepository)
        {
            _eggRepository = eggRepository;
            _mapper = mapper;
            _clutchRepository = clutchRepository;
            _breedingRepository = breedingRepository;
        }

        public async Task<int> CreateEggAsync(EggAddRequest eggAddRequest)
        {
            using(var transaction = _eggRepository.BeginTransaction())
            {
                try
                {
                    var egg = _mapper.Map<Egg>(eggAddRequest);
                    if (egg == null)
                    {
                        return -1;
                    }

                    var clutch = await _clutchRepository.GetByIdAsync(eggAddRequest.ClutchId);
                    if (clutch == null)
                    {
                        return -1;
                    }
                    if (clutch.Status == "Created")
                    {
                        clutch.Status = "Hatched";
                        clutch.Phase = 3;
                        _clutchRepository.SaveChanges();

                        /*var breeding = await _breedingRepository.GetByIdAsync(clutch.BreedingId);
                        if(breeding != null)
                        {
                            breeding.Phase = 3;
                            _breedingRepository.SaveChanges();
                        }*/
                    }
                    else if (clutch.Status == "Weaned" && egg.Status == "In Development")
                    {
                        clutch.Status = "Banding";
                        clutch.Phase = 3;
                        _clutchRepository.SaveChanges();

                        /*var breeding = await _breedingRepository.GetByIdAsync(clutch.BreedingId);
                        if (breeding != null)
                        {
                            breeding.Phase = 3;
                            _breedingRepository.SaveChanges();
                        }*/
                    }

                    egg.CreatedDate = DateTime.Now;
                    await _eggRepository.AddAsync(egg);
                    _eggRepository.SaveChanges();
                    transaction.Commit();
                    return egg.EggId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    transaction.Rollback();
                    return -1;
                }
            }
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
            return eggs.Select(e => convertToResponse(e));
        }

        public async Task<IEnumerable<EggResponse>> GetEggsByBreedingIdAsync(object breedingId)
        {
            List<Egg> totalEggs = new List<Egg>();
            var clutches = await _clutchRepository.GetClutchsByBreedingId(breedingId);
            if (clutches.Any())
            {                
                foreach (var item in clutches)
                {
                    var eggs = await _eggRepository.GetEggsByClutchIdAsync(item.ClutchId);
                    totalEggs.AddRange(eggs);
                }
            }

            return totalEggs.Select(e => _mapper.Map<EggResponse>(e));
        }

        public async Task<EggResponse?> GetEggByBirdIdAsync(object birdId)
        {
            var egg = await _eggRepository.GetByIdAsync(birdId);
            return _mapper.Map<EggResponse>(egg);
        }

        public async Task<EggResponse?> GetEggByIdAsync(object eggId)
        {
            var egg = await _eggRepository.GetByIdAsync(eggId);
            if(egg == null)
            {
                return null;
            }
            return convertToResponse(egg);
        }

        public async Task<bool> UpdateEgg(EggUpdateRequest eggUpdateRequest)
        {
            using(var transaction = _eggRepository.BeginTransaction())
            {
                try
                {
                    var egg = await _eggRepository.GetByIdAsync(eggUpdateRequest.EggId);
                    if (egg == null)
                    {
                        return false;
                    }

                    egg.Status = eggUpdateRequest.Status;
                    egg.UpdatedBy = eggUpdateRequest.UpdatedBy;
                    egg.UpdatedDate = DateTime.Now;

                    _eggRepository.SaveChanges();
                    await UpdateClutchStatus(egg.ClutchId);
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> EggHatched(EggHatchRequest eggHatchRequest)
        {
            using(var transaction =  _eggRepository.BeginTransaction())
            {
                try
                {
                    var egg = await _eggRepository.GetByIdAsync(eggHatchRequest.EggId);
                    if (egg == null)
                    {
                        return false;
                    }

                    var clutch = await _clutchRepository.GetByIdAsync(egg.ClutchId);
                    if (clutch == null)
                    {
                        return false;
                    }
                    else if(clutch.Status == "Hatched")
                    {
                        clutch.Status = "Banding";
                        _clutchRepository.SaveChanges();
                    }

                    egg.HatchedDate = eggHatchRequest.HatchedDate;
                    egg.Status = "Hatched";
                    egg.HatchedDate = DateTime.Now;
                    egg.UpdatedBy = eggHatchRequest.UpdatedBy;
                    egg.UpdatedDate = DateTime.Now;

                    _eggRepository.SaveChanges();
                    await UpdateClutchStatus(egg.ClutchId);
                    transaction.Commit();
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    transaction.Rollback();
                    return false;
                }
            }
            
        }

        private async Task UpdateClutchStatus(int clutchId)
        {
            var eggs = await _eggRepository.GetEggsByClutchIdAsync(clutchId);
            if (eggs.Any())
            {
                bool flag = true;
                foreach (var item in eggs)
                {
                    if (item.Status == "In Development")
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    var clutch = await _clutchRepository.GetByIdAsync(clutchId);
                    if (clutch != null)
                    {
                        clutch.Status = "Weaned";
                        clutch.Phase = 4;
                        _clutchRepository.SaveChanges();

                        /*var breeding = await _breedingRepository.GetByIdAsync(clutch.BreedingId);
                        if (breeding != null)
                        {
                            breeding.Phase = 4;
                            _breedingRepository.SaveChanges();
                        }*/
                    }
                }
            }
        }

        private EggResponse convertToResponse(Egg egg)
        {
            var eggResponse = _mapper.Map<EggResponse>(egg);

            var eggBird = egg.EggBirds.FirstOrDefault();
            if(eggBird != null && eggBird.Bird != null)
            {
                eggResponse.BandNumber = eggBird.Bird.BandNumber;
            }
            
            return eggResponse;
        }
    }
}
