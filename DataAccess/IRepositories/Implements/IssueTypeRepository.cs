﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class IssueTypeRepository : GenericRepository<IssueType>, IIssueTypeRepository
    {
        public IssueTypeRepository(BFRS_DBContext context) : base(context)
        {
        }
    }
}
