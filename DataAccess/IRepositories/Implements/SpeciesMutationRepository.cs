using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class SpeciesMutationRepository : GenericRepository<SpeciesMutation>, ISpeciesMutationRepository
    {
        public SpeciesMutationRepository(BFRS_DBContext context) : base(context)
        {
        }
    }
}
