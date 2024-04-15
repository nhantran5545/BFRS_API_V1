using AutoMapper;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using DataAccess.IRepositories.Implements;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    public class IssueTypeService : IIssueTypeService
    {
        private readonly IIssueTypeRepository _issueTypeRepository;
        private readonly IBreedingRepository _breedingRepository;
        private readonly IMapper _mapper;
        public IssueTypeService(IIssueTypeRepository issueTypeRepository, IMapper mapper)
        {
            _issueTypeRepository = issueTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IssueTypeResponse>> GetAllIssuesTypeAsync()
        {
            var issueTypes = await _issueTypeRepository.GetAllAsync();
            return issueTypes.Select(b => _mapper.Map<IssueTypeResponse>(b));
        }
        public async Task<int> CreateIssueTypeAsync(IssueTypeRequest issueAddRequest)
        {
            var issue = _mapper.Map<IssueType>(issueAddRequest);
            if (issue == null)
            {
                return -1;
            }
            issue.IssueName = issueAddRequest.IssueName;
            issue.Description = issueAddRequest.Description;
            issue.Status = "Active";
            await _issueTypeRepository.AddAsync(issue);
            _issueTypeRepository.SaveChanges();
            return issue.IssueTypeId;
        }

    }
}
