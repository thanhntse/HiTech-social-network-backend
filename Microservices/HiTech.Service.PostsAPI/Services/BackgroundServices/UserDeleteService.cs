using HiTech.RabbitMQ.Consumer;
using HiTech.Service.PostsAPI.UOW;
using HiTech.Shared.Constant;
using System.Text;

namespace HiTech.Service.PostsAPI.Services.BackgroundServices
{
    public class UserDeleteService : BackgroundService
    {
        private readonly IMessageConsumer _messageConsumer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserCreateUpdateService> _logger;

        public UserDeleteService(IMessageConsumer messageConsumer, IUnitOfWork unitOfWork,
            ILogger<UserCreateUpdateService> logger)
        {
            _messageConsumer = messageConsumer;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageConsumer.CreateConsumer(MessageQueueConstants.USER_DELETE_QUEUE, async (model, ea) =>
            {
                _logger.LogInformation("Message received at {Time}.", DateTime.Now);
                try
                {
                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);

                    _logger.LogInformation(json);

                    var userId = Int32.Parse(json);

                    var user = await _unitOfWork.Users.GetByIDAsync(userId);

                    if (user != null)
                    {
                        _unitOfWork.Users.Delete(user);
                        await _unitOfWork.SaveAsync();
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
