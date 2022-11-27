using Kweet.Common.Entities;

namespace Kweet.Query.Api.Persistence
{
    public interface IKweetRepository
    {
        Task SaveKweet(KweetEntity kweet);
    }
}
