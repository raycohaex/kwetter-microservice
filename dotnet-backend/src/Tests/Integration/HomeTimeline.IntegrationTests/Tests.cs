using Eventbus.Messages.Events;
using Eventbus.Messages.Events.Integration.GetFollowers;
using HomeTimeline.API.EventBusConsumer;
using HomeTimeline.Application.Contracts;
using HomeTimeline.Application.Features;
using HomeTimeline.Domain.Entities;
using HomeTimeline.Infrastructure.Repositories;
using Kweet.Domain.Entities;
using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using NUnit.Framework.Internal;
using System.Text.Json;

namespace HomeTimeline.IntegrationTests
{
    public class Tests
    {
        private readonly ILogger<KweetPostedConsumer> _logger;
        private readonly HttpClient _webclient;
        private readonly Mock<IRequestClient<GetFollowersRequest>> _client;
        private readonly Mock<IHomeTimelineService> _homeTimelineService;
        private readonly KweetPostedConsumer _consumer;

        public Tests()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(host =>
                {
                    host.ConfigureServices(services =>
                    {
                        services.AddMassTransitTestHarness();
                    });
                });

            _logger = Mock.Of<ILogger<KweetPostedConsumer>>();
            _client = new Mock<IRequestClient<GetFollowersRequest>>();
            _homeTimelineService = new Mock<IHomeTimelineService>();
            _consumer = new KweetPostedConsumer(_logger, _client.Object, _homeTimelineService.Object);
        }

        [Fact]
        public async void KweetPostedConsumer_Consume_ShouldConsumeEvent()
        {
            // This mapping is needed in the test
            EndpointConvention.Map<KweetPostedEvent>(new Uri("queue:tweetposted-hometimeline"));

            await using var provider = new ServiceCollection()
                .AddMassTransitTestHarness(cfg =>
                {
            // Need to explicitly name the endpoint.  Commenting out one
            // or both causes the test to fail.
                    cfg.AddConsumer<KweetPostedConsumer>()
                       .Endpoint(e => e.Name = "tweetposted-hometimeline");
                })
                .BuildServiceProvider(true);

            var harness = provider.GetRequiredService<ITestHarness>();

            await harness.Start();

            await harness.Bus.Send<KweetPostedEvent>(new
            {
                Id = Guid.NewGuid(),
                UserName = "test_user",
                TweetBody = "test",
                CreatedOn = new DateTime()
            });

            Assert.True(await harness.Sent.Any<KweetPostedEvent>());
            Assert.True(await harness.Consumed.Any<KweetPostedEvent>());
        }

        [Fact]
        public async Task timeline_stores_tweets_for_all_users()
        {
            // Arrange
            var mockRepository = new Mock<IHomeTimelineRepository>();
            var service = new HomeTimelineService(mockRepository.Object);
            var followers = new List<string> { "follower1", "follower2", "follower3" };
            var kweet = new KweetEntity
            {
                UserName = "testUser",
                TweetBody = "Test Kweet",
                CreatedOn = DateTime.Now
            };

            // Act
            await service.UpdateTimelines(followers, kweet);

            // Assert
            mockRepository.Verify(r => r.UpdateTimeline(It.Is<Timeline>(t =>
                t.UserName == "follower1" && t.Kweets.Count == 1 && t.Kweets[0].TweetBody == "Test Kweet")), Times.Once);
            mockRepository.Verify(r => r.UpdateTimeline(It.Is<Timeline>(t =>
                t.UserName == "follower2" && t.Kweets.Count == 1 && t.Kweets[0].TweetBody == "Test Kweet")), Times.Once);
            mockRepository.Verify(r => r.UpdateTimeline(It.Is<Timeline>(t =>
                t.UserName == "follower3" && t.Kweets.Count == 1 && t.Kweets[0].TweetBody == "Test Kweet")), Times.Once);
        }

        [Fact]
        public async Task timeline_stores_all_30()
        {
            // Arrange
            var mockRepository = new Mock<IHomeTimelineRepository>();
            var service = new HomeTimelineService(mockRepository.Object);
            var followers = new List<string> { "follower1", "follower2", "follower3" };
            var kweet = new KweetEntity
            {
                UserName = "testUser",
                TweetBody = "Test Kweet",
                CreatedOn = DateTime.Now
            };
            var kweetCounts = new Dictionary<string, int>();
            mockRepository
                .Setup(r => r.UpdateTimeline(It.IsAny<Timeline>()))
                .Callback<Timeline>(t => {
                    if (!kweetCounts.ContainsKey(t.UserName))
                        kweetCounts.Add(t.UserName, 0);
                    kweetCounts[t.UserName]++;
                });

            // Act
            for (var i = 0; i < 30; i++)
            {
                await service.UpdateTimelines(followers, kweet);
            }

            // Assert
            foreach (var follower in followers)
            {
                Assert.True(kweetCounts[follower] <= 30);
            }
        }


    }
}