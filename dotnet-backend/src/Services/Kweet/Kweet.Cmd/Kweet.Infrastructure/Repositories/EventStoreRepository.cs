using CQRS.Core.Domain;
using Eventbus.Messages.Events;
using Kweet.Cmd.Domain.Events;
using Kweet.Cmd.Infrastructure.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Cmd.Infrastructure.Repositories
{
    public class EventStoreRepository: IEventStoreRepository
    {
        private readonly IMongoCollection<EventModel> _eventStoreCollection;

        public EventStoreRepository(IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var mongoDatabase = mongoClient.GetDatabase(configuration.GetValue<string>("DatabaseSettings:Database"));

            _eventStoreCollection = mongoDatabase.GetCollection<EventModel>(configuration.GetValue<string>("DatabaseSettings:Collection"));
        }

        public async Task<List<EventModel>> FindAllAsync()
        {
            return await _eventStoreCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<EventModel>> FindByAggregateId(Guid aggregateId)
        {
            return await _eventStoreCollection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync().ConfigureAwait(false);
        }

        public async Task SaveAsync(EventModel @event)
        {
            await _eventStoreCollection.InsertOneAsync(@event).ConfigureAwait(false);
        }
    }
}
