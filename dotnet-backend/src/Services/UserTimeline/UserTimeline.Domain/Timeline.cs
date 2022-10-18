using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserTimeline.Domain
{
    public class Timeline
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        // Create list that we store in Redis
        public DateTime ?UpdatedAt { get; set; }
        public List<KweetEntity> ?Items { get; set; }
    }
}