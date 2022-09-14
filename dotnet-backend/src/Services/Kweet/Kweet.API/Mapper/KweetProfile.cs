using AutoMapper;
using Eventbus.Messages.Events;
using Kweet.Application.Features.Queries.GetKweet;
using Kweet.Domain.Entities;

namespace Kweet.API.Mapper
{
    public class KweetProfile: Profile
    {
        public KweetProfile()
        {
            CreateMap<KweetEntity, KweetPostedEvent>().ReverseMap();
            CreateMap<KweetViewModel, KweetPostedEvent>().ReverseMap();
        }
    }
}
