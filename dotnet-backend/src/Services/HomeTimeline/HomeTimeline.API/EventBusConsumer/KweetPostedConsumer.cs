using AutoMapper;
using Eventbus.Messages.Events;
using Eventbus.Messages.Events.Integration.GetFollowers;
using HomeTimeline.Application.Contracts;
using Kweet.Domain.Entities;
using MassTransit;
using MassTransit.Mediator;

namespace HomeTimeline.API.EventBusConsumer
{
    public class KweetPostedConsumer : IConsumer<KweetPostedEvent>
    {
        private readonly ILogger<KweetPostedConsumer> _logger;
        private readonly IRequestClient<GetFollowersRequest> _client;
        private readonly IHomeTimelineService _homeTimelineService;

        public KweetPostedConsumer(ILogger<KweetPostedConsumer> logger, IRequestClient<GetFollowersRequest> client, IHomeTimelineService homeTimelineService)
        {
            _logger = logger;
            _client = client;
            _homeTimelineService = homeTimelineService;
        }

        public async Task Consume(ConsumeContext<KweetPostedEvent> context)
        {
            // use different kweet entity
            _logger.Log(LogLevel.Information, $"kweet received {context.Message}");

            // Make a request/response call to the social API to get all followers.
            var response = await _client.GetResponse<GetFollowersResponse>(new { UserId = context.Message.Id, UserName = context.Message.UserName});
            if(response.Message.IsCeleb == true)
            {
                // Celiberty is someone with a large following in this case.
                // In case it's a celiberty return without updating anything. This prevents large N+1 operations.
                // Celeberties are queried a different way.
                return;
            }
            var followers = response.Message.Followers;

            // TODO: mapping
            var kweet = new KweetEntity();
            kweet.TweetBody = context.Message.TweetBody;
            kweet.UserName = context.Message.UserName;
            kweet.CreatedOn = context.Message.CreatedOn;

            try
            {
                _homeTimelineService.UpdateTimelines(followers, kweet);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error updating the timeline of tweet {context.Message.Id} amount of followers: {followers.Count}");
            }
        }
    }
}
