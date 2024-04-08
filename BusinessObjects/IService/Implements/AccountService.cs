using AutoMapper;
using BusinessObjects.InheritanceClass;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace BusinessObjects.IService.Implements
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AccountService(IAccountRepository accountRepository, IConfiguration configuration, IMemoryCache memoryCache, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            ProvideToken.Initialize(_configuration, _memoryCache);
        }


        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAsync();
        }

        public async Task<(string token, AccountResponse accountResponse)> LoginAsync(AccountLoginRequest loginRequest)
        {
            var account = await _accountRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);
        
            if (account != null)
            {
                if (account.Status != "INACTIVE")
                {
                    var token = ProvideToken.Instance.GenerateToken(account.AccountId, account.Role);

                    var accountResponse = _mapper.Map<AccountResponse>(account);

                        return (token.token, accountResponse);
                }
                else
                {
                    throw new UnauthorizedAccessException("Your account is not active, please contact Admin");
                }
            }
            else
            {
                return (null, null);
            }
        }


        public async Task RegisterAccountAsync(AccountSignUpRequest accountSignUp)
        {
            var validationContext = new ValidationContext(accountSignUp, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(accountSignUp, validationContext, validationResults, validateAllProperties: true))
            {
                var validationErrors = string.Join(", ", validationResults.Select(r => r.ErrorMessage));
                throw new ArgumentException($"Validation failed: {validationErrors}");
            }

            if (accountSignUp.Role != "Manager" && accountSignUp.Role != "Staff")
            {
                throw new ArgumentException("Invalid role. Only 'Manager' or 'Staff' roles are allowed.");
            }

            if (await CheckUsernameExist(accountSignUp.Username))
            {
                throw new ArgumentException("Username already exists");
            }

            var account = _mapper.Map<Account>(accountSignUp);
            account.Status = "Active";

            await _accountRepository.AddAsync(account);
            _accountRepository.SaveChanges();
        }

        public async Task<int> GetAccountIdFromToken()
        {
            int result = 0;
            if (_httpContextAccessor.HttpContext != null)
            {
                var token = _httpContextAccessor.HttpContext.Request.Headers[" "].FirstOrDefault()?.Split(" ").Last();
                if (!string.IsNullOrEmpty(token))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenS = tokenHandler.ReadJwtToken(token);
                    var accountId = tokenS.Claims.FirstOrDefault(claim => claim.Type == "AccountId");
                    if (accountId != null && int.TryParse(accountId.Value, out int accountIdValue))
                    {
                        result = accountIdValue;
                    }
                }
            }
            return result;
        }

        public async Task<IEnumerable<AccountDetailResponse>> GetManagerAccountsAsync()
        {
            var managerAccounts = await _accountRepository.GetAccountsByRoleAsync("Manager");
            return _mapper.Map<IEnumerable<AccountDetailResponse>>(managerAccounts);
        }

        public async Task<IEnumerable<AccountDetailResponse>> GetStaffAccountsAsync()
        {
            var managerAccounts = await _accountRepository.GetAccountsByRoleAsync("Staff");
            return _mapper.Map<IEnumerable<AccountDetailResponse>>(managerAccounts);
        }

        public async Task<bool> CheckUsernameExist(string username)
        {
            var existingAccount = await _accountRepository.GetByUsernameAsync(username);
            if (existingAccount != null)
            {
                throw new ArgumentException("Username already exists");
            }
            return false;
        }

        public async Task<AccountDetailResponse?> GetAccountByIdAsync(object accId)
        {
            var acc = await _accountRepository.GetByIdAsync(accId);
            return _mapper.Map<AccountDetailResponse>(acc);
        }




        public async Task<bool> UpdateAccount(AccountUpdateRequest accountUpdate)
        {
            var account = await _accountRepository.GetByIdAsync(accountUpdate.AccountId);
            if (account == null)
            {
                return false;
            }

            account.FirstName = accountUpdate.FirstName;
            account.LastName = accountUpdate.LastName;
            account.PhoneNumber = accountUpdate.PhoneNumber;
            account.City = accountUpdate.City;
            account.Address = accountUpdate.Address;

            var result = _accountRepository.SaveChanges();
            if (result < 1)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> InActiveAccountById(int accId )
        {

            var account = await _accountRepository.GetByIdAsync(accId);
            if (account == null)
            {
                return false;
            }

            account.Status = "INACTIVE";

            var result = _accountRepository.SaveChanges();
            if (result < 1)
            {
                return false;
            }
            return true;
        }
    }
}
