using HomeTimeline.Application.Contracts;
using HomeTimeline.Domain.Entities;
using Kweet.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeTimeline.Infrastructure.Repositories
{
    public class HomeTimelineRepository : IHomeTimelineRepository
    {
        private readonly IDistributedCache _cache;


        public HomeTimelineRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<Timeline> GetTimeline(string userName)
        {
            // Get the cached timeline string from the cache using the user's name as the key
            var cachedTimelineString = await _cache.GetStringAsync(userName);

            // If the timeline string is not null, deserialize it into a Timeline object and return it
            if (cachedTimelineString != null)
            {
                return JsonSerializer.Deserialize<Timeline>(cachedTimelineString);
            }
            // If the timeline string is null, return an empty Timeline object with the user's name set
            else
            {
                return new Timeline { UserName = userName };
            }
        }

        public async Task UpdateTimeline(Timeline timeline)
        {
            // Add the kweet to the list of kweets for the follower
            var cachedTimelineString = await _cache.GetStringAsync(timeline.UserName);
            Timeline cachedTimeline = cachedTimelineString != null ?
                JsonSerializer.Deserialize<Timeline>(cachedTimelineString) :
                new Timeline { UserName = timeline.UserName };

            // Add the new tweet to the beginning of the Kweets list
            cachedTimeline.Kweets.Insert(0, timeline.Kweets[0]);

            // Trim the list to 30 items if it has more than 30 items
            if (cachedTimeline.Kweets.Count > 30)
            {
                cachedTimeline.Kweets.RemoveRange(30, cachedTimeline.Kweets.Count - 30);
            }

            // Serialize the updated timeline object into a string
            var serializedTimeline = JsonSerializer.Serialize(cachedTimeline);

            // Set the serialized timeline string in the cache with the user's name as the key
            await _cache.SetStringAsync(timeline.UserName, serializedTimeline);
        }
    }
}
