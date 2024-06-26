﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface ISpeciesMutationRepository : IGenericRepository<SpeciesMutation>
    {
        Task<IEnumerable<SpeciesMutation>> GetBySpeciesIdAsync(object speciesId);
    }
}
