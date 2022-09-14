using AutoMapper;
using UserTimeline.API.EventBusConsumers;
using UserTimeline.Application.Features.Commands.UpdateTimeline;
using UserTimeline.Domain;

namespace UserTimeline.API.Mapper
{
    public class UserTimelineProfile: Profile
    {
        public UserTimelineProfile()
        {
            CreateMap<KweetEntity, KweetPostedConsumer>().ReverseMap();
            CreateMap<UpdateTimelineCommand, KweetEntity>().ReverseMap();
        }
    }
}
