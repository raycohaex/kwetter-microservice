using CQRS.Core.Domain;
using CQRS.Core.EventSourcing;
using Kweet.Cmd.Application;
using Kweet.Cmd.Domain.Aggregates;
using Kweet.Cmd.Infrastructure.EventSourcing;
using Kweet.Cmd.Infrastructure.Repositories;
using MassTransit;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();

builder.Services.AddMassTransit(config =>
{
    //config.AddConsumer<KweetPostedConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        //cfg.ReceiveEndpoint(EventBusConstants.KweetPostedQueue, c =>
        //{
        //    c.ConfigureConsumer<KweetPostedConsumer>(ctx);
        //});
    });
});


builder.Services.AddScoped<IEventSourcingHandler<KweetAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
//builder.Services.AddMediatR(typeof(Program));
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
