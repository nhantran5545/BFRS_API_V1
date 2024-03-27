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

        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountLoginRequest login)
        {
            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var user = await _accountService.LoginAsync(login);
            if (user != null)
            {
                var (token, role) = _accountService.CreateToken(user.AccountId, user.Role);
                return Ok(new { token, role });
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }

    }
}
