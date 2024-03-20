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
using BusinessObjects.RequestModels;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClutchesController : ControllerBase
    {
        private readonly IClutchService _clutchService;

        public ClutchesController(IClutchService clutchService)
        {
            _clutchService = clutchService;
        }

        // GET: api/Clutches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClutchResponse>>> GetClutches()
        {
            var clutches = await _clutchService.GetAllClutchsAsync();
            if(clutches == null)
            {
                return NotFound("Clutches not found!");
            }
            return Ok(clutches);
        }

        // GET: api/Clutches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClutchResponse>> GetClutch(int id)
        {
            var clutch = await _clutchService.GetClutchByIdAsync(id);
            if(clutch == null)
            {
                return NotFound("Clutch not found");
            }
            return Ok(clutch);
        }

        // PUT: api/Clutches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClutch(int id, Clutch clutch)
        {

            return NoContent();
        }

        // POST: api/Clutches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClutchResponse>> PostClutch(ClutchAddRequest clutchAddRequest)
        {
            var result = await _clutchService.CreateClutchAsync(clutchAddRequest);
            if (result < 1)
            {
                return BadRequest("Something is wrong with the server, please try again!");
            }
            return CreatedAtAction("GetClutch", new { id = result });
        }

        // DELETE: api/Clutches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClutch(int id)
        {

            return NoContent();
        }
    }
}
