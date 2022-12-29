using Social.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Contracts
{
    public interface IFollowService
    {
        Task<UserDto> CreateUserNode(UserDto user);
        Task CreateFollowRelation(RelationDto relation);
        Task<IEnumerable<UserDto>> GetFollowers(string username);
        Task<Dictionary<string, long>> GetUserSocialStats(string userId);
    }
}
