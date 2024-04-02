﻿using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories.Implements
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(BFRS_dbContext context) : base(context)
        {
        }

        public async Task<Account> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            return user;
        }

        public async Task<IEnumerable<Account>> GetAccountsByRoleAsync(string role)
        {
            return await _context.Accounts.Where(a => a.Role == role).ToListAsync();
        }
        public Account GetAccountById(int accountId)
        {
            return _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
        }

        public async Task<Account> GetByUsernameAsync(string username)
        {
            return await _context.Accounts
                .Include(a => a.Farm)
                .FirstOrDefaultAsync(a => a.Username == username);
        }
    }
}
