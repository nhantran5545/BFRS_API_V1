using BusinessObjects.IService;
using BusinessObjects.RequestModels;
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
        private readonly IClutchService _clutchService;
        private readonly ICheckListService _checkListService;

        public BreedingCheckListController(IBreedingCheckListService breedingCheckListService, IBreedingService breedingService,
            IClutchService clutchService, ICheckListService checkListService)
        {
            _breedingCheckListService = breedingCheckListService ?? throw new ArgumentNullException(nameof(breedingCheckListService));
            _breedingService = breedingService;
            _clutchService = clutchService;
            _checkListService = checkListService;
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

        [HttpGet("BreedingToday/{breedingId}")]
        public async Task<IActionResult> GetTodayBreedingCheckListByBreedingId(int breedingId)
        {
            var breeding = await _breedingService.GetBreedingById(breedingId);
            if(breeding == null)
            {
                return NotFound("Breeding not found");
            }

            if (breeding.Phase != 1)
            {
                return BadRequest("Breeding to different phase");
            }

            var breedingCheckListResponse = await _breedingCheckListService.GetTodayBreedingCheckListDetail(breedingId, breeding.Phase);
            if (breedingCheckListResponse == null)
            {
                return BadRequest("Breeding's phase is invalid");
            }

            return Ok(breedingCheckListResponse);
        }

        [HttpPost("BreedingToday/{breedingId}")]
        public async Task<IActionResult> CreateTodayBreedingCheckListByBreedingId(int breedingId, BreedingCheckListAddRequest breedingCheckListAddRequest)
        {
            if(breedingId != breedingCheckListAddRequest.BreedingId)
            {
                return BadRequest("Invalid breeding");
            }

            var breeding = await _breedingService.GetBreedingById(breedingCheckListAddRequest.BreedingId);
            if(breeding == null)
            {
                return NotFound("Breeding not found");
            }

            var checkList = await _checkListService.GetCheckListByIdAsync(breedingCheckListAddRequest.CheckListId);
            if (checkList == null)
            {
                return NotFound("CheckList not found");
            }

            if (breeding.Phase != checkList.Phase)
            {
                return BadRequest("Breeding can only be added checklist in phase " + breeding.Phase);
            }

            var result = await _breedingCheckListService.CreateBreedingCheckList(breedingCheckListAddRequest, breeding.Phase);
            if(result < 1)
            {
                return BadRequest("Something is wrong with the server. Please try again");
            }

            var breedingCheckList = await _breedingCheckListService.GetBreedingCheckListDetail(result);
            return Ok(breedingCheckList);
        }

        [HttpGet("ClutchToday/{clutchId}")]
        public async Task<IActionResult> GetTodayBreedingCheckListByClutchId(int clutchId)
        {
            var clutch = await _clutchService.GetClutchByIdAsync(clutchId);
            if (clutch == null)
            {
                return NotFound("Clutch not found");
            }

            if (clutch.Phase == 0)
            {
                return BadRequest("Clutch to closed phase");
            }

            var breedingCheckListResponse = await _breedingCheckListService.GetTodayClutchCheckListDetail(clutchId, clutch.Phase);
            if (breedingCheckListResponse == null)
            {
                return BadRequest("Clutch's phase is invalid");
            }

            return Ok(breedingCheckListResponse);
        }

        [HttpPost("ClutchToday/{clutchId}")]
        public async Task<IActionResult> CreateTodayBreedingCheckListByClutchId(int clutchId, ClutchCheckListAddRequest clutchCheckListAddRequest)
        {
            if (clutchId != clutchCheckListAddRequest.ClutchId)
            {
                return BadRequest("Invalid clutch");
            }

            var clutch = await _clutchService.GetClutchByIdAsync(clutchCheckListAddRequest.ClutchId);
            if (clutch == null)
            {
                return NotFound("Clutch not found");
            }

            var checkList = await _checkListService.GetCheckListByIdAsync(clutchCheckListAddRequest.CheckListId);
            if (checkList == null)
            {
                return NotFound("CheckList not found");
            }

            if (clutch.Phase != checkList.Phase)
            {
                return BadRequest("Clutch can only be added checklist in phase " + clutch.Phase);
            }

            var result = await _breedingCheckListService.CreateClutchCheckList(clutchCheckListAddRequest, clutch.Phase, clutch.BreedingId);
            if (result < 1)
            {
                return BadRequest("Something is wrong with the server. Please try again");
            }

            var breedingCheckList = await _breedingCheckListService.GetBreedingCheckListDetail(result);
            return Ok(breedingCheckList);
        }
    }
}
    