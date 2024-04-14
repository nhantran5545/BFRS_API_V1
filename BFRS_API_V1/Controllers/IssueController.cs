using BusinessObjects.IService;
using BusinessObjects.IService.Implements;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;
        private readonly IBreedingService _breedingService;
        private readonly IAccountService _accountService;
        public IssueController(IIssueService issueService, IAccountService accountService , IBreedingService breedingService)
        {
            _issueService = issueService;
            _accountService = accountService;
            _breedingService = breedingService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<IssueResponse>>> GetAllIssues()
        {
            try
            {
                var issue = await _issueService.GetAllIssuesAsync();
                if (issue == null)
                {
                    return NotFound("there are no issue");
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
        public async Task<ActionResult<IssueResponse>> CreateIssue(IssueAddRequest issueAddRequest)
        {
            var accountId = _accountService.GetAccountIdFromToken();
            var breeding = await _breedingService.GetBreedingById(issueAddRequest.BreedingId);
            if (breeding == null)
            {
                return BadRequest("Breeding not found");
            }     
            var result = await _issueService.CreateIssueAsync(issueAddRequest, accountId);
            if (result < 1)
            {
                return BadRequest("Something is wrong with the server, please try again!");
            }
            var clutch = await _issueService.GetIssueByIdAsync(result);
            return Ok(clutch);
        }

        [HttpGet("{issueId}")]
        [Authorize]
        public async Task<ActionResult<AreaResponse>> GetAreaById(int issueId)
        {
            var issue = await _issueService.GetIssueByIdAsync(issueId);
            if (issue == null)
            {
                return NotFound("Issue not found");
            }
            return Ok(issue);
        }
    }
}
