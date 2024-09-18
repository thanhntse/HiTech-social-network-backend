using HiTech.Service.AccountAPI.Entities;

namespace HiTech.Service.AccountAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<Entities.User>> GetAllAsync();
        Task<Entities.User> GetByIdAsync(int id);
        Task AddAsync(Entities.User user);
        Task UpdateAsync(Entities.User user);
        Task DeleteAsync(Entities.User user);
    }
}
