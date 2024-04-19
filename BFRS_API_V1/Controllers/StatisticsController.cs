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
        private readonly IBreedingService _breedingService;
        private readonly IAccountService _accountService;

        public StatisticsController(IBirdService birdService, IBirdSpeciesService birdSpeciesService, ICageService cageService, IEggService eggService, IAccountService accountService, IBreedingService breedingService)
        {
            _birdService = birdService;
            _cageService = cageService;
            _eggService = eggService;
            _accountService = accountService;
            _breedingService = breedingService;
        }

        [HttpGet("TotalByStaff")]
        [Authorize]
        public async Task<IActionResult> GetTotalCountByStaffId()
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

        [HttpGet("TotalByManager")]
        [Authorize(Roles ="Admin, Manager")]
        public async Task<IActionResult> GetTotalCountByManagerId(int farmId)
        {
            var managerId = _accountService.GetAccountIdFromToken();
            var totalBirdCount = await _birdService.GetTotalBirdByAccountIdAsync();
            var totalCageCount = await _cageService.GetTotalCagesByManagerIdAsync(farmId);
            var totalBreedingCount = await _breedingService.GetTotalBreedingCountByManagerId(managerId);

            var response = new
            {
                totalBird = totalBirdCount,
                totalCage = totalCageCount,
                totalBreeding = totalBreedingCount
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

        [HttpGet("CountBreedingByStatusForStaff")]
        [Authorize]
        public async Task<IActionResult> GetCountBreedingByStatusNameManageByStaff()
        {
            var staffId = _accountService.GetAccountIdFromToken();
            var statusCounts = new Dictionary<string, int>();

            var statuses = new List<string> { "Mating", "InProgress", "Closed", "Failed", "Cancelled"};

            foreach (var status in statuses)
            {
                var eggCount = await _breedingService.GetBreedingCountByStatusNameAndManagedByStaff(staffId, status);
                statusCounts.Add(status, eggCount);
            }

            return Ok(statusCounts);
        }
    }
}
