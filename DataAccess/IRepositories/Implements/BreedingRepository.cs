using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class BreedingRepository : GenericRepository<Breeding>, IBreedingRepository
    {
        public BreedingRepository(BFRS_dbContext context) : base(context)
        {
        }
    }
}
