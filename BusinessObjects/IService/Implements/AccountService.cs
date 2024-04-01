using AutoMapper;
using BusinessObjects.InheritanceClass;
using BusinessObjects.InheritanceClass.HandleError;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Net;

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

        public (string token, string role) CreateToken(int accountId, string role)
        {
            // Generate token using ProvideToken.Instance
            return ProvideToken.Instance.GenerateToken(accountId, role);
        }
        public async Task<Account?> LoginAsync(AccountLoginRequest account)
        {
            IEnumerable<Account> users = await GetAllAccountsAsync();
            var checkLogin = (from u in users where u.Username == account.Username && u.Password == account.Password select u)
                            .FirstOrDefault();

            if (checkLogin.Status == "Ban")
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Your Account have been banned");
            }
            if (checkLogin != null)
            {
                return checkLogin; 
            }
            return null; 
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

        public async Task<bool> CheckUsernameExist(string username)
        {
            var existingAccount = await _accountRepository.GetByUsernameAsync(username);
            if (existingAccount != null)
            {
                throw new ArgumentException("Username already exists");
            }
            return false;
        }


        public void UpdateAccount(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
