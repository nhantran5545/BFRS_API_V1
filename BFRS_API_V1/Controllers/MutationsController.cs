using BusinessObjects.IService;
using BusinessObjects.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MutationsController : ControllerBase
    {
        private readonly IMutationService _mutationService;
        private readonly IAccountService _accountService;
        public MutationsController(IMutationService mutationService, IAccountService accountService)
        {
            _mutationService = mutationService;
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var mutations = await _mutationService.GetAllMutationsAsync();
            if (!mutations.Any())
            {
                return NotFound("There are no mutations");
            }
            return Ok(mutations);
        }

        [HttpGet("{mutationId}")]
        [Authorize]
        public async Task<IActionResult> GetById(int mutationId)
        {
            var mutation = await _mutationService.GetMutationByIdAsync(mutationId);
            if (mutation == null)
            {
                return NotFound("Mutation not found");
            }
            return Ok(mutation);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateMutation(MutationRequest mutationRequest)
        {
            var result = await _mutationService.CreateMutationAsync(mutationRequest);
            if(result < 1)
            {
                return BadRequest("Something is wrong with the server, please try again");
            }

            var mutation = await _mutationService.GetMutationByIdAsync(result);
            return Ok(mutation);
        }
    }
}
