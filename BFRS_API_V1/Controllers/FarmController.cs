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
using Microsoft.AspNetCore.Authorization;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using BusinessObjects.IService.Implements;
using Azure.Core;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmController : ControllerBase
    {
        private readonly IFarmService _farmService;
        private readonly IAccountService _accountService;
        public FarmController(IFarmService farmService, IAccountService accountService)
        {
            _farmService = farmService;
            _accountService = accountService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> CreateFarm(FarmRequest farmRequest)
        {
            try
            {
                var farm = await _farmService.CreateFarmAsync(farmRequest);
                return Ok("Add successful");
            } catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(403, "You are not authorized to access this resource.");
            }
        }


        [HttpGet]
        [Authorize(Roles ="Admin, Manager")]
        public async Task<ActionResult<IEnumerable<FarmResponse>>> GetAllFarms()
        {
            try
            {
                var farm = await _farmService.GetAllFarmsAsync();
                if (farm == null)
                {
                    return NotFound("there are no farms");
                }

                return Ok(farm);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(403, "You are not authorized to access this resource.");
            }
        }

        // GET: api/Farms/5
        [HttpGet("{farmId}")]
        [Authorize]
        public async Task<ActionResult<FarmResponse>> GetFarm(int farmId)
        {
            var farm = await _farmService.GetFarmByIdAsync(farmId);
            if(farm == null)
            {
                return NotFound("Farm not found");
            }
            return Ok(farm);
        }

        [HttpPut("{farmId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> PutFarm(int farmId, [FromBody] FarmUpdateRequest farmUpdateRequest)
        {
         
                var farm = await _farmService.GetFarmByIdAsync(farmId);
                if (farm == null)
                {
                    return NotFound("Farm not found");
                }

                if ( await _farmService.UpdateFarmAsync(farmId, farmUpdateRequest))
                {
                    return Ok(farmUpdateRequest);
                }
                return BadRequest("Something wrong with the server Please try again");
        }
    }
}
