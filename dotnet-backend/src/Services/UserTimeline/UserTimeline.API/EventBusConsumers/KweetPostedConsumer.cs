using MassTransit;
using AutoMapper;
using Eventbus.Messages.Events;
using MediatR;
using UserTimeline.Domain;
using UserTimeline.Application.Features.Queries.GetTimeline;
using UserTimeline.Application.Features.Commands.UpdateTimeline;

namespace UserTimeline.API.EventBusConsumers
{
    public class KweetPostedConsumer : IConsumer<EventModel>
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

        public async Task Consume(ConsumeContext<EventModel> context)
        {
            var entity = _mapper.Map<KweetEntity>(context.Message);

            var query = new GetTimelineQuery(entity.UserName);
            var timeline = await _mediator.Send(query);

            if (timeline == null)
            {
                timeline = new TimelineViewModel(entity.UserName);
            }

            timeline.Items.Insert(0, entity);

            var command = _mapper.Map<UpdateTimelineCommand>(timeline);
            await _mediator.Send(command);
            
        }
    }
}
