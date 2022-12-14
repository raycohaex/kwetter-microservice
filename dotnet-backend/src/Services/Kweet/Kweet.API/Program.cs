using Keycloak.AuthServices.Authentication;
using Kweet.Application;
using Kweet.Infrastructure;
using Kweet.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var host = builder.Host;

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add keycloak
builder.Services.AddKeycloakAuthentication(builder.Configuration);

// Only configure a host since this service will only emit.
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});

// Add services from application & infrastructure layer.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<KweetContext>();
    if (!db.Database.IsInMemory())
    {
        db.Database.Migrate();
    }
    
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();

public partial class Program { }