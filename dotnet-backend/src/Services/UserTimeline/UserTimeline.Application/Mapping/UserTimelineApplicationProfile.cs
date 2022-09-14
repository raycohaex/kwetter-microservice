using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTimeline.Application.Features.Commands.UpdateTimeline;
using UserTimeline.Application.Features.Queries.GetTimeline;
using UserTimeline.Domain;

namespace UserTimeline.Application.Mapping
{
    public class UserTimelineApplicationProfile : Profile
    {
        public UserTimelineApplicationProfile()
        {
            CreateMap<UpdateTimelineCommand, Timeline>().ReverseMap();
            CreateMap<TimelineViewModel, Timeline>().ReverseMap();

        }
    }
}
