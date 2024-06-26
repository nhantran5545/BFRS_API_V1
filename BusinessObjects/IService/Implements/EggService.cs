﻿using AutoMapper;
using Azure.Storage.Blobs.Models;
using BusinessObjects.RequestModels.EggReqModels;
using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using DataAccess.IRepositories.Implements;
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
        private readonly IStatusChangeService _statusChangeService;
        private readonly IMapper _mapper;

        public EggService(IEggRepository eggRepository,  IMapper mapper, IClutchRepository clutchRepository, 
            IStatusChangeService statusChangeService)
        {
            _eggRepository = eggRepository;
            _mapper = mapper;
            _clutchRepository = clutchRepository;
            _statusChangeService = statusChangeService;
        }

        public async Task<int> CreateEggAsync(EggAddRequest eggAddRequest, int accountId)
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
                        await _statusChangeService.AddClutchChangeStatus(clutch.ClutchId, null, accountId, "Created", "Hatched");
                    }
                    else if (clutch.Status == "Weaned" && egg.Status == "In Development")
                    {
                        clutch.Status = "Banding";
                        clutch.Phase = 3;
                        _clutchRepository.SaveChanges();
                        await _statusChangeService.AddClutchChangeStatus(clutch.ClutchId, null, accountId, "Weaned", "Banding");
                    }

                    egg.CreatedBy = accountId;
                    egg.CreatedDate = DateTime.Now;
                    await _eggRepository.AddAsync(egg);
                    _eggRepository.SaveChanges();

                    await _statusChangeService.AddEggChangeStatus(egg.EggId, null, accountId, null, eggAddRequest.Status);
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
        public async Task<int> GetTotalEggCountByStaffId(int staffId)
        {
            var eggs = await _eggRepository.GetEggsByStaffId(staffId);
            return eggs.Count();
        }

        public async Task<int> GetEggCountByStatusNameAndManagedByStaff(string status, int staffId)
        {
            return await _eggRepository.GetEggCountByStatusNameAndManagedByStaff(status, staffId);
        }

        public async Task<int> GetTotalEggsByManagerId(int accountId)
        {
            return await _eggRepository.GetTotalEggsCountByAccountId(accountId);
        }

        public async Task<bool> UpdateEgg(EggUpdateRequest eggUpdateRequest, int accountId)
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

                    var oldStatus = egg.Status;
                    egg.Status = eggUpdateRequest.Status;
                    egg.UpdatedBy = accountId;
                    egg.UpdatedDate = DateTime.Now;

                    _eggRepository.SaveChanges();

                    await _statusChangeService.AddEggChangeStatus(egg.EggId, null, accountId, oldStatus, eggUpdateRequest.Status);
                    await UpdateClutchStatus(egg.ClutchId, accountId);
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

        public async Task<bool> EggHatched(EggHatchRequest eggHatchRequest, int accountId)
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
                        await _statusChangeService.AddClutchChangeStatus(clutch.ClutchId, null, accountId, "Hatched", "Banding");
                    }

                    var oldStatus = egg.Status;
                    egg.HatchedDate = eggHatchRequest.HatchedDate;
                    egg.Status = "Hatched";
                    egg.HatchedDate = DateTime.Now;
                    egg.UpdatedBy = accountId;
                    egg.UpdatedDate = DateTime.Now;

                    _eggRepository.SaveChanges();

                    await _statusChangeService.AddEggChangeStatus(egg.EggId, null, accountId, oldStatus, "Hatched");
                    await UpdateClutchStatus(egg.ClutchId, accountId);
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

        private async Task UpdateClutchStatus(int clutchId, int accountId)
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
                    if (clutch != null && clutch.Status != "Weaned")
                    {
                        var oldStatus = clutch.Status;
                        clutch.Status = "Weaned";
                        clutch.Phase = 4;
                        _clutchRepository.SaveChanges();
                        await _statusChangeService.AddClutchChangeStatus(clutch.ClutchId, null, accountId, oldStatus, "Weaned");
                    }
                }
                else
                {
                    var clutch = await _clutchRepository.GetByIdAsync(clutchId);
                    if (clutch != null && clutch.Status != "Banding")
                    {
                        var oldStatus = clutch.Status;
                        clutch.Status = "Banding";
                        clutch.Phase = 3;
                        _clutchRepository.SaveChanges();
                        await _statusChangeService.AddClutchChangeStatus(clutch.ClutchId, null, accountId, oldStatus, "Banding");
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
