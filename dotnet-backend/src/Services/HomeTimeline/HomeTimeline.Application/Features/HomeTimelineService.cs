using HomeTimeline.Application.Contracts;
using HomeTimeline.Domain.Entities;
using Kweet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTimeline.Application.Features
{
    public class HomeTimelineService: IHomeTimelineService
    {
        public HomeTimelineService()
        {

        }

        public Task<Domain.Entities.HomeTimeline> GetHomeTimeline(string username)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTimelines(List<string> followers, KweetEntity kweet)
        {
            throw new NotImplementedException();
        }
    }
}
