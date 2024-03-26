using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using BusinessObjects.IService;
using Microsoft.AspNetCore.OData.Query;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmsController : ControllerBase
    {
        private readonly IFarmService _farmService;

        public FarmsController(IFarmService farmService)
        {
            _farmService = farmService;
        }

        // GET: api/Farms
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Farm>>> GetFarms()
        {
            var farm = await _farmService.GetAllFarmsAsync();
            if(farm == null)
            {
                return NotFound("there are no farms");
            }

            return Ok(farm);
        }

        // GET: api/Farms/5
        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<Farm>> GetFarm(Guid id)
        {
            var farm = await _farmService.GetFarmByIdAsync(id);
            if(farm == null)
            {
                return NotFound("Farm not found");
            }
            return Ok(farm);
        }

        // PUT: api/Farms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutFarm(Guid id, Farm farm)
        {

            return NoContent();
        }

        // POST: api/Farms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Farm>> PostFarm(Farm farm)
        {

            return CreatedAtAction("GetFarm", new { id = farm.FarmId }, farm);
        }

        // DELETE: api/Farms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFarm(Guid id)
        {

            return NoContent();
        }*/
    }
}
