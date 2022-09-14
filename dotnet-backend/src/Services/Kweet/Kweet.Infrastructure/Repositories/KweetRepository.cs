using Kweet.Application.Contracts.Persistence;
using Kweet.Domain.Entities;
using Kweet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Infrastructure.Repositories
{
    public class KweetRepository : RepositoryBase<KweetEntity>, IKweetRepository
    {
        public KweetRepository(KweetContext dbContext) : base(dbContext)
        {
        }

        public async Task<KweetEntity> GetKweetById(Guid id)
        {
            return await _dbContext.Kweets
                                .Where(k => k.Id == id)
                                .FirstOrDefaultAsync();
        }
    }
}
