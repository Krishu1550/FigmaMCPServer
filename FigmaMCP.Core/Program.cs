using FigmaMcp.Application.DependencyInjection;
using FigmaMcp.Infrastructure.DependencyInjection;
using FigmaMCP.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

var basePath = AppContext.BaseDirectory;

builder.Configuration
    .SetBasePath(basePath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{builder.Environment.EnvironmentName}.json",
        optional: true,
        reloadOnChange: true)
    .AddEnvironmentVariables();

var figmaToken = builder.Configuration["FIGMA__TOKEN"];
if (string.IsNullOrEmpty(figmaToken))
{
    Console.Error.WriteLine("WARNING: FIGMA__TOKEN is not configured. Set your Figma personal access token via appsettings.json or environment variable FIGMA__TOKEN.");
}

builder.Logging.ClearProviders();
// MCP stdio transport uses stdout exclusively for JSON-RPC messages.
// Route ALL console log output to stderr so it never corrupts the protocol stream.
builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddMcpTools();

var host = builder.Build();

await host.RunAsync();
