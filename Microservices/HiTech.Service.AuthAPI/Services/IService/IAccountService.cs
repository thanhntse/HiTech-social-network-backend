using HiTech.Service.AuthAPI.DTOs.Request;
using HiTech.Service.AuthAPI.DTOs.Response;

namespace HiTech.Service.AuthAPI.Services.IService
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountResponse>> GetAllAsync();
        Task<AccountResponse?> GetByIDAsync(int id);
        Task<AccountResponse?> GetByEmailAsync(string email);
        Task<AccountResponse?> CreateAsync(AccountCreationRequest request);
        Task<bool> UpdateAsync(int id, AccountUpdationRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> AccountExists(int id);
        Task<bool> AccountExists(string email);
    }
}
