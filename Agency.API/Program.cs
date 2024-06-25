using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MassTransit;
using Agency.API.Database;
using Agency.API.Extentions;
using Agency.API.Domain.Models;
using Agency.API.Domain.Repository;
using Agency.API.Domain.Services;
using Agency.API.Repositories;
using Agency.API.Services;
using Agency.API.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(conf =>
    conf.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssembly(assembly));

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddScoped<IAgencyRepository, AgencyRepository>();
builder.Services.AddScoped<IAgencyService, AgencyService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();

    config.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]);
            h.Password(builder.Configuration["MessageBroker:Password"]);
        });

        configurator.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.AddMigrations();
}

app.UseHttpsRedirection();

app.AddAgencyEndpoints();

app.Run();
