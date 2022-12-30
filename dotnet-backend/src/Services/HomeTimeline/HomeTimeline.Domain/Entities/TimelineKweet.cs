using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTimeline.Domain.Entities
{
    public class TimelineKweet
    {
        public string TweetBody { get; set; }
        public string UserName { get; set; }
        public string CreatedAt { get; set; }
    }
}
