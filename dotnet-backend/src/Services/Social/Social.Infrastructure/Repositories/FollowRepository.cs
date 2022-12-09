using Neo4jClient;
using Social.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly IGraphClient _client;
        public FollowRepository(IGraphClient client)
        {
            _client = client;
        }
        public async Task CreateUserNode(User user)
        {
            await _client.Cypher.Create("(u:User $user)")
                                    .WithParam("user", user)
                                    .ExecuteWithoutResultsAsync();
        }

        public Task DeleteUserNode(User user)
        {
            throw new NotImplementedException();
        }

        public Task FollowUser(Relation relation)
        {
            throw new NotImplementedException();
        }

        public Task GetFollowers(User user)
        {
            throw new NotImplementedException();
        }
    }
}
