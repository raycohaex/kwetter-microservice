using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventbus.Messages.Events.Integration.AuthRegistered
{
    public class EventClientNotificationMqMsg
    {
        public long Time { get; set; }
        public string UserId { get; set; }
        public EventClientNotificationDetails Details { get; set; }
    }
}
