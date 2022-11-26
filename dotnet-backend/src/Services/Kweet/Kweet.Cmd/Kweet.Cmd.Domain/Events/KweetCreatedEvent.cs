using Eventbus.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Cmd.Domain.Events
{
    public class KweetCreatedEvent : BaseEvent
    {
        public KweetCreatedEvent() : base(nameof(KweetCreatedEvent))
        {
        }

        public string UserName { get; set; }
        public string TweetBody { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
