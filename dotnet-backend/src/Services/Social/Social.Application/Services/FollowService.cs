using Social.Application.Contracts;
using Social.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Services
{
    public class FollowService : IFollowService
    {
        public Task CreateFollowRelation(RelationDto relation)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> CreateUserNode(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
