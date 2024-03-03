using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IIssueService
    {
        Task CreateIssueAsync(Issue issue);
        void UpdateIssue(Issue issue);
        void DeleteIssue(Issue issue);
        void DeleteIssueById(object issueId);
        Task<IEnumerable<Issue>> GetAllIssuesAsync();
        Task<Issue?> GetIssueByIdAsync(object issueId);
    }
}
