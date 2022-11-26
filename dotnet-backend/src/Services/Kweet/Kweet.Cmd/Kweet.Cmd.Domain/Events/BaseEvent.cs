using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Cmd.Domain.Events
{
    public abstract class BaseEvent
    {
        protected BaseEvent(string type)
        {
            Type = type;
        }

        public Guid Id { get; set; }
        public string Type { get; set; }
        public int Version { get; set; }
    }
}
