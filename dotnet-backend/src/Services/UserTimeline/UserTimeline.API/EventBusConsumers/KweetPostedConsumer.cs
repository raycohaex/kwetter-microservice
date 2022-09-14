using MassTransit;
using AutoMapper;
using Eventbus.Messages.Events;
using MediatR;
using UserTimeline.Domain;
using UserTimeline.Application.Features.Queries.GetTimeline;
using UserTimeline.Application.Features.Commands.UpdateTimeline;

namespace UserTimeline.API.EventBusConsumers
{
    public class KweetPostedConsumer : IConsumer<KweetPostedConsumer>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public KweetPostedConsumer(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<KweetPostedConsumer> context)
        {
            var entity = _mapper.Map<KweetEntity>(context.Message);

            var query = new GetTimelineQuery(entity.UserName);
            var timeline = await _mediator.Send(query);

            timeline.Items.Add(entity);

            var command = _mapper.Map<UpdateTimelineCommand>(entity);
            await _mediator.Send(command);
            
        }
    }
}
