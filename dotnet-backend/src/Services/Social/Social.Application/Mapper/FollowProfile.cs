using AutoMapper;
using Social.Application.Dto;
using Social.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Mapper
{
    public class FollowProfile : Profile
    {
        public FollowProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Relation, RelationDto>().ReverseMap();
        }
    }
}
