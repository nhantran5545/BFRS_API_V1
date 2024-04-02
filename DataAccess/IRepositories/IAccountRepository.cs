using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> AuthenticateAsync(string username, string password);
        Task<IEnumerable<Account>> GetAccountsByRoleAsync(string role);
        Account GetAccountById(int accountId);
        Task<Account> GetByUsernameAsync(string username);
    }
}
