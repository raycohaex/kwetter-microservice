using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTimeline.Domain;

namespace UserTimeline.Infrastructure.Persistence
{
    public interface IUserTimelineContext
    {
        IMongoCollection<Timeline> Timeline { get; }
    }
}
