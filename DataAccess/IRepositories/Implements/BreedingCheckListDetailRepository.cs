using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class BreedingCheckListDetailRepository : GenericRepository<BreedingCheckListDetail>, IBreedingCheckListDetailRepository
    {
        public BreedingCheckListDetailRepository(BFRS_dbContext context) : base(context)
        {
        }
    }
}
