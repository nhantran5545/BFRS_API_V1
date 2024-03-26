using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IBreedingService _breedingService;

        public ClutchesController(IClutchService clutchService, IBreedingService breedingService)
        {
            _breedingService = breedingService;
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

        [HttpGet("ByBreeding/{breedingId}")]
        public async Task<ActionResult<IEnumerable<ClutchResponse>>> GetClutchesByBreedingId(int breedingId)
        {
            var clutches = await _clutchService.GetClutchsByBreedingId(breedingId);
            if (clutches == null)
            {
                return NotFound("Clutches not found!");
            }
            return Ok(clutches);
        }

        [HttpGet("ByCreated/{createdById}")]
        public async Task<ActionResult<IEnumerable<ClutchResponse>>> GetClutchesByCreatedById(int createdById)
        {
            var clutches = await _clutchService.GetClutchsByCreatedById(createdById);
            if (clutches == null)
            {
                return NotFound("Clutches not found!");
            }
            return Ok(clutches);
        }

        // GET: api/Clutches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClutchDetailResponse>> GetClutch(int id)
        {
            var clutch = await _clutchService.GetClutchByIdAsync(id);
            if (clutch == null)
            {
                return NotFound("Clutch not found");
            }
            return Ok(clutch);
        }

        [HttpPut("Close/{id}")]
        public async Task<IActionResult> CloseClutch(int id, [FromBody]ClutchUpdateRequest clutchUpdateRequest)
        {
            if(id != clutchUpdateRequest.ClutchId)
            {
                return BadRequest("clutch id conflict");
            }

            var clutch = await _clutchService.GetClutchByIdAsync(id);
            if(clutch == null)
            {
                return NotFound("Clutch not found");
            }

            if(await _clutchService.CloseClutch(clutchUpdateRequest))
            {
                return Ok("Close successfully!");
            }
            return BadRequest("Something wrong with the server, please try again");
        }

        // POST: api/Clutches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClutchResponse>> CreateClutch(ClutchAddRequest clutchAddRequest)
        {
            var breeding = await _breedingService.GetBreedingById(clutchAddRequest.BreedingId);
            if (breeding == null)
            {
                return BadRequest("Breeding not found");
            }
            var result = await _clutchService.CreateClutchAsync(clutchAddRequest);
            if (result < 1)
            {
                return BadRequest("Something is wrong with the server, please try again!");
            }
            var clutch = await _clutchService.GetClutchByIdAsync(result);
            return Ok(clutch);
        }

        // DELETE: api/Clutches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClutch(int id)
        {

            return NoContent();
        }
    }
}
