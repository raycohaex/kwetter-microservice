using AutoMapper;
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
            // CreateMap<KweetEntity, KweetVm>().ReverseMap();
        }
    }
}
