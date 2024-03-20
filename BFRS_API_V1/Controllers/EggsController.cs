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
    public class EggsController : ControllerBase
    {
        private readonly BFRS_dbContext _context;

        public EggsController(BFRS_dbContext context)
        {
            _context = context;
        }

        // GET: api/Eggs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Egg>>> GetEggs()
        {
          if (_context.Eggs == null)
          {
              return NotFound();
          }
            return await _context.Eggs.ToListAsync();
        }

        // GET: api/Eggs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Egg>> GetEgg(int id)
        {
          if (_context.Eggs == null)
          {
              return NotFound();
          }
            var egg = await _context.Eggs.FindAsync(id);

            if (egg == null)
            {
                return NotFound();
            }

            return egg;
        }

        // PUT: api/Eggs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEgg(int id, Egg egg)
        {
            if (id != egg.EggId)
            {
                return BadRequest();
            }

            _context.Entry(egg).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EggExists(id))
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

        // POST: api/Eggs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Egg>> PostEgg(Egg egg)
        {
          if (_context.Eggs == null)
          {
              return Problem("Entity set 'BFRS_dbContext.Eggs'  is null.");
          }
            _context.Eggs.Add(egg);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEgg", new { id = egg.EggId }, egg);
        }

        // DELETE: api/Eggs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEgg(int id)
        {
            if (_context.Eggs == null)
            {
                return NotFound();
            }
            var egg = await _context.Eggs.FindAsync(id);
            if (egg == null)
            {
                return NotFound();
            }

            _context.Eggs.Remove(egg);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EggExists(int id)
        {
            return (_context.Eggs?.Any(e => e.EggId == id)).GetValueOrDefault();
        }
    }
}
