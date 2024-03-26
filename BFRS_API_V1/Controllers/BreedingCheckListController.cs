using BusinessObjects.IService;
using BusinessObjects.ResponseModels;
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

        [HttpGet("{breedingId}")]
        public async Task<ActionResult<List<BreedingCheckListResponse>>> GetBreedingCheckListDetailsByBreedingId(int breedingId)
        {
            try
            {
                var breedingCheckListDetails = await _breedingCheckListService.GetBreedingCheckListDetailsByBreedingId(breedingId);
                if (breedingCheckListDetails == null || breedingCheckListDetails.Count == 0)
                {
                    return NotFound("No BreedingCheckList details found for the provided BreedingId.");
                }
                return Ok(breedingCheckListDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
