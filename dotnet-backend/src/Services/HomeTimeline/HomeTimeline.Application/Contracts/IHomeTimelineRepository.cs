using HomeTimeline.Domain.Entities;
using Kweet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTimeline.Application.Contracts
{
    public interface IHomeTimelineRepository
    {
        Task<Timeline> GetTimeline(string userName);
        Task UpdateTimeline(Timeline timelineKweet);
    }
}
