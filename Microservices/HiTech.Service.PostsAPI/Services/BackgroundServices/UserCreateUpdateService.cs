using AutoMapper;
using HiTech.RabbitMQ.Consumer;
using HiTech.Service.PostsAPI.Entities;
using HiTech.Service.PostsAPI.UOW;
using HiTech.Shared.Constant;
using System.Text;
using System.Text.Json;

namespace HiTech.Service.PostsAPI.Services.BackgroundServices
{
    public class UserCreateUpdateService : BackgroundService
    {
        private readonly IMessageConsumer _messageConsumer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserCreateUpdateService> _logger;

        public UserCreateUpdateService(IMessageConsumer messageConsumer, IUnitOfWork unitOfWork,
            ILogger<UserCreateUpdateService> logger, IMapper mapper)
        {
            _messageConsumer = messageConsumer;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageConsumer.CreateConsumer(MessageQueueConstants.USER_CREATE_UPDATE_QUEUE, async (model, ea) =>
            {
                _logger.LogInformation("Message received at {Time}.", DateTime.Now);
                try
                {
                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);

                    _logger.LogInformation(json);

                    var user = JsonSerializer.Deserialize<User>(json);

                    if (user != null)
                    {
                        var existUser = await _unitOfWork.Users.GetByIDAsync(user.UserId);
                        if (existUser == null)
                        {
                            // Create the new one
                            await _unitOfWork.Users.CreateAsync(user);
                            await _unitOfWork.SaveAsync();
                        }
                        else
                        {
                            //Update user
                            _mapper.Map(user, existUser);
                            _unitOfWork.Users.Update(existUser);
                            await _unitOfWork.SaveAsync();
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
