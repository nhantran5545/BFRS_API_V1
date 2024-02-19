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
        Task Login(string username, string password);
        Task CreateAccountAsync(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(Account account);
        void DeleteAccountById(object accountId);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByEmailAsync(string email);
    }
}
