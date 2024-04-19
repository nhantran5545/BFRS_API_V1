using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IIssueRepository : IGenericRepository<Issue>
    {
        Task<IEnumerable<Issue>> GetIssuesByBreedingAsync(int breedingId);
        Task<IEnumerable<Issue>> GetIssueByStaffId(int staffId);
    }
}
