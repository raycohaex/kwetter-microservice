using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventbus.Messages.Events
{
    public class KweetPostedEvent: IntegrationBaseEvent
    {
        public KweetPostedEvent() : base(nameof(KweetPostedEvent))
        {

        }

        public Guid Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        // Kweet content
        public string UserName { get; set; }
        public string TweetBody { get; set; }
    }
}
