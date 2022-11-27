using Eventbus.Messages.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventbus.Messages.Events
{
    public abstract class BaseEvent: Message
    {
        protected BaseEvent(string type)
        {
            Type = type;
        }

        public string Type { get; set; }
        public int Version { get; set; }
    }
}
