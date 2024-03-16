using AutoMapper;
using DataAccess.IRepositories;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IMapper _mapper;
        public IssueService(IIssueRepository issueRepository, IMapper mapper)
        {
            _issueRepository = issueRepository;
            _mapper = mapper;
        }

        public Task CreateIssueAsync(Issue issue)
        {
            throw new NotImplementedException();
        }

        public void DeleteIssue(Issue issue)
        {
            throw new NotImplementedException();
        }

        public void DeleteIssueById(object issueId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Issue>> GetAllIssuesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Issue?> GetIssueByIdAsync(object issueId)
        {
            throw new NotImplementedException();
        }

        public void UpdateIssue(Issue issue)
        {
            throw new NotImplementedException();
        }
    }
}
