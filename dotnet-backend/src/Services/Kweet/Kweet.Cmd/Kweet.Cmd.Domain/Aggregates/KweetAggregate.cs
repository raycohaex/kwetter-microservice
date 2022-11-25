using Eventbus.Messages.Events;
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
        private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();

        public bool Active { get => _active; set => _active = value; }

        public KweetAggregate()
        {
        }

        public KweetAggregate(Guid id, string username, string message)
        {
            RaiseEvent(new KweetPostedEvent
            {
                Id = id,
                UserName = username,
                TweetBody = message
            });
        }

        public void Apply(KweetPostedEvent @event)
        {
            _id = @event.Id;
            _active = true;
            _user = @event.UserName;
        }
    }
}
