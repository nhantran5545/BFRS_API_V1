using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IIssueTypeService
    {
        Task<IEnumerable<IssueTypeResponse>> GetAllIssuesTypeAsync();
        Task<int> CreateIssueTypeAsync(IssueTypeRequest issueAddRequest);
    }
}
