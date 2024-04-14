using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class EggStatusChangeRepository : GenericRepository<EggStatusChange>, IEggStatusChangeRepository
    {
        public EggStatusChangeRepository(BFRS_DBContext context) : base(context)
        {
        }
    }
}
