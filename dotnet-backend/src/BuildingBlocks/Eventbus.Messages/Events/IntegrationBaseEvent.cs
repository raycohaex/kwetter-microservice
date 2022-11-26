using Eventbus.Messages.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventbus.Messages.Events
{
    public abstract class IntegrationBaseEvent: Message
    {
        // Base information for my MQ event
        protected IntegrationBaseEvent(string type)
        {
            this.Type = type;
        }

        public int Version { get; set; }
        public string Type { get; set; }
    }
}
