using AutoMapper;
using Kweet.Application.Features.Commands.PostKweet;
using Kweet.Application.Features.Queries.GetKweet;
using Kweet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Application.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<KweetEntity, KweetViewModel>().ReverseMap();
            CreateMap<KweetEntity, PostKweetCommand>().ReverseMap();
        }
    }
}
