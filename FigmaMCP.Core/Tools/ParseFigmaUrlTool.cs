using FigmaMcp.Application.UseCases.ParseFigmaUrl;
using ModelContextProtocol.Server;

namespace FigmaMCP.Core.Tools
{
    [McpServerToolType]
    public sealed class ParseFigmaUrlTool
    {
        private readonly ParseFigmaUrlHandler _handler;

        public ParseFigmaUrlTool(ParseFigmaUrlHandler handler)
        {
            _handler = handler;
        }

        [McpServerTool(
            Name = "parse_figma_url",
            Title = "Parse Figma URL"
        )]
        public ParseFigmaUrlResponse ParseUrl(
            string figmaUrl)
        {
            return _handler.Handle(
                new ParseFigmaUrlRequest
                {
                    Url = figmaUrl
                });
        }
    }
  
}
