using AutoMapper;
using BusinessObjects.InheritanceClass;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.IService.Implements
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;


        public AccountService(IAccountRepository accountRepository, IConfiguration configuration, IMemoryCache memoryCache, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            ProvideToken.Initialize(_configuration, _memoryCache);
        }


        public async Task CreateAccountAsync(Account account)
        {
            await _accountRepository.AddAsync(account);
            _accountRepository.SaveChanges();
        }

        public void DeleteAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccountById(object accountId)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAsync();
        }

        public async Task<(string token, AccountResponse accountResponse)> AuthenticateAsync(AccountLoginRequest loginRequest)
        {
            var user = await _accountRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);

            if (user == null)
                return (null, null);

            var token = ProvideToken.Instance.GenerateToken(user.AccountId, user.Role);

            var account = _mapper.Map<AccountResponse>(user);

            return (token.token, account);
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
    }
}
