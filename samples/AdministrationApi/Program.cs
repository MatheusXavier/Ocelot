using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Ocelot.Administration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

using System.IO;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost
    .UseContentRoot(Directory.GetCurrentDirectory());

builder.Services
    .AddOcelot()
    .AddAdministration("/administration", "secret");

builder.Logging
    .ClearProviders()
    .AddConsole();

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json");

WebApplication app = builder.Build();

await app.UseOcelot();
await app.RunAsync();
