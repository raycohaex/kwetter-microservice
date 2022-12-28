using Eventbus.Messages.Events.Integration.AuthRegistered;
using MassTransit;

namespace Social.API.EventBusConsumer
{
    public class RegisterEventConsumer : IConsumer<RegisterEvent>
    {
        private readonly ILogger<RegisterEventConsumer> _logger;

        public RegisterEventConsumer(ILogger<RegisterEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RegisterEvent> context)
        {
            var username = context.Message.Username;
            var userId = context.Message.UserId;

            // You can do something with the username and userId here, such as storing them in a database or processing them further

            _logger.LogInformation($"Received register event for user {username} with ID {userId}");
        }
    }





}
