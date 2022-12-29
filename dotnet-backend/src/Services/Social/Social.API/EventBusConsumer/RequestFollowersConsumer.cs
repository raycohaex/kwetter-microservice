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
            _logger.LogInformation("processing");
            var isCeleb = false;

            var result = await _followRepository.GetFollowers(context.Message.UserName);
            var followers = result.Select(f => f.UserName).ToList();

            await context.RespondAsync<GetFollowersResponse>(new GetFollowersResponse
            {
                UserId = context.Message.UserId,
                UserName = context.Message.UserName,
                Followers = followers,
                IsCeleb = isCeleb
            });
        }
    }
}
