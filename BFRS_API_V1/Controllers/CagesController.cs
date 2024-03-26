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

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CagesController : ControllerBase
    {
        private readonly ICageService _cageService;
        private readonly IBirdService _birdService;
        private readonly IFarmService _farmService;

        public CagesController(ICageService cageService, IBirdService birdService, IFarmService farmService)
        {
            _cageService = cageService;
            _birdService = birdService;
            _farmService = farmService;
        }

        // GET: api/Cages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CageResponse>>> GetCages()
        {
            var cages = await _cageService.GetAllCagesAsync();
            if(cages == null)
            {
                return NotFound("Cages not found");
            }
            return Ok(cages);
        }

        [HttpGet("ForBreeding")]
        public async Task<ActionResult<IEnumerable<CageResponse>>> GetCagesForBreeding(int fatherBirdId, int motherBirdId, int farmId)
        {
            var fatherBird = await _birdService.GetBirdByIdAsync(fatherBirdId);
            if (fatherBird == null || fatherBird.Gender != "Male")
            {
                return BadRequest("Wrong male bird");
            }

            var motherBird = await _birdService.GetBirdByIdAsync(motherBirdId);
            if (motherBird == null || motherBird.Gender != "Female")
            {
                return BadRequest("Wrong female bird");
            }

            var farm = await _farmService.GetFarmByIdAsync(farmId);
            if(farm == null)
            {
                return BadRequest("Farm not found");
            }
            if (farm.FarmId != fatherBird.FarmId || farm.FarmId != motherBird.FarmId)
            {
                return BadRequest("Birds are not in your farm");
            }
            var cages = await _cageService.GetCagesForBreeding(fatherBirdId, motherBirdId, farmId);
            if (cages == null)
            {
                return NotFound("Cages not found");
            }
            return Ok(cages);
        }

        [HttpGet("ForBreeding/{farmId}")]
        public async Task<ActionResult<IEnumerable<CageResponse>>> GetCagesForBreedingByFarm(int farmId)
        {
            var farm = await _farmService.GetFarmByIdAsync(farmId);
            if (farm == null)
            {
                return BadRequest("Farm not found");
            }

            var cages = await _cageService.GetCagesForBreeding(farmId);
            if (cages == null)
            {
                return NotFound("Cages not found");
            }
            return Ok(cages);
        }

        // GET: api/Cages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cage>> GetCage(int id)
        {
            var cage = await _cageService.GetCageByIdAsync(id);
            if(cage == null)
            {
                return NotFound("Cage not found");
            }
            return Ok(cage);
        }

        // PUT: api/Cages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCage(int id, Cage cage)
        {

            return NoContent();
        }

        // POST: api/Cages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cage>> PostCage(Cage cage)
        {

            return CreatedAtAction("GetCage", new { id = cage.CageId }, cage);
        }

        // DELETE: api/Cages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCage(int id)
        {

            return NoContent();
        }
    }
}
