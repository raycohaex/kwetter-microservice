using Eventbus.Messages.Events.Integration.AuthRegistered;
using MassTransit;
using Social.Application.Contracts;
using Social.Application.Dto;

namespace Social.API.EventBusConsumer
{
    public class RegisterEventConsumer : IConsumer<EventClientNotificationMqMsg>
    {
        private readonly ILogger<RegisterEventConsumer> _logger;
        private readonly IFollowService _followService;

        public RegisterEventConsumer(ILogger<RegisterEventConsumer> logger, IFollowService followService)
        {
            _logger = logger;
            _followService = followService;
        }

        public async Task Consume(ConsumeContext<EventClientNotificationMqMsg> context)
        {
            var username = context.Message.Details.Username;
            var userId = context.Message.UserId;

            var id = new Guid(userId);

            var user = new UserDto();
            user.Id = id;
            user.UserName = username;

            try
            {
                await _followService.CreateUserNode(user);
            }
            catch(Exception e)
            {
                _logger.LogWarning($"Failed to create user {userId} as node. Reason: {e.Message}");

            }

        }
    }
}
