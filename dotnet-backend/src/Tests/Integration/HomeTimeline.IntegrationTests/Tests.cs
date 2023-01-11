using Eventbus.Messages.Events;
using Eventbus.Messages.Events.Integration.GetFollowers;
using HomeTimeline.API.EventBusConsumer;
using HomeTimeline.Application.Contracts;
using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using NUnit.Framework.Internal;

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


    }
}