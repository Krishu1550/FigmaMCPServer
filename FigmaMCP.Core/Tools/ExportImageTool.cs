using FigmaMcp.Application;
using FigmaMcp.Application.Interfaces;
using ModelContextProtocol.Server;

namespace FigmaMCP.Core.Tools
{
    [McpServerToolType]
    public sealed class ExportImageTool
    {
        private readonly IImageExporter _imageExporter;
        private readonly IFigmaUrlParser _urlParser;

        public ExportImageTool(
            IImageExporter imageExporter,
            IFigmaUrlParser urlParser)
        {
            _imageExporter = imageExporter;
            _urlParser = urlParser;
        }

        [McpServerTool(
            Name = "export_figma_image",
            Title = "Export Image"
        )]
        /// <param name="figmaAccessToken">
        /// Your Figma personal access token
        /// (Settings → Security → Personal access tokens).
        /// Passed securely per-call; never stored by the server.
        /// </param>
        /// <param name="figmaUrl">
        /// Full Figma URL, e.g. https://www.figma.com/design/ABC123/File?node-id=1-2.
        /// When provided, fileKey and nodeId are extracted automatically.
        /// Takes priority over explicit fileKey / nodeId parameters.
        /// </param>
        /// <param name="fileKey">Figma file key. Used when figmaUrl is not supplied.</param>
        /// <param name="nodeId">Node ID of the frame/component to export.</param>
        /// <param name="format">Image format: png (default), jpg, svg, or pdf.</param>
        public async Task<string> ExportImage(
            string? figmaAccessToken = null,
            string? figmaUrl = null,
            string? fileKey  = null,
            string? nodeId   = null,
            string  format   = "png")
        {
            // Set the per-call token so FigmaHttpClient uses it for this invocation.
            FigmaCallContext.Token = figmaAccessToken;

            // If a full URL was supplied, parse it to extract fileKey / nodeId.
            if (!string.IsNullOrWhiteSpace(figmaUrl))
            {
                var parsed = _urlParser.Parse(figmaUrl);
                fileKey = parsed.FileKey;
                nodeId  = parsed.NodeId;
            }

            if (string.IsNullOrWhiteSpace(fileKey))
                throw new ArgumentException(
                    "Either 'figmaUrl' or 'fileKey' must be provided.");

            if (string.IsNullOrWhiteSpace(nodeId))
                throw new ArgumentException(
                    "A 'nodeId' is required to export an image. " +
                    "Provide it explicitly or include a node-id in the figmaUrl.");

            return await _imageExporter.ExportAsync(fileKey, nodeId, format);
        }
    }
}
