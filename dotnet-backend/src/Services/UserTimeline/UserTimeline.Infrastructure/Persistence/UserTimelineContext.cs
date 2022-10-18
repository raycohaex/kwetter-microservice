using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using UserTimeline.Domain;

namespace UserTimeline.Infrastructure.Persistence
{
    public class UserTimelineContext: IUserTimelineContext
    {
        public UserTimelineContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Timeline = database.GetCollection<Timeline>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }

        public IMongoCollection<Timeline> Timeline { get; }
    }
}
