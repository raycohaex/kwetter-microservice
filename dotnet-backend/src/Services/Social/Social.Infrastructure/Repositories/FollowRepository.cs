using Neo4jClient;
using Neo4jClient.Cypher;
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

        public Task FindUserNode(Guid userId)
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

        public async Task<IEnumerable<User>> GetFollowers(Guid user)
        {
            var followers = _client.Cypher
                .Match("(user:User {name: {userName}})<-[:FOLLOWS]-(follower:User)")
                .WithParam("userName", user)
                .Return(follower => follower.As<User>());

            return await followers.ResultsAsync;
        }

        public async Task<long> GetFollowingCount(string userName)
        {
            var query = _client.Cypher
                .Match(@"(user:User {UserName: $userName})-[relation:FOLLOWS]->()")
                .WithParam("userName", userName)
                .Return((relation) => relation.Count());

            var result = await query.ResultsAsync;
            return result.Single();
        }

        public async Task<long> GetFollowersCount(string userName)
        {
            var query = _client.Cypher
                .Match(@"(user:User {UserName: $userName})<-[relation:FOLLOWS]-()")
                .WithParam("userName", userName)
                .Return((relation) => relation.Count());

            var result = await query.ResultsAsync;
            return result.Single();
        }
    }
}
