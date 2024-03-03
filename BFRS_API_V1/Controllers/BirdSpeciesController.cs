using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdSpeciesController : ControllerBase
    {
        private readonly BFRS_dbContext _context;

        public BirdSpeciesController(BFRS_dbContext context)
        {
            _context = context;
        }

        // GET: api/BirdSpecies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BirdSpecy>>> GetBirdSpecies()
        {
          if (_context.BirdSpecies == null)
          {
              return NotFound();
          }
            return await _context.BirdSpecies.ToListAsync();
        }

        // GET: api/BirdSpecies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BirdSpecy>> GetBirdSpecy(Guid id)
        {
          if (_context.BirdSpecies == null)
          {
              return NotFound();
          }
            var birdSpecy = await _context.BirdSpecies.FindAsync(id);

            if (birdSpecy == null)
            {
                return NotFound();
            }

            return birdSpecy;
        }

        // PUT: api/BirdSpecies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBirdSpecy(Guid id, BirdSpecy birdSpecy)
        {
            if (id != birdSpecy.BirdSpeciesId)
            {
                return BadRequest();
            }

            _context.Entry(birdSpecy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BirdSpecyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BirdSpecies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BirdSpecy>> PostBirdSpecy(BirdSpecy birdSpecy)
        {
          if (_context.BirdSpecies == null)
          {
              return Problem("Entity set 'BFRS_dbContext.BirdSpecies'  is null.");
          }
            _context.BirdSpecies.Add(birdSpecy);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BirdSpecyExists(birdSpecy.BirdSpeciesId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBirdSpecy", new { id = birdSpecy.BirdSpeciesId }, birdSpecy);
        }

        // DELETE: api/BirdSpecies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBirdSpecy(Guid id)
        {
            if (_context.BirdSpecies == null)
            {
                return NotFound();
            }
            var birdSpecy = await _context.BirdSpecies.FindAsync(id);
            if (birdSpecy == null)
            {
                return NotFound();
            }

            _context.BirdSpecies.Remove(birdSpecy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BirdSpecyExists(Guid id)
        {
            return (_context.BirdSpecies?.Any(e => e.BirdSpeciesId == id)).GetValueOrDefault();
        }
    }
}
