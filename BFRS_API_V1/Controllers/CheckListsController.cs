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

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckListsController : ControllerBase
    {
        private readonly ICheckListService _checkListService;

        public CheckListsController(ICheckListService checkListService)
        {
            _checkListService = checkListService;
        }

        // GET: api/CheckLists
        [HttpGet]
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
        public async Task<ActionResult<CheckList>> GetCheckList(Guid id)
        {
            var checkList = await _checkListService.GetCheckListByIdAsync(id);
            if(checkList == null)
            {
                return NotFound("Checklist not found");
            }
            return Ok(checkList);
        }

        // PUT: api/CheckLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutCheckList(Guid id, CheckList checkList)
        {

            return NoContent();
        }

        // POST: api/CheckLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CheckList>> PostCheckList(CheckList checkList)
        {

            return CreatedAtAction("GetCheckList", new { id = checkList.CheckListId }, checkList);
        }

        // DELETE: api/CheckLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckList(Guid id)
        {

            return NoContent();
        }*/



    }
}
