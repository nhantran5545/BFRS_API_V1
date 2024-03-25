using BusinessObjects.IService;
using BusinessObjects.RequestModels;
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
            _checkListDetailService = checkListDetailService;
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
    }
}
