using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;

using System.IO;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost
    .UseContentRoot(Directory.GetCurrentDirectory());

builder.Services.AddOcelot();

if (builder.Environment.IsDevelopment())
{
    builder.Logging
        .ClearProviders()
        .AddConsole();
}

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json");

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

await app.UseOcelot();
await app.RunAsync();
