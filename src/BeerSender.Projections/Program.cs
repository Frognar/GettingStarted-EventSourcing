﻿using BeerSender.Projections.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

HostBuilder builder = new();

builder.ConfigureHostConfiguration(config =>
{
    config.AddUserSecrets<Program>();
    config.AddJsonFile("appsettings.json", true);
    config.AddEnvironmentVariables();
});

builder.ConfigureServices((_, services) =>
{
    services.RegisterDataConnections();
});

IHost app = builder.Build();

app.Run();