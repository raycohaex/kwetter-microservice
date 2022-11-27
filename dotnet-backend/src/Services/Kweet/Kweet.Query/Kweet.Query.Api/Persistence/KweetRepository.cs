using Kweet.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kweet.Query.Api.Persistence
{
    public class KweetRepository: IKweetRepository
    {
        private readonly KweetContext _dbContext;
        public KweetRepository(KweetContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveKweet(KweetEntity kweet)
        {
            _dbContext.Kweets.Add(kweet);
            await _dbContext.SaveChangesAsync();
        }
    }
}
