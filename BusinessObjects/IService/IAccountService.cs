using BusinessObjects.InheritanceClass;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IAccountService
    {
        Task<Account?> LoginAsync(AccountLoginRequest account);
        (string token, string role) CreateToken(int accountId, string role);
        Task CreateAccountAsync(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(Account account);
        void DeleteAccountById(object accountId);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<bool> CheckUsernameExist(string username);
        Task RegisterAccountAsync(AccountSignUpRequest accountSignUp);
    }
}
