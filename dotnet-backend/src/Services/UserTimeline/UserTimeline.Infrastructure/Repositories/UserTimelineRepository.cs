using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTimeline.Application.Contracts.Persistence;
using UserTimeline.Domain;

namespace UserTimeline.Infrastructure.Repositories
{
    public class UserTimelineRepository : IUserTimelineRepository
    {
        private readonly IDistributedCache _cache;

        public UserTimelineRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<Timeline> GetTimeline(string userName)
        {
            var basket = await _cache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(basket))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Timeline>(basket);
        }

        public async Task<Timeline> UpdateTimeline(Timeline timeline)
        {
            await _cache.SetStringAsync(timeline.UserName, JsonConvert.SerializeObject(timeline));

            return await GetTimeline(timeline.UserName);
        }
    }
}
