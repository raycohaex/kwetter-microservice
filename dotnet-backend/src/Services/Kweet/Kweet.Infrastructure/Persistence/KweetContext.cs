using Kweet.Domain.Common;
using Kweet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Infrastructure.Persistence
{
    public class KweetContext : DbContext
    {
        public KweetContext(DbContextOptions<KweetContext> options) : base(options)
        {
            // ...
        }

        public DbSet<KweetEntity> Kweets { get; set; }
    }
}
