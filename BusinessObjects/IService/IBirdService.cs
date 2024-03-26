﻿using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IBirdService
    {
        Task<int> CreateBirdAsync(BirdAddRequest birdAddRequest);
        Task<IEnumerable<BirdResponse>> GetAllBirdsAsync();
        Task<IEnumerable<BirdResponse>> GetBirdsByFarmId(object farmId);
        Task<IEnumerable<BirdResponse>> GetInRestBirdsBySpeciesIdAndFarmId(object speciesId, object farmId);
        Task<BirdDetailResponse?> GetBirdByIdAsync(object birdId);
        Task<BirdDetailResponse?> GetBirdByEggIdAsync(object eggId);
        Task<Dictionary<string, BirdPedi>> GetPedigreeOfABird(int birdId);
    }
}
