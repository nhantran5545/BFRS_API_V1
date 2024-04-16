using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IEggStatusChangeRepository : IGenericRepository<EggStatusChange>
    {
        Task<IEnumerable<EggStatusChange>> GetTimelineByEggIdAsync(object eggId);
    }
}
