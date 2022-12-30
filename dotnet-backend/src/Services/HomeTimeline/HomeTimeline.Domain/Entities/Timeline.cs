using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTimeline.Domain.Entities
{
    public class Timeline
    {
        public string UserName { get; set; }
        public List<TimelineKweet> Kweets { get; set; } = new List<TimelineKweet>();
    }
}
