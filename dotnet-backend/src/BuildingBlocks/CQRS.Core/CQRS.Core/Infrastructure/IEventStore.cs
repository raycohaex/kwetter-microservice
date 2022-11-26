using Eventbus.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Infrastructure
{
    public interface IEventStore
    {
        Task SaveEventsAsync(Guid aggregateId, IEnumerable<IntegrationBaseEvent> events, int expectedVersion);
        Task<List<IntegrationBaseEvent>> GetEventsAsync(Guid aggregateId);
        Task<List<Guid>> GetAggregateIdsAsync();
    }
}
