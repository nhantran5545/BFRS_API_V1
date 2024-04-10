                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   using BusinessObjects.IService;
using BusinessObjects.IService.Implements;
using BusinessObjects.RequestModels;
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
        private readonly IAccountService _accountService;

        public AreaController(IAreaService areaService, IAccountService accountService)
        {
            _areaService = areaService;
            _accountService = accountService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> CreateArea(AreaAddRequest areaAddRequest)
        {
            try
            {
                var areaId = await _areaService.CreateAreaAsync(areaAddRequest);
                return Ok("add successful");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: api/Areas/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<AreaResponse>> GetAreaById(int id)
        {
            var bird = await _areaService.GetAreaByIdAsync(id);
            if (bird == null)
            {
                return NotFound("Bird not found");
            }
            return Ok(bird);
        }

        [HttpGet("GetAreasByFarmId")]
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult GetAreasByFarm()
        {
            try
            {
                var managerId = _accountService.GetAccountIdFromToken();
                if (!_accountService.IsManager(managerId))
                {
                    throw new UnauthorizedAccessException("User is not authorized to access this resource.");
                }
                var areas = _areaService.GetAreaByManagerId(managerId);
                return Ok(areas);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
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
