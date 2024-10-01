using AutoMapper;
using HiTech.RabbitMQ.Consumer;
using HiTech.Service.GroupAPI.Entities;
using HiTech.Service.GroupAPI.UOW;
using HiTech.Shared.Constant;
using System.Text;
using System.Text.Json;

namespace HiTech.Service.GroupAPI.Services.BackgroundServices
{
    public class UserCreateUpdateService : BackgroundService
    {
        private readonly IMessageConsumer _messageConsumer;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        private readonly ILogger<UserCreateUpdateService> _logger;

        public UserCreateUpdateService(IMessageConsumer messageConsumer, IServiceProvider serviceProvider,
            ILogger<UserCreateUpdateService> logger, IMapper mapper)
        {
            _messageConsumer = messageConsumer;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _mapper = mapper;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageConsumer.CreateConsumer(MessageQueueConstants.USER_CREATE_UPDATE_QUEUE, async (model, ea) =>
            {
                _logger.LogInformation("========Message received at {Time}.========", DateTime.Now);
                try
                {
                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);

                    _logger.LogInformation(json);

                    var user = JsonSerializer.Deserialize<User>(json);

                    if (user != null)
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                            var existUser = await unitOfWork.Users.GetByIDAsync(user.UserId);
                            if (existUser == null)
                            {
                                // Create the new one
                                await unitOfWork.Users.CreateAsync(user);
                                await unitOfWork.SaveAsync();
                            }
                            else
                            {
                                //Update user
                                _mapper.Map(user, existUser);
                                unitOfWork.Users.Update(existUser);
                                await unitOfWork.SaveAsync();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating or updating the user at {Time}.", DateTime.Now);
                }
            });

            return Task.CompletedTask;
        }
    }
}
