﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class BirdMutationRepository : GenericRepository<BirdMutation>, IBirdMutationRepository
    {
        public BirdMutationRepository(BFRS_DBContext context) : base(context)
        {
        }
    }
}