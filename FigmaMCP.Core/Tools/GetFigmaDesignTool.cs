using FigmaMcp.Application.Interfaces;
using FigmaMcp.Application.UseCases.GetFigmaDesign;
using ModelContextProtocol.Server;

namespace FigmaMCP.Core.Tools
{
    [McpServerToolType]
    public sealed class GetFigmaDesignTool
    {
        private readonly GetFigmaDesignHandler _handler;
        private readonly IFigmaUrlParser _urlParser;

        public GetFigmaDesignTool(
            GetFigmaDesignHandler handler,
            IFigmaUrlParser urlParser)
        {
            _handler = handler;
            _urlParser = urlParser;
        }

        [McpServerTool(
            Name = "get_figma_design",
            Title = "Get Figma Design"
        )]
        /// <param name="figmaAccessToken">
        /// Your Figma personal access token
        /// (Settings → Security → Personal access tokens).
        /// This is passed securely per-call and is never stored by the server.
        /// </param>
        /// <param name="figmaUrl">
        /// Full Figma file or node URL, e.g.
        /// https://www.figma.com/design/ABC123/MyFile?node-id=1-2.
        /// When provided, fileKey and nodeId are extracted automatically.
        /// Takes priority over explicit fileKey / nodeId parameters.
        /// </param>
        /// <param name="fileKey">Figma file key (the segment after /design/ in the URL). Used when figmaUrl is not supplied.</param>
        /// <param name="nodeId">Optional node ID to fetch a specific frame/component instead of the whole file.</param>
        public async Task<GetFigmaDesignResponse> GetDesign(
            string? figmaAccessToken = null,
            string? figmaUrl = null,
            string? fileKey  = null,
            string? nodeId   = null)
        {
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

            return await _handler.Handle(
                new GetFigmaDesignRequest
                {
                    FileKey          = fileKey,
                    NodeId           = nodeId,
                    FigmaAccessToken = figmaAccessToken
                });
        }
    }
}
