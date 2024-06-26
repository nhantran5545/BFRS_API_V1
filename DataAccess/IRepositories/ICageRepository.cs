﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface ICageRepository : IGenericRepository<Cage>
    {
        Task<IEnumerable<Cage>> GetAllCagesAsync();
        Task<IEnumerable<Cage>> GetCagesManagedByStaffAsync(int accountId);
        Task<IEnumerable<Cage>> GetStandbyCagesByFarmId(int farmId);
        Task<IEnumerable<Cage>> GetCagesByFarmIdAsync(int farmId);
        Task<int> GetTotalCageStatusByFarmIdAsync(int farmId, string status);
        Dictionary<string, int> GetCageCountByAreaAndFarm(int farmId);
        List<Dictionary<string, object>> GetTotalCageByFarm();
    }
}
