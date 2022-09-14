using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTimeline.Domain;

namespace UserTimeline.Application.Features.Queries.GetTimeline
{
    public class TimelineViewModel
    {
        public string UserName { get; set; }
        // Create list that we store in Redis
        public List<KweetEntity> Items { get; set; } = new List<KweetEntity>();
        public TimelineViewModel()
        {

        }

        public TimelineViewModel(string userName)
        {
            UserName = userName;
        }
    }
}
