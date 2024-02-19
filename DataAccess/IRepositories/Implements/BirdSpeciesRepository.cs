using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    internal class BirdSpeciesRepository : GenericRepository<BirdSpecy>, IBirdSpeciesRepository
    {
        public BirdSpeciesRepository(BFRS_dbContext context) : base(context)
        {
        }
    }
}
