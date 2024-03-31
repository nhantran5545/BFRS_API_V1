using BusinessObjects.InheritanceClass;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.IService.Implements
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;


        public AccountService(IAccountRepository accountRepository, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _accountRepository = accountRepository;
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

        public Task<Account?> GetAccountByEmailAsync(string email)
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
            if (checkLogin != null)
            {
                return checkLogin; 
            }
            return null; 
        }


        public void UpdateAccount(Account account)
        {
            throw new NotImplementedException();
        }


    }
}
