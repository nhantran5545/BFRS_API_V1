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
    public class CheckListsController : ControllerBase
    {
        private readonly BFRS_dbContext _context;

        public CheckListsController(BFRS_dbContext context)
        {
            _context = context;
        }

        // GET: api/CheckLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckList>>> GetCheckLists()
        {
          if (_context.CheckLists == null)
          {
              return NotFound();
          }
            return await _context.CheckLists.ToListAsync();
        }

        // GET: api/CheckLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckList>> GetCheckList(Guid id)
        {
          if (_context.CheckLists == null)
          {
              return NotFound();
          }
            var checkList = await _context.CheckLists.FindAsync(id);

            if (checkList == null)
            {
                return NotFound();
            }

            return checkList;
        }

        // PUT: api/CheckLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheckList(Guid id, CheckList checkList)
        {
            if (id != checkList.CheckListId)
            {
                return BadRequest();
            }

            _context.Entry(checkList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckListExists(id))
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

        // POST: api/CheckLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CheckList>> PostCheckList(CheckList checkList)
        {
          if (_context.CheckLists == null)
          {
              return Problem("Entity set 'BFRS_dbContext.CheckLists'  is null.");
          }
            _context.CheckLists.Add(checkList);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CheckListExists(checkList.CheckListId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCheckList", new { id = checkList.CheckListId }, checkList);
        }

        // DELETE: api/CheckLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckList(Guid id)
        {
            if (_context.CheckLists == null)
            {
                return NotFound();
            }
            var checkList = await _context.CheckLists.FindAsync(id);
            if (checkList == null)
            {
                return NotFound();
            }

            _context.CheckLists.Remove(checkList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CheckListExists(Guid id)
        {
            return (_context.CheckLists?.Any(e => e.CheckListId == id)).GetValueOrDefault();
        }
    }
}
