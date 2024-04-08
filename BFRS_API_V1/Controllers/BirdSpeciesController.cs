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

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdSpeciesController : ControllerBase
    {
        private readonly IBirdSpeciesService _birdSpeciesService;

        public BirdSpeciesController(IBirdSpeciesService birdSpeciesService)
        {
            _birdSpeciesService = birdSpeciesService;
        }

        // GET: api/BirdSpecies
        [HttpGet]
        [Authorize(Roles = "Admin, Manager, Staff")]
        public async Task<ActionResult<IEnumerable<BirdSpeciesResponse>>> GetBirdSpecies()
        {
            var birdSpecies = await _birdSpeciesService.GetBirdSpeciesAsync();
            if (birdSpecies == null)
            {
                return NotFound("There are no bird species");
            }
            return Ok(birdSpecies);
        }

        // GET: api/BirdSpecies/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager, Staff")]
        public async Task<ActionResult<BirdSpeciesDetailResponse>> GetBirdSpecy(int id)
        {
            var birdspecy = await _birdSpeciesService.GetBirdSpeciesByIdAsync(id);
            if (birdspecy == null)
            {
                return NotFound("There are no bird species");
            }
            return Ok(birdspecy);
        }
    }
}
