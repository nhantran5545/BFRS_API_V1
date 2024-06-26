﻿using BusinessObjects.RequestModels.CageReqModels;
using BusinessObjects.ResponseModels.CageResModels;
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
        Task CreateCageAsync(CageAddRequest request);
        Task<bool> UpdateCageAsync(int cageId, CageUpdateRequest request);
        Task<IEnumerable<CageResponse>> GetAllCagesAsync();
        Task<IEnumerable<CageResponse>> GetCagesByStaffIdAsync(int staffAccountId);
        Task<int> GetTotalCagesByStaffIdAsync(int accountId);
        Task<int> GetTotalCagesStatusByFarmIdAsync(string status, int farmId);
        Dictionary<string, int> GetCageCountByAreaAndFarm(int farmId);
        Task<IEnumerable<CageResponse>> GetCagesByFarmIdAsync(int farmId);
        Task<IEnumerable<CageResponse>> GetCagesForBreeding(int fatherBirdId, int motherBirdId, int farmId);
        Task<IEnumerable<CageResponse>> GetCagesForBreeding(int farmId);
        Task<CageDetailResponse?> GetCageByIdAsync(object cageId);
        List<Dictionary<string, object>> GetTotalCageByFarm();

    }
}
