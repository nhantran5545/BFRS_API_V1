using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BusinessObjects.RequestModels;
using BusinessObjects.IService;
using BusinessObjects.InheritanceClass;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using BusinessObjects.ResponseModels;
using DataAccess.Models;
using BusinessObjects.IService.Implements;

namespace BFRS_API_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        IAccountService _accountService;


        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("managers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetManagerAccounts()
        {
            try
            {
                var managerAccounts = await _accountService.GetManagerAccountsAsync();
                return Ok(managerAccounts);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("staff")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStaffAccounts()
        {
            try
            {
                var managerAccounts = await _accountService.GetStaffAccountsAsync();
                return Ok(managerAccounts);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountLoginRequest loginRequest)
        {
            var (token, account, errorMessage) = await _accountService.LoginAsync(loginRequest);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return Unauthorized(errorMessage);
            }

            return Ok(new { token, account });
        }


        [HttpPost("signup")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SignUp(AccountSignUpRequest accountSignUp)
        {
            try
            {

                if (await _accountService.CheckUsernameExist(accountSignUp.Username))
                {
                    BadRequest("Username already exists");
                }
                await _accountService.RegisterAccountAsync(accountSignUp);
                return Ok("Account registered successfully");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutAccount(int id, [FromBody] AccountUpdateRequest accountUpdate)
        {
            var account = await _accountService.GetAccountByIdAsync(accountUpdate.AccountId);
            if (account == null)
            {
                return NotFound("Account not found");
            }

            if (await _accountService.UpdateAccount(accountUpdate))
            {
                return Ok(accountUpdate);
            }
            return BadRequest("Something wrong with the server Please try again");
        }



        [HttpPut("status/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangStatusAccount(int id)
        {
            try
            {
                var account = await _accountService.ChangStatusAccountById(id);

                if (account == null)
                {
                    return NotFound("Account not found.");
                }

                if (!User.IsInRole("Admin"))
                {
                    return Forbid(); // Returns 403 Forbidden status code
                }

                if (account)
                {
                    return Ok("Change status successfully.");
                }
                return NotFound($"Account with ID {id} not found.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }
        // GET: api/Areas/5
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProfileResponse>> GetProfileAccountById()
        {
            var accId =  _accountService.GetAccountIdFromToken();
            var profileDetail = await _accountService.GetProfileAccountByIdAsync(accId);
            return Ok(profileDetail);
        }

        [HttpGet("staff/farm")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetStaffByFarm()
        {
            try
            {
                var managerAccountId = _accountService.GetAccountIdFromToken();
                if (managerAccountId == null)
                {
                    return BadRequest("Invalid ManagerId");
                }
                if (!_accountService.IsManager(managerAccountId))
                {
                    return Forbid("You are not authorized to access this resource.");
                }

                var staffAccounts = await _accountService.GetStaffByFarmAsync(managerAccountId);
                return Ok(staffAccounts);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
