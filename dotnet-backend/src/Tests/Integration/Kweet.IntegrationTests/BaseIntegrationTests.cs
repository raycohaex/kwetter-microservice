using Kweet.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;

namespace Kweet.IntegrationTests
{
    public class BaseIntegrationTests
    {
        private readonly HttpClient _client;

        public BaseIntegrationTests()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(host =>
                {
                    host.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                            typeof(DbContextOptions<KweetContext>));

                        services.Remove(descriptor);

                        services.AddDbContext<KweetContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDB");
                        });
                    });
                });

            _client = application.CreateClient();
        }


        [Fact]
        public async Task HelloWorldTest()
        {

            var response = await _client.PostAsync("/", null);

            //// Assert
            //response.EnsureSuccessStatusCode(); // Status Code 200-299
            //Assert.Equal("text/html; charset=utf-8",
            //    response.Content.Headers.ContentType.ToString());
        }
    }
}