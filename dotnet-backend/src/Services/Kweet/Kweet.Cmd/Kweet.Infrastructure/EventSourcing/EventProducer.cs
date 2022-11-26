using CQRS.Core.EventSourcing;
using Eventbus.Messages.Events;
using Kweet.Cmd.Domain.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Cmd.Infrastructure.EventSourcing
{
    public class EventProducer: IEventProducer
    {
        private readonly IPublishEndpoint _publish;

        public EventProducer(IPublishEndpoint publish)
        {
            _publish = publish;
        }

        public async Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent
        {
            // publish event
        }
    }
}
