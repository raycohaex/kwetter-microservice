using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kweet.Domain.Entities;

namespace Kweet.Application.Contracts.Persistence
{
    public interface IKweetRepository : IAsyncRepository<KweetEntity>
    {
        Task<KweetEntity> GetKweetById(Guid id);
    }
}
