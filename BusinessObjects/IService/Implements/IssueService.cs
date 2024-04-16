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
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IBreedingRepository _breedingRepository;
        private readonly IMapper _mapper;
        public IssueService(IIssueRepository issueRepository, IMapper mapper, IBreedingRepository breedingRepository)
        {
            _issueRepository = issueRepository;
            _mapper = mapper;
            _breedingRepository = breedingRepository;
        }

        public async Task<int> CreateIssueAsync(IssueAddRequest issueAddRequest, int accountId)
        {
            var issue = _mapper.Map<Issue>(issueAddRequest);
            if (issue == null)
            {
                return -1;
            }

            var breeding = await _breedingRepository.GetByIdAsync(issueAddRequest.BreedingId);
            if (breeding == null)
            {
                return -1;
            }
            issue.IssueName = issueAddRequest.IssueName;
            issue.BreedingId = issueAddRequest.BreedingId;
            issue.Description = issueAddRequest.Description;
            issue.IssueTypeId = issueAddRequest.IssueTypeId;
            issue.CreatedBy = accountId;
            issue.CreatedDate = DateTime.Now;
            issue.AssignedTo = breeding.CreatedBy;
            issue.Status = "InProgress";
            await _issueRepository.AddAsync(issue);
            _issueRepository.SaveChanges();
            return issue.IssueId;
        }

        public async Task<bool> UpdateProcessNoteIssue(int issueId, IssueUpdateRequest issueUpdateRequest, int accountId)
        {
            var issue = await _issueRepository.GetByIdAsync(issueId);
            if (issue == null)
            {
                return false;
            }
            if (issueUpdateRequest.Status != "Ignore" && issueUpdateRequest.Status != "Processed")
            {
                return false;
            }

            issue.ProcessNote = issueUpdateRequest.ProcessNote;
            issue.Status = issueUpdateRequest.Status;
            issue.UpdatedBy = accountId;
            issue.UpdatedDate = DateTime.Now;

            _issueRepository.Update(issue);
            var result = _issueRepository.SaveChanges();
            if (result < 1)
            {
                return false;
            }
            return true;
        }

        public void DeleteIssue(Issue issue)
        {
            throw new NotImplementedException();
        }

        public void DeleteIssueById(object issueId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IssueResponse>> GetAllIssuesAsync()
        {
            var issues = await _issueRepository.GetAllAsync();
            return issues.Select(b => _mapper.Map<IssueResponse>(b));
        }

        public async Task<IssueResponse> GetIssueByIdAsync(int issueId)
        {
            var issue = await _issueRepository.GetByIdAsync(issueId);
            return _mapper.Map<IssueResponse>(issue);
        }

        public void UpdateIssue(Issue issue)
        {
            throw new NotImplementedException();
        }
    }
}
