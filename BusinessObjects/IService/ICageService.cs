﻿using BusinessObjects.ResponseModels;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface ICageService
    {
        Task CreateCageAsync(Cage cage);
        void UpdateCage(Cage cage);
        void DeleteCage(Cage cage);
        void DeleteCageById(object cageId);
        Task<IEnumerable<CageResponse>> GetAllCagesAsync();
        Task<IEnumerable<CageResponse>> GetCagesForBreeding(int fatherBirdId, int motherBirdId, int farmId);
        Task<CageDetailResponse?> GetCageByIdAsync(object cageId);
    }
}
