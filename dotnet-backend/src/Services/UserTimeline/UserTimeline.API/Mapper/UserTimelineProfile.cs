﻿using AutoMapper;
using Eventbus.Messages.Events;
using UserTimeline.API.EventBusConsumers;
using UserTimeline.Application.Features.Commands.UpdateTimeline;
using UserTimeline.Domain;

namespace UserTimeline.API.Mapper
{
    public class UserTimelineProfile: Profile
    {
        public UserTimelineProfile()
        {
            CreateMap<KweetEntity, KweetPostedEvent>().ReverseMap();
            CreateMap<UpdateTimelineCommand, KweetEntity>().ReverseMap();
        }
    }
}
