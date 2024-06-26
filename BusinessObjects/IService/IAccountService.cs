﻿using BusinessObjects.RequestModels.AccountReqModels;
using BusinessObjects.ResponseModels;
using BusinessObjects.ResponseModels.AccountResModels;
using DataAccess.Models;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IAccountService
    {

        Task<(string token, AccountResponse accountResponse, string errorMessage)> LoginAsync(AccountLoginRequest loginRequest);
        Task<bool> UpdateAccount(AccountUpdateRequest accountUpdate);
        Task<AccountDetailResponse?> GetAccountByIdAsync(object accId);
        Task<IEnumerable<AccountDetailResponse>> GetStaffByFarmAsync(int managerId);
        Task<bool> ChangStatusAccountById(int accId);
        int GetAccountIdFromToken();
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<bool> CheckUsernameExist(string username);
        Task RegisterAccountAsync(AccountSignUpRequest accountSignUp);
        Task<IEnumerable<AccountDetailResponse>> GetManagerAccountsAsync();
        Task<IEnumerable<AccountDetailResponse>> GetStaffAccountsAsync();
        Task<ProfileResponse?> GetProfileAccountByIdAsync(int accId);
        Task<bool> UpdateAdminProfile(int accountId, AdminUpdateRequest accountUpdate);
        bool IsManager(int accountId);
        bool IsStaff(int accountId);
    }
}
