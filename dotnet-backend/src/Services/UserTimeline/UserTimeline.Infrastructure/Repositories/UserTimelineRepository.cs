using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTimeline.Application.Contracts.Persistence;
using UserTimeline.Domain;
using UserTimeline.Infrastructure.Persistence;

namespace UserTimeline.Infrastructure.Repositories
{
    public class UserTimelineRepository : IUserTimelineRepository
    {
        private readonly IDistributedCache _cache;
        private readonly IUserTimelineContext _context;


        public UserTimelineRepository(IDistributedCache cache, IUserTimelineContext context)
        {
            _cache = cache;
            _context = context;
        }

        public async Task<Timeline> GetTimeline(string userName)
        {
            var timeline = await _cache.GetStringAsync(userName);

            // No cached timeline, get from db and put in cache.
            if (String.IsNullOrEmpty(timeline))
            {
                FilterDefinition<Timeline> filter = Builders<Timeline>.Filter.Eq(t => t.UserName, userName);
                var tl = await _context.Timeline
                                            .Find(filter)
                                            .FirstOrDefaultAsync();
                if (tl != null)
                {
                    await _cache.SetStringAsync(tl.UserName, JsonConvert.SerializeObject(tl));
                }

                return tl;
            }

            return JsonConvert.DeserializeObject<Timeline>(timeline);
        }

        public async Task<Timeline> UpdateTimeline(Timeline timeline)
        {
            FilterDefinition<Timeline> filter = Builders<Timeline>.Filter.Eq(t => t.UserName, timeline.UserName);
            var existingTimeline = await _context.Timeline
                                        .Find(filter)
                                        .FirstOrDefaultAsync();

            // If timeline exists, update it, else create.
            if (existingTimeline == null)
            {
                await _context.Timeline.InsertOneAsync(timeline);
            } else
            {
                var updateResult = await _context.Timeline
                                        .ReplaceOneAsync(filter: t => t.Id == existingTimeline.Id, replacement: timeline);
            }

            // Update cached timeline too.
            var cachedTimeline = await _cache.GetStringAsync(timeline.UserName);
            if(cachedTimeline != null)
            {
                await _cache.SetStringAsync(timeline.UserName, JsonConvert.SerializeObject(timeline));
            }

            return await GetTimeline(timeline.UserName);
        }
    }
}
