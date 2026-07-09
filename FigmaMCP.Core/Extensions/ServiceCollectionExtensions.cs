using Microsoft.Extensions.DependencyInjection;
using ModelContextProtocol.Server;

namespace FigmaMCP.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMcpTools(
            this IServiceCollection services)
        {
            // Register the MCP server as a hosted service wired to stdio transport,
            // so the generic host's RunAsync() drives the JSON-RPC stdin/stdout loop.
            // WithToolsFromAssembly() scans this assembly for [McpServerToolType] classes
            // and registers their [McpServerTool] methods (and the tool classes themselves) in DI.
            services
                .AddMcpServer()
                .WithStdioServerTransport()
                .WithToolsFromAssembly();

            return services;
        }
    }
}