using BusinessObjects.IService;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckListDetailController : ControllerBase
    {
        private readonly ICheckListDetailService _checkListDetailService;

        public CheckListDetailController(ICheckListDetailService checkListDetailService)
        {
            _checkListDetailService = checkListDetailService ?? throw new ArgumentNullException(nameof(checkListDetailService));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCheckListAsync([FromBody] CheckListDetailRequest checkListDetailRequest)
        {
            if (checkListDetailRequest == null)
            {
                return BadRequest("Invalid data");
            }
            try
            {
                var createdCheckListDetail = await _checkListDetailService.CreateCheckListAsync(checkListDetailRequest);
                return Ok(createdCheckListDetail);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{checkListId}")]
        public async Task<ActionResult<List<CheckListDetailResponse>>> GetCheckListDetailsByCheckListId(int checkListId)
        {
            try
            {
                if (checkListId <= 0)
                {
                    return BadRequest("Invalid CheckListId. CheckListId must be greater than 0.");
                }

                var checkListDetails = await _checkListDetailService.GetCheckListDetailsByCheckListId(checkListId);

                if (checkListDetails == null || checkListDetails.Count == 0)
                {
                    return NotFound("No CheckListDetails found for the provided CheckListId.");
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
