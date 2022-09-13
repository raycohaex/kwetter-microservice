using UserTimeline.Domain;

namespace UserTimeline.Application.Contracts.Persistence
{
    public interface IUserTimelineRepository
    {
        Task<Timeline> GetTimeline(string userName);
        Task<Timeline> UpdateTimeline(Timeline timeline);
    }
}