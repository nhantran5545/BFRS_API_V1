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
            var (token, account) = await _accountService.LoginAsync(loginRequest);
            if ( account == null)
                return Unauthorized("Username or password are not correct");

            return Ok(new { token, account });
        }

        [HttpPost("signup")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SignUp(AccountSignUpRequest accountSignUp)
        {
            try
            {
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
            var account  = await _accountService.GetAccountByIdAsync(accountUpdate.AccountId);
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

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> InActivateAccount(int id)
        {
            try
            {
                var account = await _accountService.InActiveAccountById(id);

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
                    return Ok("Account deactivated successfully.");
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


    }
}
