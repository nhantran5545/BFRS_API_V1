﻿using BusinessObjects.IService;
using BusinessObjects.RequestModels.IssueReqModels;
using BusinessObjects.ResponseModels.IssueResModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueTypesController : ControllerBase
    {

        private readonly IIssueTypeService _issueTypeService;
        private readonly IBreedingService _breedingService;
        private readonly IAccountService _accountService;
        public IssueTypesController(IIssueTypeService issueTypeService, IAccountService accountService)
        {
            _issueTypeService = issueTypeService;
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<IssueTypeResponse>>> GetAllIssueTypes()
        {
            try
            {
                var issue = await _issueTypeService.GetAllIssuesTypeAsync();
                if (issue == null)
                {
                    return NotFound("there are no Issue Type");
                }

                return Ok(issue);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(403, "You are not authorized to access this resource.");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<IssueTypeResponse>> CreateIssue(IssueTypeRequest issueAddRequest)
        {
            var result = await _issueTypeService.CreateIssueTypeAsync(issueAddRequest);
            if (result < 1)
            {
                return BadRequest("Something is wrong with the server, please try again!");
            }
            return Ok(result);
        }
    }
}
