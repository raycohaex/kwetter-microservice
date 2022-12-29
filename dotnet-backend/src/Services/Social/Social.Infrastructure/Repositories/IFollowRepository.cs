using Social.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure.Repositories
{
    public interface IFollowRepository
    {
        Task CreateUserNode(User user);
        Task FindUserNode(Guid userId);
        Task DeleteUserNode(User user);
        Task FollowUser(Relation relation);
        Task<IEnumerable<User>> GetFollowers(string username);
        Task<long> GetFollowersCount(string userName);
        Task<long> GetFollowingCount(string userName);
    }
}
