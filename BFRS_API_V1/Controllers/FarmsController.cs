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
    public class FarmsController : ControllerBase
    {
        private readonly BFRS_dbContext _context;

        public FarmsController(BFRS_dbContext context)
        {
            _context = context;
        }

        // GET: api/Farms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Farm>>> GetFarms()
        {
          if (_context.Farms == null)
          {
              return NotFound();
          }
            return await _context.Farms.ToListAsync();
        }

        // GET: api/Farms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Farm>> GetFarm(Guid id)
        {
          if (_context.Farms == null)
          {
              return NotFound();
          }
            var farm = await _context.Farms.FindAsync(id);

            if (farm == null)
            {
                return NotFound();
            }

            return farm;
        }

        // PUT: api/Farms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFarm(Guid id, Farm farm)
        {
            if (id != farm.FarmId)
            {
                return BadRequest();
            }

            _context.Entry(farm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FarmExists(id))
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

        // POST: api/Farms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Farm>> PostFarm(Farm farm)
        {
          if (_context.Farms == null)
          {
              return Problem("Entity set 'BFRS_dbContext.Farms'  is null.");
          }
            _context.Farms.Add(farm);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FarmExists(farm.FarmId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFarm", new { id = farm.FarmId }, farm);
        }

        // DELETE: api/Farms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFarm(Guid id)
        {
            if (_context.Farms == null)
            {
                return NotFound();
            }
            var farm = await _context.Farms.FindAsync(id);
            if (farm == null)
            {
                return NotFound();
            }

            _context.Farms.Remove(farm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FarmExists(Guid id)
        {
            return (_context.Farms?.Any(e => e.FarmId == id)).GetValueOrDefault();
        }
    }
}
