using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTimeline.Application.Contracts.Persistence;
using UserTimeline.Application.Features.Queries.GetTimeline;
using UserTimeline.Domain;

namespace UserTimeline.Application.Features.Commands.UpdateTimeline
{
    public class UpdateTimelineCommandHandler : IRequestHandler<UpdateTimelineCommand>
    {
        private readonly IUserTimelineRepository _kweetRepository;
        private readonly IMapper _mapper;

        public UpdateTimelineCommandHandler(IUserTimelineRepository kweetRepository, IMapper mapper)
        {
            _kweetRepository = kweetRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTimelineCommand request, CancellationToken cancellationToken)
        {
            var timelineToUpdate = await _kweetRepository.GetTimeline(request.UserName);

            if (timelineToUpdate == null)
            {
                timelineToUpdate = new Timeline(request.UserName);
            }

            _mapper.Map(request, timelineToUpdate, typeof(UpdateTimelineCommand), typeof(Timeline));

            await _kweetRepository.UpdateTimeline(timelineToUpdate);

            return Unit.Value;
        }

    }
}
