using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DataAccess.Models;

namespace BusinessObjects.IService
{
    public interface IAccountService
    {

        Task<(string token, AccountResponse accountResponse)> LoginAsync(AccountLoginRequest loginRequest);
        Task<bool> UpdateAccount(AccountUpdateRequest accountUpdate);
        Task<AccountDetailResponse?> GetAccountByIdAsync(object accId);
        void DeleteAccountById(object accountId);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<bool> CheckUsernameExist(string username);
        Task<int> GetAccountIdFromToken();
        Task RegisterAccountAsync(AccountSignUpRequest accountSignUp);
        Task<IEnumerable<AccountDetailResponse>> GetManagerAccountsAsync();
        Task<IEnumerable<AccountDetailResponse>> GetStaffAccountsAsync();
    }
}
