using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class EggReasonRepository : GenericRepository<EggReason>, IEggReasonRepository
    {
        public EggReasonRepository(BFRS_DBContext context) : base(context)
        {
        }
    }
}
