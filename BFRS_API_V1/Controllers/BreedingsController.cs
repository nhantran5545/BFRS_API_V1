using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.IService;
using BusinessObjects.RequestModels;
using Microsoft.AspNetCore.OData.Query;
using BusinessObjects.ResponseModels;
using AutoMapper;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedingsController : ControllerBase
    {
        private readonly IBreedingService _breedingService;
        private readonly IBirdService _birdService;
        private readonly ICageService _cageService;
        private readonly IMapper _mapper;

        public BreedingsController(IBreedingService breedingService, IBirdService birdService, ICageService cageService, IMapper mapper)
        {
            _breedingService = breedingService;
            _birdService = birdService;
            _cageService = cageService;
            _mapper = mapper;
        }

        [HttpGet("InbreedingCoefficient")]
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
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<BreedingResponse>>> GetBreedings()
        {
            var breedings = await _breedingService.GetAllBreedings();
            if(breedings == null)
            {
                return NotFound("There are no breeding!");
            }
            return Ok(breedings); 
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<BreedingResponse>> GetBreeding(int id)
        {
            var breeding = await _breedingService.GetBreedingById(id);
            if(breeding == null)
            {
                return NotFound("Breeding not found");
            }
            return Ok(breeding);
        }

        [HttpPost]
        public async Task<ActionResult<BreedingResponse>> OpenBreeding(BreedingAddRequest breedingAddRequest)
        {
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
            if(cage == null || cage.Status != "Breeding")
            {
                return BadRequest("Cage is either unavailable or not for breeding");
            }

            var result = await _breedingService.CreateBreeding(breedingAddRequest);
            if(result < 1)
            {
                return BadRequest("Something is wrong with the server, please try again!");
            }
            var breeding = await _breedingService.GetBreedingById(result);
            return Ok(breeding);
        }

        [HttpPut("PutBirdsTogether")]
        public async Task<IActionResult> PutBirdsTogether(BreedingUpdateRequest breedingUpdateRequest)
        {
            var breeding = await _breedingService.GetBreedingById(breedingUpdateRequest.BreedingId);
            if (breeding == null)
            {
                return NotFound("Breeding not found");
            }

            var cage = await _cageService.GetCageByIdAsync(breeding.CageId);
            if (cage == null || cage.AccountId != breedingUpdateRequest.StaffId)
            {
                return BadRequest("You are not in charge of this breeding!");
            }

            if (await _breedingService.PutBirdsToBreeding(breedingUpdateRequest))
            {
                return Ok("Update Successfully");
            }
            return BadRequest("Something is wrong with the server please try again!");
        }

        /*[HttpPut("BreedingInProgress")]
        public async Task<IActionResult> BreedingInProgress(BreedingUpdateRequest breedingUpdateRequest)
        {
            var breeding = await _breedingService.GetBreedingById(breedingUpdateRequest.BreedingId);
            if (breeding == null)
            {
                return NotFound("Breeding not found");
            }

            var cage = await _cageService.GetCageByIdAsync(breeding.CageId);
            if (cage == null || cage.AccountId != breedingUpdateRequest.StaffId)
            {
                return BadRequest("You are not in charge of this breeding!");
            }

            if (await _breedingService.BreedingInProgress(breedingUpdateRequest))
            {
                return Ok("Update Successfully");
            }
            return BadRequest("Something is wrong with the server please try again!");
        }*/

        [HttpPut("closeBreeding")]
        public async Task<IActionResult> CloseBreeding(BreedingCloseRequest breedingCloseRequest)
        {
            var breeding = await _breedingService.GetBreedingById(breedingCloseRequest.BreedingId);
            if (breeding == null)
            {
                return NotFound("Breeding not found");
            }

            if(breeding.CreatedBy != breedingCloseRequest.ManagerId)
            {
                return BadRequest("This is not your breeding");
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

            if (await _breedingService.CloseBreeding(breedingCloseRequest))
            {
                return Ok("Update Successfully");
            }
            return BadRequest("Something is wrong with the server please try again!");
        }

        // DELETE: api/Breedings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreeding(Guid id)
        {
            /*if (_context.Breedings == null)
            {
                return NotFound();
            }
            var breeding = await _context.Breedings.FindAsync(id);
            if (breeding == null)
            {
                return NotFound();
            }

            _context.Breedings.Remove(breeding);
            await _context.SaveChangesAsync();*/

            return NoContent();
        }
    }
}
