using BusinessObjects.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeLineController : ControllerBase
    {
        private readonly IReasonService _reasonService;

        public TimeLineController(IReasonService reasonService)
        {
            _reasonService = reasonService;
        }

        [HttpGet("Breeding/{BreedingId}")]
        public async Task<IActionResult> GetByBreedingId(int breedingId)
        {
            var reasons = await _reasonService.GetStatusByBreedingId(breedingId);
            if(!reasons.Any())
            {
                return NotFound("Reason notfound");
            }
            return Ok(reasons);
        }
    }
}
