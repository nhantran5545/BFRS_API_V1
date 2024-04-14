using BusinessObjects.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReasonsController : ControllerBase
    {
        private readonly IReasonService _reasonService;

        public ReasonsController(IReasonService reasonService)
        {
            _reasonService = reasonService;
        }

        [HttpGet("Breeding/{BreedingId}")]
        public async Task<IActionResult> GetByBreedingId(int breedingId)
        {
            var reasons = await _reasonService.GetReasonsByBreedingId(breedingId);
            if(!reasons.Any())
            {
                return NotFound("Reason notfound");
            }
            return Ok(reasons);
        }
    }
}
