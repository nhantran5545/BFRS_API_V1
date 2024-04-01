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
        public Account GetAccountById(int accountId)
        {
            return _context.Accounts.FirstOrDefault(a => a.AccountId == accountId);
        }

        public async Task<Account> GetByUsernameAsync(string username)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Username == username);
        }
    }
}
