﻿using BusinessObjects.IService;
using BusinessObjects.IService.Implements;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedingCheckListController : ControllerBase
    {
        private readonly IBreedingCheckListService _breedingCheckListService;
        private readonly IBreedingService _breedingService;

        public BreedingCheckListController(IBreedingCheckListService breedingCheckListService, IBreedingService breedingService)
        {
            _breedingCheckListService = breedingCheckListService ?? throw new ArgumentNullException(nameof(breedingCheckListService));
            _breedingService = breedingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBreedingCheckLists()
        {
            var breedingCheckLists = await _breedingCheckListService.GetBreedingCheckListsAsync();
            if(!breedingCheckLists.Any())
            {
                return NotFound("CheckList answers not found!");
            }
            return Ok(breedingCheckLists);
        }

        [HttpGet("ByBreeding/{breedingId}")]
        public async Task<IActionResult> GetBreedingCheckListsByBreedingId(int breedingId)
        {
            var breedingCheckLists = await _breedingCheckListService.GetBreedingCheckListsByBreedingId(breedingId);
            if (!breedingCheckLists.Any())
            {
                return NotFound("CheckList answers not found!");
            }
            return Ok(breedingCheckLists);
        }

        [HttpGet("ByBreedingAndPhase")]
        public async Task<IActionResult> GetBreedingCheckListsByBreedingId(int breedingId, int phase)
        {
            var breedingCheckLists = await _breedingCheckListService.GetBreedingCheckListsByBreedingIdAndPhase(breedingId, phase);
            if (!breedingCheckLists.Any())
            {
                return NotFound("CheckList answers not found!");
            }
            return Ok(breedingCheckLists);
        }

        [HttpGet("ByClutchAndPhase")]
        public async Task<IActionResult> GetBreedingCheckListsByClutchId(int clutchId, int phase)
        {
            var breedingCheckLists = await _breedingCheckListService.GetBreedingCheckListsByClutchIdAndPhase(clutchId, phase);
            if (!breedingCheckLists.Any())
            {
                return NotFound("CheckList answers not found!");
            }
            return Ok(breedingCheckLists);
        }

        [HttpGet("{breedingCheckListId}")]
        public async Task<IActionResult> GetBreedingCheckList(int breedingCheckListId)
        {
            var breedingCheckList = await _breedingCheckListService.GetBreedingCheckListDetail(breedingCheckListId);
            if (breedingCheckList == null)
            {
                return NotFound("CheckList answer not found");
            }
            return Ok(breedingCheckList);
        }

        [HttpGet("Today/{breedingId}")]
        public async Task<IActionResult> GetTodayBreedingCheckListByBreedingId(int breedingId)
        {
            var breeding = await _breedingService.GetBreedingById(breedingId);
            if(breeding == null)
            {
                return NotFound("Breeding not found");
            }

            var breedingCheckListResponse = await _breedingCheckListService.GetTodayBreedingCheckListDetail(breeding);
            if (breedingCheckListResponse == null)
            {
                return BadRequest("Breeding's phase is invalid");
            }

            return Ok(breedingCheckListResponse);
        }

        [HttpPost("Today/{breedingId}")]
        public async Task<IActionResult> CreateTodayBreedingCheckListByBreedingId(BreedingCheckListAddRequest breedingCheckListAddRequest)
        {
            var result = await _breedingCheckListService.CreateBreedingCheckList(breedingCheckListAddRequest);
            if(result < 1)
            {
                return BadRequest("Something is wrong with the server. Please try again");
            }

            var breedingCheckList = await _breedingCheckListService.GetBreedingCheckListDetail(result);
            return Ok(breedingCheckList);
        }

        [HttpGet("ForMating{breedingId}")]
        public async Task<IActionResult> GetCheckListForMatingBreeding(int breedingId)
        {
            var breeding = await _breedingService.GetBreedingById(breedingId);
            if(breeding == null)
            {
                return NotFound("Breeding not found");
            }

            if(breeding.Status != "Mating")
            {
                return BadRequest("You can not create checklist because the phase has passed!");
            }

            var breedingCheckLists = await _breedingCheckListService.GetBreedingCheckListsByBreedingIdAndPhase(breedingId, 1);
            if (breedingCheckLists.Any())
            {

            }
            return Ok(breeding);
        }
    }
}
    