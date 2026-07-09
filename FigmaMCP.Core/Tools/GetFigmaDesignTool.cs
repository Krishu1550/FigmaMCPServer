using FigmaMcp.Application.UseCases.GetFigmaDesign;
using ModelContextProtocol.Server;

namespace FigmaMCP.Core.Tools
{
    [McpServerToolType]
    public sealed class GetFigmaDesignTool
    {
        private readonly GetFigmaDesignHandler _handler;

        public GetFigmaDesignTool(
            GetFigmaDesignHandler handler)
        {
            _handler = handler;
        }

        [McpServerTool(
            Name = "get_figma_design",
            Title = "Get Figma Design"
        )]
        public async Task<GetFigmaDesignResponse> GetDesign(
            string fileKey,
            string? nodeId = null)
        {
            return await _handler.Handle(
                new GetFigmaDesignRequest
                {
                    FileKey = fileKey,
                    NodeId = nodeId
                });
        }
    }
}
