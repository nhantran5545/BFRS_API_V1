﻿using AutoMapper;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
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
            issue.Status = "Active";
            await _issueRepository.AddAsync(issue);
            _issueRepository.SaveChanges();
            return issue.IssueId;
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
