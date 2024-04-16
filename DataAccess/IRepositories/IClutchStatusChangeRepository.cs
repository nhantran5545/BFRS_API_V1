using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IClutchStatusChangeRepository : IGenericRepository<ClutchStatusChange>
    {
        Task<IEnumerable<ClutchStatusChange>> GetTimelineByClutchIdAsync(object clutchId);
    }
}
