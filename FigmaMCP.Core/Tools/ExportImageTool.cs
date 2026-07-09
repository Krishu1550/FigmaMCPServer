using FigmaMcp.Application.Interfaces;
using ModelContextProtocol.Server;

namespace FigmaMCP.Core.Tools
{
    [McpServerToolType]
    public sealed class ExportImageTool
    {
        private readonly IImageExporter _imageExporter;

        public ExportImageTool(
            IImageExporter imageExporter)
        {
            _imageExporter = imageExporter;
        }

        [McpServerTool(
            Name = "export_figma_image",
            Title = "Export Image"
        )]
        public async Task<string> ExportImage(
            string fileKey,
            string nodeId,
            string format = "png")
        {
            return await _imageExporter.ExportAsync(
                fileKey,
                nodeId,
                format);
        }
    }
}


