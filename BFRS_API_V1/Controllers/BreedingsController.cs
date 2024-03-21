using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using BusinessObjects.IService;
using BusinessObjects.RequestModels;
using Microsoft.AspNetCore.OData.Query;
using BusinessObjects.ResponseModels;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedingsController : ControllerBase
    {
        private readonly IBreedingService _breedingService;
        private readonly IBirdService _birdService;
        private readonly ICageService _cageService;

        public BreedingsController(IBreedingService breedingService, IBirdService birdService, ICageService cageService)
        {
            _breedingService = breedingService;
            _birdService = birdService;
            _cageService = cageService;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBirdsTogether(BreedingUpdateRequest breedingUpdateRequest)
        {
            var breeding = await _breedingService.GetBreedingById(breedingUpdateRequest.BreedingId);
            if (breeding == null)
            {
                return NotFound("Breeding not found");
            }

            var cage = await _cageService.GetCageByIdAsync(breeding.CageId);
            if(cage == null || cage.AccountId != breedingUpdateRequest.StaffId)
            {

            }
            return Ok();
        }

        // POST: api/Breedings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Breeding>> OpenBreeding(BreedingAddRequest breeding)
        {
            var result = await _breedingService.CreateBreeding(breeding);
            if(result < 1)
            {
                return BadRequest("Something is wrong with the server, please try again!");
            }
            return CreatedAtAction("GetBreeding", new { id = result }, breeding);
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
