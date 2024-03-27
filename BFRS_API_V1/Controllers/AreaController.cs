                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   using BusinessObjects.IService;
using BusinessObjects.IService.Implements;
using BusinessObjects.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }


        // GET: api/Areas/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AreaResponse>> GetAreaById(int id)
        {
            var bird = await _areaService.GetAreaByIdAsync(id);
            if (bird == null)
            {
                return NotFound("Bird not found");
            }
            return Ok(bird);
        }

        [HttpGet("GetAreasByFarmId/{managerId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAreasByFarmId(int managerId)
        {
            try
            {
                var areas = _areaService.GetAreaByManagerId(managerId);
                return Ok(areas);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<AreaResponse>>> GetAllArea()
        {
            try
            {
                var areas = await _areaService.GetAllAreaAsync();

                if (areas == null || !areas.Any())
                {
                    return NotFound("There are no area");
                }

                return Ok(areas);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(403, "You are not authorized to access this resource.");
            }
        }

    }
}
