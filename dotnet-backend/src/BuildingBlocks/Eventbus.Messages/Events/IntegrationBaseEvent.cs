using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventbus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        // Base information for my MQ event
        public IntegrationBaseEvent()
        {
            EventId = Guid.NewGuid();
            EventCreationDate = DateTime.UtcNow;
        }

        public IntegrationBaseEvent(Guid id, DateTime createDate)
        {
            EventId = id;
            EventCreationDate = createDate;
        }

        public Guid EventId { get; private set; }
        public DateTime EventCreationDate { get; private set; }
    }
}
