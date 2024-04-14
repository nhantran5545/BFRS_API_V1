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
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using BusinessObjects.IService.Implements;
using BusinessObjects.RequestModels;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdSpeciesController : ControllerBase
    {
        private readonly IBirdSpeciesService _birdSpeciesService;
        private readonly IAccountService _accountService;

        public BirdSpeciesController(IBirdSpeciesService birdSpeciesService, IAccountService accountService)
        {
            _birdSpeciesService = birdSpeciesService;
            _accountService = accountService;
        }

        // GET: api/BirdSpecies
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BirdSpeciesResponse>>> GetBirdSpecies()
        {
            var birdSpecies = await _birdSpeciesService.GetBirdSpeciesAsync();
            if (birdSpecies == null)
            {
                return NotFound("There are no bird species");
            }
            return Ok(birdSpecies);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<int>> CreateBirdSpecies(BirdSpeciesRequest speciesRequest)
        {
            try
            {
                var species = await _birdSpeciesService.CreateBirdSpeciesAsync(speciesRequest);
                return Ok("Add successful");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(403, "You are not authorized to access this resource.");
            }
        }

        // GET: api/BirdSpecies/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<BirdSpeciesDetailResponse>> GetBirdSpecy(int id)
        {
            var birdspecy = await _birdSpeciesService.GetBirdSpeciesByIdAsync(id);
            if (birdspecy == null)
            {
                return NotFound("There are no bird species");
            }
            return Ok(birdspecy);
        }

        [HttpPut("{speciesId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateSpecies(int speciesId, [FromBody] BirdSpeciesRequest request)
        {
            var birdSpecies = await _birdSpeciesService.GetBirdSpeciesByIdAsync(speciesId);
            if (birdSpecies == null)
            {
                return NotFound("Farm not found");
            }

            if (await _birdSpeciesService.UpdateSpeciesAsync(speciesId, request))
            {
                return Ok(request);
            }
            return BadRequest("Something wrong with the server Please try again");
        }
    }
}
