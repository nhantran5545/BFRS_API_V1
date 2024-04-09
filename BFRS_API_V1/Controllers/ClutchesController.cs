using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.IService;
using BusinessObjects.ResponseModels;
using BusinessObjects.RequestModels;
using Microsoft.AspNetCore.Authorization;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClutchesController : ControllerBase
    {
        private readonly IClutchService _clutchService;
        private readonly IBreedingService _breedingService;
        private readonly IAccountService _accountService;
        public ClutchesController(IClutchService clutchService, IBreedingService breedingService, IAccountService accountService)
        {
            _breedingService = breedingService;
            _clutchService = clutchService;
            _accountService = accountService;
        }

        // GET: api/Clutches
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ClutchResponse>>> GetClutches()
        {
            var clutches = await _clutchService.GetAllClutchsAsync();
            if(clutches == null)
            {
                return NotFound("Clutches not found!");
            }
            return Ok(clutches);
        }

        [HttpGet("ByBreeding/{breedingId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ClutchResponse>>> GetClutchesByBreedingId(int breedingId)
        {
            var clutches = await _clutchService.GetClutchsByBreedingId(breedingId);
            if (clutches == null)
            {
                return NotFound("Clutches not found!");
            }
            return Ok(clutches);
        }

        [HttpGet("ByCreated")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ClutchResponse>>> GetClutchesByCreatedById()
        {
            var createdById = _accountService.GetAccountIdFromToken();
            var clutches = await _clutchService.GetClutchsByCreatedById(createdById);
            if (clutches == null)
            {
                return NotFound("Clutches not found!");
            }
            return Ok(clutches);
        }

        // GET: api/Clutches/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ClutchDetailResponse>> GetClutch(int id)
        {
            var clutch = await _clutchService.GetClutchByIdAsync(id);
            if (clutch == null)
            {
                return NotFound("Clutch not found");
            }
            return Ok(clutch);
        }

        // POST: api/Clutches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ClutchResponse>> CreateClutch(ClutchAddRequest clutchAddRequest)
        {
            var accountId = _accountService.GetAccountIdFromToken();
            var breeding = await _breedingService.GetBreedingById(clutchAddRequest.BreedingId);
            if (breeding == null)
            {
                return BadRequest("Breeding not found");
            }

            if(breeding.Status != "Mating" && breeding.Status != "InProgress")
            {
                return BadRequest("Breeding already closed");
            }

            if(breeding.ClutchResponses != null && breeding.ClutchResponses.Any())
            {
                foreach (var item in breeding.ClutchResponses)
                {
                    if (item.Status != "Closed" && item.Status != "Eliminated")
                    {
                        return BadRequest("Another clutch is in progress");
                    }
                }
            }
            
            var result = await _clutchService.CreateClutchAsync(clutchAddRequest, accountId);
            if (result < 1)
            {
                return BadRequest("Something is wrong with the server, please try again!");
            }
            var clutch = await _clutchService.GetClutchByIdAsync(result);
            return Ok(clutch);
        }

        [HttpPut("Close/{id}")]
        [Authorize]
        public async Task<IActionResult> CloseClutch(int id, [FromBody] ClutchCloseRequest clutchUpdateRequest)
        {
            var accountId = _accountService.GetAccountIdFromToken();
            if (id != clutchUpdateRequest.ClutchId)
            {
                return BadRequest("clutch id conflict");
            }

            if(clutchUpdateRequest.Status != "Closed" && clutchUpdateRequest.Status != "Eliminated")
            {
                return BadRequest("Invalid status");
            }

            var clutch = await _clutchService.GetClutchByIdAsync(id);
            if (clutch == null)
            {
                return NotFound("Clutch not found");
            }

            if (clutch.EggResponses != null && clutch.EggResponses.Any())
            {
                foreach (var item in clutch.EggResponses)
                {
                    if(item.Status == "In Development")
                    {
                        return BadRequest("An Egg is in development");
                    }
                    if(item.Status == "Hatched" && item.BirdId == 0)
                    {
                        return BadRequest("An egg is hatched but no related bird profile!");
                    }
                }
            }

            if (await _clutchService.CloseClutch(id, clutchUpdateRequest, accountId))
            {
                return Ok("Close successfully!");
            }
            return BadRequest("Something wrong with the server, please try again");
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateClutchInfo(int id, [FromBody]ClutchUpdateRequest clutchUpdateRequest)
        {
            var accountId = _accountService.GetAccountIdFromToken();
            if (clutchUpdateRequest.BroodStartDate != null && clutchUpdateRequest.BroodEndDate != null
                && clutchUpdateRequest.BroodEndDate < clutchUpdateRequest.BroodStartDate)
            {
                return BadRequest("Brood Start Date must before Brood End Date");
            }

            var clutch = await _clutchService.GetClutchByIdAsync(id);
            if (clutch == null)
            {
                return NotFound("clutch not found");
            }

            if(await _clutchService.UpdateClutch(id, clutchUpdateRequest, accountId))
            {
                return Ok("Update Successfully");
            }
            return BadRequest();
        }
    }
}
