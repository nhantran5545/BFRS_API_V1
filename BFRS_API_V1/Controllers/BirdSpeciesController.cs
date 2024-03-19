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

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdSpeciesController : ControllerBase
    {
        private readonly IBirdSpeciesService _birdSpeciesService;

        public BirdSpeciesController(IBirdSpeciesService birdSpeciesService)
        {
            _birdSpeciesService = birdSpeciesService;
        }

        // GET: api/BirdSpecies
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<BirdSpeciesResponse>>> GetBirdSpecies()
        {
            var birdSpecies = await _birdSpeciesService.GetBirdSpeciesAsync();
            if (birdSpecies == null)
            {
                return NotFound("There are no bird species");
            }
            return Ok(birdSpecies);
        }

        // GET: api/BirdSpecies/5
        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<BirdSpeciesResponse>> GetBirdSpecy(Guid id)
        {
            var birdspecy = await _birdSpeciesService.GetBirdSpeciesByIdAsync(id);
            if(birdspecy == null)
            {
                return NotFound("There are no bird species");
            }
            return Ok(birdspecy);
        }

        // PUT: api/BirdSpecies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBirdSpecy(Guid id, BirdSpecy birdSpecy)
        {

            return NoContent();
        }

        // POST: api/BirdSpecies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BirdSpecy>> PostBirdSpecy(BirdSpecy birdSpecy)
        {

            return CreatedAtAction("GetBirdSpecy", new { id = birdSpecy.BirdSpeciesId }, birdSpecy);
        }

        // DELETE: api/BirdSpecies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBirdSpecy(Guid id)
        {

            return NoContent();
        }
    }
}
