using HiTech.Service.NotificationAPI.DTOs.Request;
using HiTech.Service.NotificationAPI.DTOs.Response;

namespace HiTech.Service.NotificationAPI.Services.IService
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationResponse>> GetAllByUserIDAsync(int id);
        Task<NotificationResponse?> GetByIDAsync(int id);
        Task<NotificationResponse?> CreateAsync(NotificationRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAllByUserIDAsync(int id);
        Task<bool> NotificationExists(int id);
        Task<bool> ReadNotification(int id);
    }
}
