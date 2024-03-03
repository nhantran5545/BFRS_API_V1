﻿using DataAccess.IRepositories;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    public class CageService : ICageService
    {
        private readonly ICageRepository _cageRepository;

        public CageService(ICageRepository cageRepository)
        {
            _cageRepository = cageRepository;
        }

        public Task CreateCageAsync(Cage cage)
        {
            throw new NotImplementedException();
        }

        public void DeleteCage(Cage cage)
        {
            throw new NotImplementedException();
        }

        public void DeleteCageById(object cageId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Cage>> GetAllCagesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Cage?> GetCageByIdAsync(object cageId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCage(Cage cage)
        {
            throw new NotImplementedException();
        }
    }
}
