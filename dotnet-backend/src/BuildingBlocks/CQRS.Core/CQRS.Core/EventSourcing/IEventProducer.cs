using Eventbus.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.EventSourcing
{
    public interface IEventProducer
    {
        Task ProduceAsync<T>(string topic, T @event);
    }
}
