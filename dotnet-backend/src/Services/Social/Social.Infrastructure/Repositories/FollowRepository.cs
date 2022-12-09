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

        public async Task FollowUser(Relation relation)
        {
            await _client.Cypher
             .Match("(fr:User {UserName: $fromId}), (to:User {UserName: $toId})")
             .Create("(fr)-[:FOLLOWS]->(to)")
             .WithParams(new
             {
                 fromId = relation.Follower,
                 toId = relation.Followee
             })
             .ExecuteWithoutResultsAsync();
        }

        public Task GetFollowers(User user)
        {
            throw new NotImplementedException();
        }
    }
}
