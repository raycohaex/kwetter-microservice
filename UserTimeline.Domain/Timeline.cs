namespace UserTimeline.Infrastructure
{
    public class Timeline
    {
        public string UserName { get; set; }
        // Create list that we store in Redis
        public List<KweetEntity> Items { get; set; } = new List<KweetEntity>();
        public Timeline()
        {

        }

        public Timeline(string userName)
        {
            UserName = userName;
        }
    }
}