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

        [HttpGet("{breedingId}/{phase}")]
        public IActionResult GetBreedingCheckList(int breedingId, int phase)
        {
            var breedingCheckList = _breedingCheckListService.GetBreedingCheckList(breedingId, phase);
            if (breedingCheckList == null)
            {
                return NotFound();
            }
            return Ok(breedingCheckList);
        }
    }
}
    