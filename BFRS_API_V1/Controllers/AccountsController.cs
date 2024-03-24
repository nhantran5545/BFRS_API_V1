using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
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
        private readonly IMapper _mapper;


        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountLoginRequest login)
        {
            var user = await _accountService.LoginAsync(login);
            if (user != null)
            {
                var token = _accountService.CreateToken(user.AccountId, user.Role);
                return Ok(token);
            }
            else
            {
                return BadRequest("Login failed");
            }
        }

    }
}
