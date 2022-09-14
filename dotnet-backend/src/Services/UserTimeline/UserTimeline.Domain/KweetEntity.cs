namespace UserTimeline.Domain
{
    public class KweetEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string TweetBody { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}