using MassTransit;
using AutoMapper;
using Eventbus.Messages.Events;
using MediatR;
using UserTimeline.Domain;
using UserTimeline.Application.Features.Queries.GetTimeline;
using UserTimeline.Application.Features.Commands.UpdateTimeline;

namespace UserTimeline.API.EventBusConsumers
{
    public class KweetPostedConsumer : IConsumer<KweetPostedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<KweetPostedConsumer> _logger;

        public KweetPostedConsumer(IMapper mapper, IMediator mediator, ILogger<KweetPostedConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<KweetPostedEvent> context)
        {
            var entity = _mapper.Map<KweetEntity>(context.Message);

            var query = new GetTimelineQuery(entity.UserName);
            var timeline = await _mediator.Send(query);

            // TODO map
            var test = new Timeline(entity.UserName);
            test.Items.Add(entity);

            var command = _mapper.Map<UpdateTimelineCommand>(test);
            await _mediator.Send(command);
            
        }
    }
}
