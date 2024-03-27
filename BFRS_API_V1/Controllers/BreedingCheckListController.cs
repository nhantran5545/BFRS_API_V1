using BusinessObjects.IService;
using BusinessObjects.IService.Implements;
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

        public BreedingCheckListController(IBreedingCheckListService breedingCheckListService)
        {
            _breedingCheckListService = breedingCheckListService ?? throw new ArgumentNullException(nameof(breedingCheckListService));
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
    }
}
    