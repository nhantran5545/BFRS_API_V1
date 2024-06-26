﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.IService;
using Microsoft.AspNetCore.Authorization;
using BusinessObjects.RequestModels.CageReqModels;
using BusinessObjects.ResponseModels.CageResModels;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CagesController : ControllerBase
    {
        private readonly ICageService _cageService;
        private readonly IBirdService _birdService;
        private readonly IAreaService  _areaService;
        private readonly IFarmService _farmService;
        private readonly IAccountService _accountService;

        public CagesController(ICageService cageService, IBirdService birdService, IFarmService farmService, IAccountService accountService, IAreaService areaService)
        {
            _cageService = cageService;
            _birdService = birdService;
            _farmService = farmService;
            _accountService = accountService;
            _areaService = areaService; 
        }

        // GET: api/Cages
        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<IEnumerable<CageResponse>>> GetCages()
        {
            var cages = await _cageService.GetAllCagesAsync();
            if (cages == null)
            {
                return NotFound("Cages not found");
            }
            return Ok(cages);
        }

        [HttpGet("staff")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetCagesByStaffId()
        {
            try
            {
                var staffAccountId =  _accountService.GetAccountIdFromToken();

                if (!_accountService.IsStaff(staffAccountId))
                {
                    return Forbid("You are not authorized to access this resource.");
                }

                var cages = await _cageService.GetCagesByStaffIdAsync(staffAccountId);
                return Ok(cages);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("farm/{farmId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetCagesByFarmId(int farmId)
        {
            var farm = await _farmService.GetFarmByIdAsync(farmId);
            if(farm == null)
            {
                return NotFound("Farm not found");
            }

            var cages = await _cageService.GetCagesByFarmIdAsync(farmId);
            if (cages == null)
            {
                return NotFound("Cages not found");
            }
            return Ok(cages);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CreateCage([FromBody] CageAddRequest request)
        {
            try
            {
                await _cageService.CreateCageAsync(request);
                return Ok("Add cage successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{cageId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateCage(int cageId, CageUpdateRequest request)
        {
            var cage = await _cageService.GetCageByIdAsync(cageId);
            if (cage == null)
            {
                return NotFound("Cage Not Found");
            }
            var currentArea = await _areaService.GetAreaByIdAsync(cage.AreaId);
            if (currentArea == null)
            {
                return BadRequest("Current area not found");
            }

            var targetArea = await _areaService.GetAreaByIdAsync(request.AreaId);
            if (targetArea == null)
            {
                return BadRequest("Target area not found");
            }

            var birdsInCage = await _birdService.GetBirdsByCageIdAsync(cageId);

            if (currentArea.Status == "For Nourishing" && targetArea.Status == "For Breeding" && birdsInCage.Any())
            {
                return BadRequest("Please move birds to another cage before transferring the cage to a breeding area");
            }

            if (currentArea.Status == "For Breeding" && targetArea.Status == "For Nourishing" && cage.Status != "Standby")
            {
                return BadRequest("You can only transfer a cage with standby status from breeding area to nourishing area");
            }
            var success = await _cageService.UpdateCageAsync(cageId, request);
            if (success)
            {
                return Ok("Cage updated successfully");
            }
            else
            {
                return BadRequest("Failed to update cage");
            }
        }


        [HttpGet("ForBreeding")]
        [Authorize(Roles = "Admin, Manager")]
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
        [Authorize(Roles = "Admin, Manager")]
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
        [HttpGet("{cageId}")]
        [Authorize]
        public async Task<ActionResult<CageDetailResponse>> GetCage(int cageId)
        {
            var cage = await _cageService.GetCageByIdAsync(cageId);
            if(cage == null)
            {
                return NotFound("Cage not found");
            }
            return Ok(cage);
        }
    }
}
