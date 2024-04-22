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
        private readonly IClutchService  _clutchService;
        private readonly IAccountService _accountService;

        public StatisticsController(IBirdService birdService, IBirdSpeciesService birdSpeciesService, ICageService cageService, IEggService eggService, IAccountService accountService, IBreedingService breedingService, IClutchService clutchService)
        {
            _birdService = birdService;
            _cageService = cageService;
            _eggService = eggService;
            _accountService = accountService;
            _breedingService = breedingService;
            _clutchService = clutchService;
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
                var breedingCount = await _breedingService.GetBreedingCountByStatusNameAndManagedByStaff(staffId, status);
                statusCounts.Add(status, breedingCount);
            }

            return Ok(statusCounts);
        }

        [HttpGet("CountBreedingByStatusForManager")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetCountBreedingByStatusNameManageByManager()
        {
            var managerId = _accountService.GetAccountIdFromToken();
            var statusCounts = new Dictionary<string, int>();

            var statuses = new List<string> { "Mating", "InProgress", "Closed", "Failed", "Cancelled" };

            foreach (var status in statuses)
            {
                var breedingCount = await _breedingService.GetBreedingCountByStatusNameAndManagedByManager(managerId, status);
                statusCounts.Add(status, breedingCount);
            }

            return Ok(statusCounts);
        }

        [HttpGet("totalBirdByFarmId")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetTotalBirdsByFarmId(int farmId)
        {
            try
            {
                var inReproductionPeriodCount = await _birdService.GetTotalBirdsByStatusAndFarmId("InReproductionPeriod", farmId);
                var inRestPeriodCount = await _birdService.GetTotalBirdsByStatusAndFarmId("InRestPeriod", farmId);
                var totalBirdBySpecies = await _birdService.GetBirdCountBySpeciesAsync(farmId);
                var totalBirdByGender = await _birdService.GetBirdCountByGenderAsync(farmId);

                int totalBirdsCount = inReproductionPeriodCount + inRestPeriodCount;

                var totalCounts = new
                {
                    InReproductionPeriodCount = inReproductionPeriodCount,
                    InRestPeriodCount = inRestPeriodCount,
                    TotalBirdsCount = totalBirdsCount,
                    totalBySpecie = totalBirdBySpecies,
                    totalByGender = totalBirdByGender

                };

                return Ok(totalCounts);
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong with server, please try again!");
            }
        }


        [HttpGet("totalCageByStatusAndFarmId")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetTotalCagesByStatusAndFarmId(int farmId)
        {
            var statusCounts = new Dictionary<string, int>();

            var statuses = new List<string> { "Nourishing", "Breeding", "Standby" };

            foreach (var status in statuses)
            {
                var cageCount = await _cageService.GetTotalCagesStatusByFarmIdAsync(status, farmId);
                statusCounts.Add(status, cageCount);
            }

            int totalCageCount = statusCounts.Values.Sum();

            statusCounts.Add("Total", totalCageCount);

            return Ok(statusCounts);
        }


        [HttpGet("TotalByManager")]
        [Authorize(Roles ="Manager")]
        public async Task<IActionResult> GetTotalCountByManagerId()
        {
            var managerId = _accountService.GetAccountIdFromToken();

            var totalBreedingCount = await _breedingService.GetTotalBreedingCountByManagerId(managerId);
            var totalClutchCount = await _clutchService.GetTotalClutchByManagerId(managerId);
            var totalEggCount = await _eggService.GetTotalEggsByManagerId(managerId);

            var response = new
            {
                 totalBreeding = totalBreedingCount, 
                 totalClutch = totalClutchCount,
                 totalEgg = totalEggCount
            };

            return Ok(response);
        }
    }
}
