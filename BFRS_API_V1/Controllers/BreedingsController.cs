using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.IService;
using Microsoft.AspNetCore.Authorization;
using BusinessObjects.RequestModels.BreedingReqModels;
using BusinessObjects.ResponseModels.BreedingResModels;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedingsController : ControllerBase
    {
        private readonly IBreedingService _breedingService;
        private readonly IBirdService _birdService;
        private readonly ICageService _cageService;
        private readonly IAccountService _accountService;

        public BreedingsController(IBreedingService breedingService, IBirdService birdService, 
            ICageService cageService, IAccountService accountService)
        {
            _breedingService = breedingService;
            _birdService = birdService;
            _cageService = cageService;
            _accountService = accountService;
        }

        [HttpGet("InbreedingCoefficient")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetInbreedingCoefficientPercentage(int fatherBirdId, int motherBirdId)
        {
            var fatherBird = await _birdService.GetBirdByIdAsync(fatherBirdId);
            if (fatherBird == null || fatherBird.Gender != "Male")
            {
                return NotFound("Wrong male bird id");
            }

            var motherBird = await _birdService.GetBirdByIdAsync(motherBirdId);
            if (motherBird == null || motherBird.Gender != "Female")
            {
                return NotFound("Wrong female bird id");
            }

            var percentage = await _breedingService.CalculateInbreedingPercentage(fatherBirdId, motherBirdId);
            return Ok(percentage);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<IEnumerable<BreedingResponse>>> GetBreedings()
        {
            var breedings = await _breedingService.GetAllBreedings();
            if(breedings == null)
            {
                return NotFound("There are no breeding!");
            }
            return Ok(breedings); 
        }

        [HttpGet("manager")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<IEnumerable<BreedingResponse>>> GetBreedingsByManagerId()
        {
            var managerId = _accountService.GetAccountIdFromToken();
            var breedings = await _breedingService.GetAllBreedingsByManagerId(managerId);
            if (breedings == null)
            {
                return NotFound("There are no breeding!");
            }
            return Ok(breedings);
        }

        [HttpGet("staff")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BreedingResponse>>> GetBreedingsByStaff()
        {
            var accountId = _accountService.GetAccountIdFromToken();
            var breedings = await _breedingService.GetBreedingsByStaffIdAsync(accountId);
            if (breedings == null)
            {
                return NotFound("There are no breeding!");
            }
            return Ok(breedings);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<BreedingDetailResponse>> GetBreedingById(int id)
        {
            var breeding = await _breedingService.GetBreedingById(id);
            if(breeding == null)
            {
                return NotFound("Breeding not found");
            }
            return Ok(breeding);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<BreedingResponse>> OpenBreeding(BreedingAddRequest breedingAddRequest)
        {
            var managerId = _accountService.GetAccountIdFromToken();
            var fatherBird = await _birdService.GetBirdByIdAsync(breedingAddRequest.FatherBirdId);
            if (fatherBird == null || fatherBird.Gender != "Male")
            {
                return NotFound("Wrong male bird id");
            }

            if (fatherBird.Status != "InRestPeriod")
            {
                return BadRequest("Father Bird is in another breeding or sick");
            }

            var motherBird = await _birdService.GetBirdByIdAsync(breedingAddRequest.MotherBirdId);
            if (motherBird == null || motherBird.Gender != "Female")
            {
                return NotFound("Wrong female bird id");
            }

            if (motherBird.Status != "InRestPeriod")
            {
                return BadRequest("Mother Bird is in another breeding or sick");
            }

            var cage = await _cageService.GetCageByIdAsync(breedingAddRequest.CageId);
            if(cage == null)
            {
                return BadRequest("Cage not found");
            }

            if(cage.Status != "Standby")
            {
                return BadRequest("Cage is either not for breeding or in breeding progress");
            }

            var result = await _breedingService.CreateBreeding(breedingAddRequest, managerId);
            if(result < 1)
            {
                return BadRequest("Something is wrong with the server, please try again!");
            }
            var breeding = await _breedingService.GetBreedingById(result);
            return Ok(breeding);
        }

        [HttpPut("closeBreeding")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CloseBreeding(BreedingCloseRequest breedingCloseRequest)
        {
            var managerId = _accountService.GetAccountIdFromToken();
            var breeding = await _breedingService.GetBreedingById(breedingCloseRequest.BreedingId);
            if (breeding == null)
            {
                return NotFound("Breeding not found");
            }

            if(breeding.CreatedBy != managerId)
            {
                return BadRequest("This is not your breeding");
            }

            if (breeding.ClutchResponses != null && breeding.ClutchResponses.Any())
            {
                foreach (var item in breeding.ClutchResponses)
                {
                    if (item.Status != "Closed" && item.Status != "Eliminated")
                    {
                        return BadRequest("A clutch is in progress");
                    }
                }
            }

            var fatherCage = await _cageService.GetCageByIdAsync(breedingCloseRequest.FatherCageId); 
            if (fatherCage == null || fatherCage.Status != "Nourishing")
            {
                return BadRequest("Father can not move to this cage");
            }

            var motherCage = await _cageService.GetCageByIdAsync(breedingCloseRequest.MotherCageId);
            if (motherCage == null || motherCage.Status != "Nourishing")
            {
                return BadRequest("Mother can not move to this cage");
            }

            if (await _breedingService.CloseBreeding(breedingCloseRequest, managerId))
            {
                return Ok("Update Successfully");
            }
            return BadRequest("Something is wrong with the server please try again!");
        }

        [HttpPut("cancelBreeding")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CancelBreeding(BreedingUpdateRequest breedingUpdateRequest)
        {
            var managerId = _accountService.GetAccountIdFromToken();
            /*if (breedingUpdateRequest.Status != "Failed" && breedingUpdateRequest.Status != "Cancelled")
            {
                return BadRequest("Invalid Status");
            }*/

            var breeding = await _breedingService.GetBreedingById(breedingUpdateRequest.BreedingId);
            if (breeding == null)
            {
                return NotFound("Breeding not found");
            }

            if (breeding.CreatedBy != managerId)
            {
                return BadRequest("This is not your breeding");
            }

            var fatherCage = await _cageService.GetCageByIdAsync(breedingUpdateRequest.FatherCageId);
            if (fatherCage == null || fatherCage.Status != "Nourishing")
            {
                return BadRequest("Father can not move to this cage");
            }

            var motherCage = await _cageService.GetCageByIdAsync(breedingUpdateRequest.MotherCageId);
            if (motherCage == null || motherCage.Status != "Nourishing")
            {
                return BadRequest("Mother can not move to this cage");
            }

            if (await _breedingService.CancelBreeding(breedingUpdateRequest, managerId))
            {
                return Ok("Update Successfully");
            }
            return BadRequest("Something is wrong with the server please try again!");
        }
    }
}
