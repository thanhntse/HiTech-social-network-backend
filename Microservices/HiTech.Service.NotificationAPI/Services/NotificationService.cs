using AutoMapper;
using HiTech.Service.NotificationAPI.DTOs.Request;
using HiTech.Service.NotificationAPI.DTOs.Response;
using HiTech.Service.NotificationAPI.Entities;
using HiTech.Service.NotificationAPI.Services.IService;
using HiTech.Service.NotificationAPI.UOW;

namespace HiTech.Service.NotificationAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<NotificationResponse?> CreateAsync(NotificationRequest request)
        {
            var noti = _mapper.Map<Notification>(request);

            Notification? response;
            try
            {
                response = await _unitOfWork.Notifications.CreateAsync(noti);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the notification at {Time}.", DateTime.Now);
                return null;
            }
            return _mapper.Map<NotificationResponse>(response);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            var noti = await _unitOfWork.Notifications.GetByIDAsync(id);

            if (noti != null)
            {
                try
                {
                    _unitOfWork.Notifications.Delete(noti);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting the notification at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            return result;
        }

        public async Task<IEnumerable<NotificationResponse>> GetAllByUserIDAsync(int id)
        {
            var notis = await _unitOfWork.Notifications.FindAllAsync(n => n.UserId == id);
            return _mapper.Map<IEnumerable<NotificationResponse>>(notis);
        }

        public async Task<NotificationResponse?> GetByIDAsync(int id)
        {
            var noti = await _unitOfWork.Notifications.GetByIDAsync(id);

            if (noti == null)
            {
                return null;
            }

            return _mapper.Map<NotificationResponse>(noti);
        }

        public async Task<bool> NotificationExists(int id)
        {
            var noti = await _unitOfWork.Notifications.GetByIDAsync(id);
            return noti != null;
        }

        public async Task<bool> ReadNotification(int id)
        {
            bool result = false;
            var noti = await _unitOfWork.Notifications.GetByIDAsync(id);

            if (noti != null)
            {
                try
                {
                    noti.IsRead = true;
                    _unitOfWork.Notifications.Update(noti);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while reading the notification at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            return result;
        }
    }
}
