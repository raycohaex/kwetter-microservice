using Microsoft.OpenApi.Models;
using Neo4jClient;
using Social.Application.Contracts;
using Social.Application.Mapper;
using Social.Application.Services;
using Social.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var server = builder.Configuration.GetValue<string>("neo4j:server");
var username = builder.Configuration.GetValue<string>("neo4j:username");
var password = builder.Configuration.GetValue<string>("neo4j:password");

var client = new BoltGraphClient(
    new Uri(server),
    username,
    password
);


client.ConnectAsync();

builder.Services.AddSingleton<IGraphClient>(client);

builder.Services.AddAutoMapper(typeof(FollowProfile));

builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddScoped<IFollowRepository, FollowRepository>();

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