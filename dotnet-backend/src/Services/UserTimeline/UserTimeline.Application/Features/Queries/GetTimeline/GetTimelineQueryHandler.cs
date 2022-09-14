using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTimeline.Application.Contracts.Persistence;
using UserTimeline.Domain;

namespace UserTimeline.Application.Features.Queries.GetTimeline
{
    public class GetTimelineQueryHandler : IRequestHandler<GetTimelineQuery, TimelineViewModel>
    {
        private readonly IUserTimelineRepository _kweetRepository;
        private readonly IMapper _mapper;

        public GetTimelineQueryHandler(IUserTimelineRepository kweetRepository, IMapper mapper)
        {
            _kweetRepository = kweetRepository;
            _mapper = mapper;
        }

        public async Task<TimelineViewModel> Handle(GetTimelineQuery request, CancellationToken cancellationToken)
        {
            var timeline = await _kweetRepository.GetTimeline(request.Username);
            return _mapper.Map<TimelineViewModel>(timeline);
        }
    }
}
