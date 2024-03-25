using BusinessObjects.IService;
using BusinessObjects.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedingCheckListDetailController : ControllerBase
    {
        private readonly IBreedingCheckListDetailService _breedingCheckListDetailService;

        public BreedingCheckListDetailController(IBreedingCheckListDetailService breedingCheckListDetailService)
        {
            _breedingCheckListDetailService = breedingCheckListDetailService ?? throw new ArgumentNullException(nameof(breedingCheckListDetailService));
        }
        [HttpGet("{breedingId}")]
        public async Task<ActionResult<List<CheckListDetailResponse>>> GetCheckListDetailsByBreedingId(int breedingId)
        {
            try
            {
                if (breedingId <= 0)
                {
                    return BadRequest("Invalid BreedingId. BreedingId must be greater than 0.");
                }

                var checkListDetails = await _breedingCheckListDetailService.GetCheckListDetailsByBreedingId(breedingId);

                if (checkListDetails == null || checkListDetails.Count == 0)
                {
                    return NotFound("No CheckListDetails found for the provided BreedingId.");
                }

                return Ok(checkListDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
