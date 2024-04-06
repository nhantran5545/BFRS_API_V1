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
            var (token, account) = await _accountService.AuthenticateAsync(loginRequest);
            if (token == null || account == null)
                return Unauthorized();

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

    }
}
