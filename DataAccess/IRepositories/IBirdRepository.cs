﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IBirdRepository : IGenericRepository<Bird>
    {
        Task<IEnumerable<Bird>> GetAllBirdsByFarmId(object farmId);
        Task<IEnumerable<Bird>> GetAllBirdsBySpeciesId(object SpeciesId);
        Task<IEnumerable<Bird>> GetAllInRestBirdsBySpeciesId(object SpeciesId);
    }
}
