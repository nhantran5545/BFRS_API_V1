using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using BusinessObjects.IService;
using BusinessObjects.ResponseModels;
using Microsoft.AspNetCore.Authorization;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckListsController : ControllerBase
    {
        private readonly ICheckListService _checkListService;
        private readonly IAccountService _accountService;

        public CheckListsController(ICheckListService checkListService , IAccountService accountService)
        {
            _checkListService = checkListService;
            _accountService = accountService;
        }

        // GET: api/CheckLists
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CheckList>>> GetCheckLists()
        {
            var checkLists = await _checkListService.GetAllCheckListsAsync();
            if(checkLists == null)
            {
                return NotFound("CheckList not found");
            }
            return Ok(checkLists);
        }


        // GET: api/CheckLists/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CheckList>> GetCheckList(Guid id)
        {
            var checkList = await _checkListService.GetCheckListByIdAsync(id);
            if(checkList == null)
            {
                return NotFound("Checklist not found");
            }
            return Ok(checkList);
        }




    }
}
