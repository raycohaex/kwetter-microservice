using AutoMapper;
using Eventbus.Messages.Events;
using Eventbus.Messages.Events.Integration.GetFollowers;
using Kweet.Domain.Entities;
using MassTransit;
using MassTransit.Mediator;

namespace HomeTimeline.API.EventBusConsumer
{
    public class KweetPostedConsumer : IConsumer<KweetPostedEvent>
    {
        private readonly ILogger<KweetPostedConsumer> _logger;
        private readonly IRequestClient<GetFollowersRequest> _client;

        public KweetPostedConsumer(ILogger<KweetPostedConsumer> logger, IRequestClient<GetFollowersRequest> client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Consume(ConsumeContext<KweetPostedEvent> context)
        {
            // use different kweet entity
            _logger.Log(LogLevel.Information, $"kweet received {context.Message}");

            var response = await _client.GetResponse<GetFollowersResponse>(new { UserId = context.Message.Id});

            var followers = response.Message.Followers;
            var followersString = "[" + string.Join(",", followers.Select(f => f.ToString())) + "]";

            _logger.Log(LogLevel.Information, followersString);
        }
    }
}
