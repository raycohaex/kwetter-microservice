using Eventbus.Messages.Common;
using Eventbus.Messages.Events;
using MassTransit;
using Microsoft.OpenApi.Models;
using UserTimeline.API.EventBusConsumers;
using UserTimeline.Application;
using UserTimeline.Application.Contracts.Persistence;
using UserTimeline.Infrastructure.Persistence;
using UserTimeline.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

builder.Services.AddApplicationServices();

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<KweetPostedConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstants.KweetPostedQueue, c =>
        {
            c.ConfigureConsumer<KweetPostedConsumer>(ctx);
        });
    });
});

builder.Services.AddSwaggerGen(options => {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "User timeline API", Version = "v1",
        Description = "Receives new Kweets by users through a message queue and updates the timeline in the background"
        });
    }
);


// TODO: Consider moving to smaller register function like in the Kweet API.
builder.Services.AddScoped<IUserTimelineRepository, UserTimelineRepository>();
builder.Services.AddScoped<IUserTimelineContext, UserTimelineContext>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
