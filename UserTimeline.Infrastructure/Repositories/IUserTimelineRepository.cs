namespace UserTimeline.Infrastructure.Repositories
{
    public interface IUserTimelineRepository
    {
        Task<Timeline> GetTimeline(string userName);
        Task<Timeline> UpdateTimeline(Timeline timeline);
    }
}