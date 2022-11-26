using CQRS.Core.Domain;
using Eventbus.Messages.Events;
using Kweet.Cmd.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Cmd.Domain.Aggregates
{
    public class KweetAggregate : AggregateRoot
    {
        private bool _active;
        private string _user;

        public bool Active { get => _active; set => _active = value; }

        public KweetAggregate()
        {
        }

        public KweetAggregate(Guid id, string username, string message)
        {
            RaiseEvent(new KweetCreatedEvent
            {
                Id = id,
                UserName = username,
                TweetBody = message,
                DatePosted = DateTime.Now
            });
        }

        public void Apply(KweetCreatedEvent @event)
        {
            _id = @event.Id;
            _active = true;
            _user = @event.UserName;
        }
    }
}
