using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.IService;
using BusinessObjects.ResponseModels;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Authorization;
using BusinessObjects.RequestModels;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        private readonly IBirdService _birdService;
        private readonly IBirdSpeciesService _birdSpeciesService;
        private readonly ICageService _cageService;
        private readonly IEggService _eggService;
        private readonly IAccountService _accountService;

        public BirdsController(IBirdService birdService, IBirdSpeciesService birdSpeciesService, ICageService cageService, IEggService eggService, IAccountService accountService)
        {
            _birdService = birdService;
            _birdSpeciesService = birdSpeciesService;
            _cageService = cageService;
            _eggService = eggService;
            _accountService = accountService;
        }

        // GET: api/Birds
        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        //[Authorize(Roles = "Manager")]
        public async Task<ActionResult<IEnumerable<BirdResponse>>> GetAllBirds()
        {
            try
            {
                var birds = await _birdService.GetAllBirdsAsync();
                if (birds == null || !birds.Any())
                {
                    return NotFound("There are no birds");
                }
                return Ok(birds);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(403, "You are not authorized to access this resource.");
            }
 
        }

        [HttpGet("InFarm")]
        public async Task<ActionResult<IEnumerable<BirdResponse>>> GetBirdsByFarmId(int FarmId)
        {
            var birds = await _birdService.GetBirdsByFarmId(FarmId);
            if (birds == null || !birds.Any())
            {
                return NotFound("There are no birds");
            }
            return Ok(birds);
        }

        [HttpGet("BySpeciesAndFarm")]
        public async Task<ActionResult<IEnumerable<BirdResponse>>> GetBirdsBySpeciesIdAndFarmId(int SpeciesId, int FarmId)
        {
            var birds = await _birdService.GetInRestBirdsBySpeciesIdAndFarmId(SpeciesId, FarmId);
            if (birds == null || !birds.Any())
            {
                return NotFound("There are no birds");
            }
            return Ok(birds);
        }

        [HttpGet("Staff/{staffId}")]
        public async Task<ActionResult<IEnumerable<BirdResponse>>> GetBirdsByStaffId(int staffId)
        {
            var birds = await _birdService.GetBirdsByStaffId(staffId);
            if (birds == null || !birds.Any())
            {
                return NotFound("There are no birds");
            }
            return Ok(birds);
        }

        // GET: api/Birds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BirdResponse>> GetBird(int id)
        {
            var bird = await _birdService.GetBirdByIdAsync(id);
            if (bird == null)
            {
                return NotFound("Bird not found");
            }
            return Ok(bird);
        }

        [HttpGet("Pedigree/{id}")]
        public async Task<IActionResult> GetBirdPedigree(int id)
        {
            var pedigree = await _birdService.GetPedigreeOfABird(id);
            return Ok(pedigree);
        }

        // PUT: api/Birds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBird(int id, [FromBody]BirdUpdateRequest birdUpdateRequest)
        {
            var bird = await _birdService.GetBirdByIdAsync(birdUpdateRequest.BirdId);
            if(bird == null)
            {
                return NotFound("Bird not found");
            }

            if (birdUpdateRequest.BirdSpeciesId != null)
            {
                var birdSpecies = await _birdSpeciesService.GetBirdSpeciesByIdAsync(birdUpdateRequest.BirdSpeciesId);
                if (birdSpecies == null)
                {
                    return NotFound("Invalid bird species");
                }
            }
            
            if(birdUpdateRequest.CageId != null)
            {
                var cage = await _cageService.GetCageByIdAsync(birdUpdateRequest.CageId);
                if (cage == null)
                {
                    return NotFound("Invalid cage");
                }
            }
            
            if(await _birdService.UpdateBirdAsync(birdUpdateRequest))
            {
                return Ok(birdUpdateRequest);
            }
            return BadRequest("Something wrong with the server Please try again");
        }

        // POST: api/Birds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("ForEgg")]
        public async Task<ActionResult<BirdResponse>> PostBirdForEgg(BirdAddFromEggRequest birdAddFromEggRequest)
        {
            var egg = await _eggService.GetEggByIdAsync(birdAddFromEggRequest.EggId);
            if (egg == null)
            {
                return NotFound("Egg not found");
            }

            if(egg.Status != "Hatched")
            {
                return BadRequest("Egg is either in development or dead");
            }

            if(egg.BirdId != 0)
            {
                return BadRequest("This egg have bird profile already");
            }

            var cage = await _cageService.GetCageByIdAsync(birdAddFromEggRequest.CageId);
            if(cage == null)
            {
                return NotFound("Invalid cage");
            }

            if (cage.Status != "Nourishing")
            {
                return BadRequest("Cage is not for nourishing");
            }

            var result = await _birdService.CreateBirdFromEggAsync(birdAddFromEggRequest);
            if (result < 1)
            {
                return BadRequest("Something wrong with the server, please try again!");
            }

            var bird = await _birdService.GetBirdByIdAsync(result);
            return Ok(bird);
        }

        [HttpPost]
        public async Task<ActionResult<BirdResponse>> PostBird(BirdAddRequest birdAddRequest)
        {
            var species = await _birdSpeciesService.GetBirdSpeciesByIdAsync(birdAddRequest.BirdSpeciesId);
            if (species == null)
            {
                return NotFound("Invalid Species");
            }

            var cage = await _cageService.GetCageByIdAsync(birdAddRequest.CageId);
            if (cage == null)
            {
                return NotFound("Invalid cage");
            }

            if (birdAddRequest.FatherBirdId != null)
            {
                var fatherBirdId = await _birdService.GetBirdByIdAsync(birdAddRequest.FatherBirdId);
                if (fatherBirdId == null || fatherBirdId.Gender != "Male")
                {
                    return NotFound("Father Bird not found");
                }
            }

            if (birdAddRequest.MotherBirdId != null)
            {
                var motherBirdId = await _birdService.GetBirdByIdAsync(birdAddRequest.MotherBirdId);
                if (motherBirdId == null || motherBirdId.Gender != "Female")
                {
                    return NotFound("Mother Bird not found");
                }
            }

            var result = await _birdService.CreateBirdAsync(birdAddRequest);
            if (result < 1)
            {
                return BadRequest("Something wrong with the server, please try again!");
            }

            var bird = await _birdService.GetBirdByIdAsync(result);
            return Ok(bird);
        }
        // DELETE: api/Birds/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBird(int id)
        {

            return NoContent();
        }*/
    }
}
