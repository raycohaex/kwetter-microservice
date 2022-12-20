using Eventbus.Messages.Events.Integration.GetFollowers;
using MassTransit;
using Social.Infrastructure.Repositories;

namespace Social.API.EventBusConsumer
{
    public class RequestFollowersConsumer : IConsumer<GetFollowersRequest>
    {
        private readonly ILogger<RequestFollowersConsumer> _logger;
        private readonly IRequestClient<GetFollowersRequest> _client;
        private readonly IFollowRepository _followRepository;

        public RequestFollowersConsumer(ILogger<RequestFollowersConsumer> logger, IRequestClient<GetFollowersRequest> client, IFollowRepository followRepository)
        {
            _logger = logger;
            _client = client;
            _followRepository = followRepository;
        }

        public async Task Consume(ConsumeContext<GetFollowersRequest> context)
        {
            // use different kweet entity
            _logger.Log(LogLevel.Information, $"request received {context.Message}");

            //_followRepository.GetFollowers();

            await context.RespondAsync<GetFollowersResponse>(new GetFollowersResponse
            {
                UserId = context.Message.UserId,
                Followers = new List<Guid>
                {
                    new Guid(),
                    new Guid(),
                    new Guid(),
                }
            });
        }
    }
}
