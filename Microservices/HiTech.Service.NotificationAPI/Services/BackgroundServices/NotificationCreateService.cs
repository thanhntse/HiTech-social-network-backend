using HiTech.RabbitMQ.Consumer;
using HiTech.Service.NotificationAPI.DTOs.Request;
using HiTech.Service.NotificationAPI.Services.IService;
using HiTech.Shared.Constant;
using System.Text;
using System.Text.Json;

namespace HiTech.Service.NotificationAPI.Services.BackgroundServices
{
    public class NotificationCreateService : BackgroundService
    {
        private readonly IMessageConsumer _messageConsumer;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NotificationCreateService> _logger;

        public NotificationCreateService(IMessageConsumer messageConsumer, IServiceProvider serviceProvider,
            ILogger<NotificationCreateService> logger)
        {
            _messageConsumer = messageConsumer;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageConsumer.CreateConsumer(MessageQueueConstants.NOTI_CREATE_QUEUE, async (model, ea) =>
            {
                _logger.LogInformation("========Message received at {Time}.========", DateTime.Now);
                try
                {
                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);

                    _logger.LogInformation(json);

                    var noti = JsonSerializer.Deserialize<NotificationRequest>(json);

                    if (noti != null)
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                            await notificationService.CreateAsync(noti);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the notification at {Time}.", DateTime.Now);
                }
            });

            return Task.CompletedTask;
        }
    }
}
