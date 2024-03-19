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

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedingsController : ControllerBase
    {
        private readonly IBreedingService _breedingService;
        private readonly IBirdService _birdService;

        public BreedingsController(IBreedingService breedingService, IBirdService birdService)
        {
            _breedingService = breedingService;
            _birdService = birdService;
        }

        [HttpGet("InbreedingCoefficient")]
        public async Task<IActionResult> GetInbreedingCoefficientPercentage(Guid fatherBirdId, Guid motherBirdId)
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

        // GET: api/Breedings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Breeding>>> GetBreedings()
        {
            var breedings = await _breedingService.GetAllBreedings();
            if(breedings != null)
            {
                return Ok(breedings);
            }
            return NotFound("There are no breeding!");
        }

        // GET: api/Breedings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Breeding>> GetBreeding(Guid id)
        {
            var breeding = await _breedingService.GetBreedingById(id);
            if(breeding != null)
            {
                return Ok(breeding);
            }
            return NotFound("Breeding not found");
        }

        // PUT: api/Breedings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBreeding(Guid id, Breeding breeding)
        {
            /*if (id != breeding.BreedingId)
            {
                return BadRequest();
            }

            _context.Entry(breeding).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreedingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }*/

            return NoContent();
        }

        // POST: api/Breedings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Breeding>> PostBreeding(BreedingAddRequest breeding)
        {
            var result = await _breedingService.CreateBreeding(breeding);
            if(result.Item1 == -1)
            {
                return BadRequest("Something is wrong with the server, please try again!");
            }
            return CreatedAtAction("GetBreeding", new { id = result.Item2 }, breeding);
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
