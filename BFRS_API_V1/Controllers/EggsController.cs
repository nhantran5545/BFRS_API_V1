﻿using System;
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
            
            if (await _eggService.UpdateEgg(eggUpdateRequest))
            {
                return BadRequest("Something is wrong with server , please try again");
            }
            return Ok("Update Sucessfully");
        }

        [HttpPut("Hatched/{id}")]
        public async Task<IActionResult> EggHatched(int id, [FromBody]EggUpdateRequest eggUpdateRequest)
        {
            if (id != eggUpdateRequest.EggId)
            {
                return BadRequest("Egg Id conflict");
            }

            var egg = await _eggService.GetEggByIdAsync(eggUpdateRequest.EggId);
            if (egg == null)
            {
                return BadRequest("Egg not found");
            }

            if (await _eggService.UpdateEgg(eggUpdateRequest))
            {
                return BadRequest("Something is wrong with server , please try again");
            }
            return Ok("Update Sucessfully");
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

        // DELETE: api/Eggs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEgg(int id)
        {

            return NoContent();
        }
    }
}
