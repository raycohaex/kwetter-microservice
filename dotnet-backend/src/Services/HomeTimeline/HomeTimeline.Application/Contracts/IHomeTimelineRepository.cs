using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTimeline.Application.Contracts
{
    public class IHomeTimelineRepository
    {
        Task<Timeline> GetTimeline(string userName);
        Task<Timeline> UpdateTimeline(Timeline timeline);
    }
}
