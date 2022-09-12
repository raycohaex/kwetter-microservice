using Kweet.Application.Contracts.Persistence;
using Kweet.Infrastructure.Persistence;
using Kweet.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kweet.Infrastructure
{
    public static class RegisterInfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KweetContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("KweetDbConnectionString");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IKweetRepository, KweetRepository>();

            return services;
        }
    }
}
