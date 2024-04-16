using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
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
        Task<int> CreateIssueAsync(IssueAddRequest issueAddRequest, int accountId);
        Task<bool> UpdateProcessNoteIssue(int issueId, IssueUpdateRequest issueUpdateRequest, int accountId);
        void UpdateIssue(Issue issue);
        void DeleteIssue(Issue issue);
        void DeleteIssueById(object issueId);
        Task<IEnumerable<IssueResponse>> GetAllIssuesAsync();
        Task<IssueResponse> GetIssueByIdAsync(int issueId);
    }
}
