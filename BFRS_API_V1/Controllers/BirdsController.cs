using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using BusinessObjects.IService;
using BusinessObjects.ResponseModels;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Authorization;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        private readonly IBirdService _birdService;

        public BirdsController(IBirdService birdService)
        {
            _birdService = birdService;
        }

        // GET: api/Birds
        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<BirdResponse>>> GetAllBirds()
        {
            var birds = await _birdService.GetAllBirdsAsync();
            if (birds == null || !birds.Any())
            {
                return NotFound("There are no birds");
            }
            return Ok(birds);
        }

        [HttpGet("InFarm")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<BirdResponse>>> GetBirdsByFarmId(int FarmId)
        {
            var birds = await _birdService.GetBirdsByFarmId(FarmId);
            if (birds == null || !birds.Any())
            {
                return NotFound("There are no birds");
            }
            return Ok(birds);
        }

        [HttpGet("BySpeciesAndFarm")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<BirdResponse>>> GetBirdsBySpeciesIdAndFarmId(int SpeciesId, int FarmId)
        {
            var birds = await _birdService.GetInRestBirdsBySpeciesIdAndFarmId(SpeciesId, FarmId);
            if (birds == null || !birds.Any())
            {
                return NotFound("There are no birds");
            }
            return Ok(birds);
        }

        // GET: api/Birds/5
        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<Bird>> GetBird(int id)
        {
            var bird = await _birdService.GetBirdByIdAsync(id);
            if (bird == null)
            {
                return NotFound("Bird not found");
            }
            return Ok(bird);
        }

        [HttpGet("Pedigree/{id}")]
        [EnableQuery]
        public async Task<IActionResult> GetBirdPedigree(int id)
        {
            var pedigree = await _birdService.GetPedigreeOfABird(id);
            return Ok(pedigree);
        }

        // PUT: api/Birds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBird(int id, Bird bird)
        {
            /*if (id != bird.BirdId)
            {
                return BadRequest();
            }

            _context.Entry(bird).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BirdExists(id))
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

        // POST: api/Birds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bird>> PostBird(Bird bird)
        {

            return CreatedAtAction("GetBird", new { id = bird.BirdId }, bird);
        }

        // DELETE: api/Birds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBird(int id)
        {

            return NoContent();
        }
    }
}
