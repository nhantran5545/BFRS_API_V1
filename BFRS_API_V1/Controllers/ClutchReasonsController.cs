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
    public class ClutchReasonsController : ControllerBase
    {
        private readonly BFRS_dbContext _context;

        public ClutchReasonsController(BFRS_dbContext context)
        {
            _context = context;
        }

        // GET: api/ClutchReasons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClutchReason>>> GetClutchReasons()
        {
          if (_context.ClutchReasons == null)
          {
              return NotFound();
          }
            return await _context.ClutchReasons.ToListAsync();
        }

        // GET: api/ClutchReasons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClutchReason>> GetClutchReason(int id)
        {
          if (_context.ClutchReasons == null)
          {
              return NotFound();
          }
            var clutchReason = await _context.ClutchReasons.FindAsync(id);

            if (clutchReason == null)
            {
                return NotFound();
            }

            return clutchReason;
        }

        // PUT: api/ClutchReasons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClutchReason(int id, ClutchReason clutchReason)
        {
            if (id != clutchReason.ClutchReasonId)
            {
                return BadRequest();
            }

            _context.Entry(clutchReason).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClutchReasonExists(id))
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

        // POST: api/ClutchReasons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClutchReason>> PostClutchReason(ClutchReason clutchReason)
        {
          if (_context.ClutchReasons == null)
          {
              return Problem("Entity set 'BFRS_dbContext.ClutchReasons'  is null.");
          }
            _context.ClutchReasons.Add(clutchReason);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClutchReason", new { id = clutchReason.ClutchReasonId }, clutchReason);
        }

        // DELETE: api/ClutchReasons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClutchReason(int id)
        {
            if (_context.ClutchReasons == null)
            {
                return NotFound();
            }
            var clutchReason = await _context.ClutchReasons.FindAsync(id);
            if (clutchReason == null)
            {
                return NotFound();
            }

            _context.ClutchReasons.Remove(clutchReason);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClutchReasonExists(int id)
        {
            return (_context.ClutchReasons?.Any(e => e.ClutchReasonId == id)).GetValueOrDefault();
        }
    }
}
