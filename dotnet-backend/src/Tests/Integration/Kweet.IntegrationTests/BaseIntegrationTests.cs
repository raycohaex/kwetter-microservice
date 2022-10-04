using Kweet.API.Controllers;
using Kweet.Application.Features.Queries.GetKweet;
using Kweet.Infrastructure.Persistence;
using MassTransit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
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

                        services.AddMassTransitTestHarness();

                        services.AddDbContext<KweetContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDB");
                        });
                    });
                });

            _client = application.CreateClient();
        }



        [Fact]
        public async Task GetKweet_InvalidId_ReturnsEmptyResponse()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/Tweet/70b443c4-50f9-45b4-b6c8-73623061df57");
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Empty(responseString);
        }

        [Fact]
        public async Task Create_MissingUsername_ReturnsViewWithErrorMessages()
        {
            var formModel = new Dictionary<string, string>
                {
                    { "tweetBody", "Bla" }
                };

            string json = JsonConvert.SerializeObject(formModel);
            StringContent body = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var postRequest = await _client.PostAsync("/api/v1/Tweet/", body);

            var responseString = await postRequest.Content.ReadAsStringAsync();

            Assert.Contains("One or more validation errors occurred.", responseString);
        }

        [Fact]
        public async Task Create_MissingTweetBody_ReturnsViewWithErrorMessages()
        {
            var formModel = new Dictionary<string, string>
                {
                    { "UserName", "Bla" }
                };

            string json = JsonConvert.SerializeObject(formModel);
            StringContent body = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var postRequest = await _client.PostAsync("/api/v1/Tweet/", body);

            var responseString = await postRequest.Content.ReadAsStringAsync();

            Assert.Contains("One or more validation errors occurred.", responseString);
        }

        [Fact]
        public async Task Create_CorrectlyFilledBody_ReturnsSuccess()
        {
            var formModel = new Dictionary<string, string>
                {
                    { "UserName", "Bla" },
                    { "tweetBody", "Bla" }
                };

            string json = JsonConvert.SerializeObject(formModel);
            StringContent body = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var postRequest = await _client.PostAsync("/api/v1/Tweet/", body);

            postRequest.EnsureSuccessStatusCode();

            var responseString = await postRequest.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<KweetViewModel>(responseString);

            Assert.Contains("Bla", response.UserName);
            Assert.Contains("Bla", response.TweetBody);
            Assert.IsType<Guid>(response.Id);
        }
    }
}