using Eventbus.Messages.Events;
using Kweet.Cmd.Domain.Events;
using Kweet.Common.Entities;
using Kweet.Query.Api.Persistence;
using MassTransit;

namespace Kweet.Query.Api.Consumer
{
    public class KweetPostedConsumer : IConsumer<KweetCreatedEvent>
    {
        private readonly ILogger<KweetPostedConsumer> _logger;
        private readonly IKweetRepository _kweetRepository;

        public KweetPostedConsumer(ILogger<KweetPostedConsumer> logger, IKweetRepository kweetRepository)
        {
            _logger = logger;
            _kweetRepository = kweetRepository;
        }

        public async Task Consume(ConsumeContext<KweetCreatedEvent> context)
        {
            _logger.Log(LogLevel.Error, context.Message.UserName);
            var kweet = new KweetEntity();
            kweet.Id = context.Message.Id;
            kweet.UserName = context.Message.UserName;
            kweet.TweetBody = context.Message.TweetBody;
            kweet.CreatedBy = "TO_BE_IMPLEMENTED";

            await _kweetRepository.SaveKweet(kweet);


        }
    }
}
