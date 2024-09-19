using HiTech.Service.AccountAPI.DTOs.Request;
using HiTech.Service.AccountAPI.DTOs.Response;

namespace HiTech.Service.AccountAPI.Services
{
    public interface IAccountService
    {
        IAsyncEnumerable<AccountResponse> GetAllAsync();
        Task<AccountResponse?> GetByIDAsync(int id);
        Task<AccountResponse?> CreateAsync(AccountRequest request);
        Task<bool> UpdateAsync(int id, AccountRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> AccountExists(int id);
        Task<bool> AccountExists(string email);
    }
}
