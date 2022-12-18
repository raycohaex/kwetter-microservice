using AutoMapper;
using Eventbus.Messages.Events;
using Kweet.Domain.Entities;
using MassTransit;
using MassTransit.Mediator;

namespace HomeTimeline.API.EventBusConsumer
{
    public class KweetPostedConsumer : IConsumer<KweetPostedEvent>
    {
        private readonly ILogger<KweetPostedConsumer> _logger;

        public KweetPostedConsumer(ILogger<KweetPostedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<KweetPostedEvent> context)
        {
            // use different kweet entity
            _logger.Log(LogLevel.Information, $"kweet received {context.Message}");
        }
    }
}
