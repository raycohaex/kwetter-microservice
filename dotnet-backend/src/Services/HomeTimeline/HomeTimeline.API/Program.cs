using Eventbus.Messages.Common;
using Eventbus.Messages.Events.Integration.GetFollowers;
using HomeTimeline.API.EventBusConsumer;
using HomeTimeline.Application.Contracts;
using HomeTimeline.Application.Features;
using HomeTimeline.Infrastructure.Repositories;
using MassTransit;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<KweetPostedConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstants.KweetPostedHomeQueue, c =>
        {
            c.ConfigureConsumer<KweetPostedConsumer>(ctx);
        });
    });

    config.AddRequestClient<GetFollowersRequest>();
});

builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Home timeline API",
        Version = "v1",
        Description = "Receives new Kweets by users through a message queue and updates the timeline in the background"
    });
}
);

builder.Services.AddScoped<IHomeTimelineService, HomeTimelineService>();
builder.Services.AddScoped<IHomeTimelineRepository, HomeTimelineRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
