using BusinessObjects.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IBirdService _birdService;
        private readonly ICageService _cageService;
        private readonly IEggService _eggService;
        private readonly IAccountService _accountService;

        public StatisticsController(IBirdService birdService, IBirdSpeciesService birdSpeciesService, ICageService cageService, IEggService eggService, IAccountService accountService)
        {
            _birdService = birdService;
            _cageService = cageService;
            _eggService = eggService;
            _accountService = accountService;
        }

        [HttpGet("TotalByStaff")]
        [Authorize]
        public async Task<IActionResult> GetTotalBirdCountByStaffId()
        {
            var staffId = _accountService.GetAccountIdFromToken();
            var totalEggCount = await _eggService.GetTotalEggCountByStaffId(staffId);
            var totalBirdCount = await _birdService.GetTotalBirdCountByStaffId(staffId);
            var totalCageCount = await _cageService.GetTotalCagesByStaffIdAsync(staffId);

            var response = new
            {
                totalEgg = totalEggCount,
                totalBird = totalBirdCount,
                totalCage = totalCageCount
            };

            return Ok(response);
        }

        [HttpGet("CountEggByStatusForStaff")]
        [Authorize]
        public async Task<IActionResult> GetCountEggByStatusNameManageByStaff()
        {
            var staffId = _accountService.GetAccountIdFromToken();
            var statusCounts = new Dictionary<string, int>();

            var statuses = new List<string> { "Unknown", "Broken", "Empty", "Abandoned", "Dead Empryo", "Dead In Shell", "In Development", "Hatched", "Missing" };

            foreach (var status in statuses)
            {
                var eggCount = await _eggService.GetEggCountByStatusNameAndManagedByStaff(status, staffId);
                statusCounts.Add(status, eggCount);
            }

            return Ok(statusCounts);
        }
    }
}
