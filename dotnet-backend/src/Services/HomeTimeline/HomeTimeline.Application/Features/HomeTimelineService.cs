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
        private readonly IHomeTimelineRepository _repository;
        public HomeTimelineService(IHomeTimelineRepository repository)
        {
            _repository = repository;
        }

        public async Task<Timeline> GetHomeTimeline(string username)
        {
            var timeline = await _repository.GetTimeline(username);
            return timeline;
        }

        public async Task UpdateTimelines(List<string> followers, KweetEntity kweet)
        {
            var timelineKweet = new TimelineKweet();
            timelineKweet.UserName = kweet.UserName;
            timelineKweet.TweetBody = kweet.TweetBody;
            timelineKweet.CreatedAt = kweet.CreatedOn;

            foreach (var follower in followers)
            {
                var timeline = new Timeline();
                timeline.UserName = follower;
                timeline.Kweets.Add(timelineKweet);

                await _repository.UpdateTimeline(timeline);
            }
        }
    }
}
