using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using DataAccess.Models;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IAccountService
    {

        Task<(string token, AccountResponse accountResponse, string errorMessage)> LoginAsync(AccountLoginRequest loginRequest);
        Task<bool> UpdateAccount(AccountUpdateRequest accountUpdate);
        Task<AccountDetailResponse?> GetAccountByIdAsync(object accId);
        Task<bool> InActiveAccountById(int accId);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<bool> CheckUsernameExist(string username);
        Task<int> GetAccountIdFromToken();
        Task RegisterAccountAsync(AccountSignUpRequest accountSignUp);
        Task<IEnumerable<AccountDetailResponse>> GetManagerAccountsAsync();
        Task<IEnumerable<AccountDetailResponse>> GetStaffAccountsAsync();
    }
}
