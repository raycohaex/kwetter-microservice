using Kweet.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kweet.Query.Api.Persistence
{
    public class KweetContext : DbContext
    {
        public KweetContext(DbContextOptions<KweetContext> options) : base(options)
        {
            // ...
        }

        public virtual DbSet<KweetEntity> Kweets { get; set; }
    }
}
