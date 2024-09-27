using HiTech.Service.NotificationAPI.Data;
using HiTech.Service.NotificationAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;

namespace HiTech.Service.NotificationAPI.Repositories
{
    public interface INotificationRepository : IGenericRepository<Notification, int>
    {
    }

    public sealed class NotificationRepository
        : GenericRepository<NotificationDbContext, Notification, int>, INotificationRepository
    {
        public NotificationRepository(NotificationDbContext context) : base(context)
        {
        }
    }
}
