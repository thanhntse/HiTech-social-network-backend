using HiTech.RabbitMQ.Consumer;
using HiTech.Service.GroupAPI.UOW;
using HiTech.Shared.Constant;
using System.Text;

namespace HiTech.Service.GroupAPI.Services.BackgroundServices
{
    public class UserDeleteService : BackgroundService
    {
        private readonly IMessageConsumer _messageConsumer;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<UserDeleteService> _logger;

        public UserDeleteService(IMessageConsumer messageConsumer, IServiceProvider serviceProvider,
            ILogger<UserDeleteService> logger)
        {
            _messageConsumer = messageConsumer;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageConsumer.CreateConsumer(MessageQueueConstants.USER_DELETE_QUEUE, async (model, ea) =>
            {
                _logger.LogInformation("========Message received at {Time}.========", DateTime.Now);
                try
                {
                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);

                    _logger.LogInformation(json);

                    var userId = Int32.Parse(json);

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                        var user = await unitOfWork.Users.GetByIDAsync(userId);

                        if (user != null)
                        {
                            unitOfWork.Users.Delete(user);
                            await unitOfWork.SaveAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting the user at {Time}.", DateTime.Now);
                }
            });

            return Task.CompletedTask;
        }
    }
}
