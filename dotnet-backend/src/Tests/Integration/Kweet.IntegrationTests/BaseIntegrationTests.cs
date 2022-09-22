using Kweet.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
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
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(KweetContext));
                        services.AddDbContext<KweetContext>(options => { options.UseInMemoryDatabase("TestDb"); });
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