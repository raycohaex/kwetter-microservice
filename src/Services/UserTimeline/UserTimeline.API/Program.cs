using Microsoft.OpenApi.Models;
using UserTimeline.Application;
using UserTimeline.Application.Contracts.Persistence;
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

builder.Services.AddApplicationServices();

builder.Services.AddSwaggerGen(options => {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "User timeline API", Version = "v1",
        Description = "Receives new Kweets by users through a message queue and updates the timeline in the background"
        });
    }
);


// TODO: Consider moving to smaller register function like in the Kweet API.
builder.Services.AddScoped<IUserTimelineRepository, UserTimelineRepository>();
builder.Services.AddAutoMapper(typeof(Program));

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
