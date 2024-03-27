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
    public class EggsController : ControllerBase
    {
        private readonly IEggService _eggService;
        private readonly IClutchService _clutchService;

        public EggsController(IEggService eggService, IClutchService clutchService)
        {
            _eggService = eggService;
            _clutchService = clutchService;
        }

        // GET: api/Eggs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EggResponse>>> GetEggs()
        {
            var eggs = await _eggService.GetAllEggsAsync();
            if(!eggs.Any())
            {
                return NotFound("There are no eggs!");
            }
            return Ok(eggs);
        }

        [HttpGet("ByClutch/{clutchId}")]
        public async Task<ActionResult<IEnumerable<EggResponse>>> GetEggsByClutchId(int clutchId)
        {
            var eggs = await _eggService.GetEggsByClutchIdAsync(clutchId);
            if (!eggs.Any())
            {
                return NotFound("There are no eggs!");
            }
            return Ok(eggs);
        }

        [HttpGet("ByBreeding/{breedingId}")]
        public async Task<ActionResult<IEnumerable<EggResponse>>> GetEggsByBreedingId(int breedingId)
        {
            var eggs = await _eggService.GetEggsByBreedingIdAsync(breedingId);
            if (!eggs.Any())
            {
                return NotFound("There are no eggs!");
            }
            return Ok(eggs);
        }

        // GET: api/Eggs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EggResponse>> GetEgg(int id)
        {
            var egg = await _eggService.GetEggByIdAsync(id);
            if(egg == null)
            {
                return NotFound("Egg not found");
            }
            return Ok(egg);
        }

        [HttpGet("Bird/{birdId}")]
        public async Task<ActionResult<EggResponse>> GetEggByBirdId(int birdId)
        {
            var egg = await _eggService.GetEggByBirdIdAsync(birdId);
            if (egg == null)
            {
                return NotFound("Egg not found");
            }
            return Ok(egg);
        }

        // PUT: api/Eggs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Other/{id}")]
        public async Task<IActionResult> PutEgg(int id, [FromBody]EggUpdateRequest eggUpdateRequest)
        {
            if(id != eggUpdateRequest.EggId)
            {
                return BadRequest("Egg Id conflict");
            }

            var egg = await _eggService.GetEggByIdAsync(eggUpdateRequest.EggId);
            if (egg == null)
            {
                return BadRequest("Egg not found");
            }
            
            if(egg.Status == "Hatched")
            {
                return BadRequest("Invalid Status");
            }
            if (await _eggService.UpdateEgg(eggUpdateRequest))
            {
                return Ok("Update Sucessfully");
            }
            return BadRequest("Something is wrong with server , please try again");
        }

        [HttpPut("Hatched/{id}")]
        public async Task<IActionResult> EggHatched(int id, [FromBody]EggHatchRequest eggHatchRequest)
        {
            if (id != eggHatchRequest.EggId)
            {
                return BadRequest("Egg Id conflict");
            }

            var egg = await _eggService.GetEggByIdAsync(eggHatchRequest.EggId);
            if (egg == null)
            {
                return BadRequest("Egg not found");
            }

            if(egg.Status != "In Development")
            {
                return BadRequest("The egg is either hatched or unavailabel");
            }

            if (await _eggService.EggHatched(eggHatchRequest))
            {
                return Ok("Update Sucessfully");
            }
            return BadRequest("Something is wrong with server , please try again");
        }
        // POST: api/Eggs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EggResponse>> PostEgg(EggAddRequest eggAddRequest)
        {
            var clutch = await _clutchService.GetClutchByIdAsync(eggAddRequest.ClutchId);
            if(clutch == null)
            {
                return BadRequest("Clutch not found!");
            }

            var result = await _eggService.CreateEggAsync(eggAddRequest);
            if(result < 1)
            {
                return BadRequest("Something is wrong with server , please try again");
            }

            var egg = await _eggService.GetEggByIdAsync(result);
            return Ok(egg);
        }
    }
}
