using FigmaMcp.Application.Interfaces;

namespace FigmaMcp.Application.UseCases.ParseFigmaUrl
{

    public class ParseFigmaUrlHandler
    {
        private readonly IFigmaUrlParser _parser;

        public ParseFigmaUrlHandler(IFigmaUrlParser parser)
        {
            _parser = parser;
        }

        public ParseFigmaUrlResponse Handle(ParseFigmaUrlRequest request)
        {
            var result = _parser.Parse(request.Url);

            return new ParseFigmaUrlResponse
            {
                FileKey = result.FileKey,
                NodeId = result.NodeId
            };
        }
    }
}