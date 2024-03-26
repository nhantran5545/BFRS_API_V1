using BusinessObjects.IService;
using BusinessObjects.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedingCheckListDetailController : ControllerBase
    {
        private readonly IBreedingCheckListDetailService _breedingCheckListDetailService;

        public BreedingCheckListDetailController(IBreedingCheckListDetailService breedingCheckListDetailService)
        {
            _breedingCheckListDetailService = breedingCheckListDetailService ?? throw new ArgumentNullException(nameof(breedingCheckListDetailService));
        }

    }
}
