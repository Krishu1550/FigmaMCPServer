using FigmaMcp.Application;
using FigmaMcp.Application.Interfaces;

namespace FigmaMcp.Application.UseCases.GetFigmaDesign
{
    public class GetFigmaDesignHandler
    {
        private readonly IFigmaService _figma;

        public GetFigmaDesignHandler(IFigmaService figma)
        {
            _figma = figma;
        }

        public async Task<GetFigmaDesignResponse> Handle(GetFigmaDesignRequest request)
        {
            // Propagate the caller's token into the async context so FigmaHttpClient
            // picks it up on every downstream HTTP call within this invocation.
            if (!string.IsNullOrEmpty(request.FigmaAccessToken))
                FigmaCallContext.Token = request.FigmaAccessToken;

            string json;

            if (!string.IsNullOrEmpty(request.NodeId))
            {
                json = await _figma.GetNodeAsync(request.FileKey, request.NodeId);
            }
            else
            {
                json = await _figma.GetFileAsync(request.FileKey);
            }

            return new GetFigmaDesignResponse
            {
                RawJson = json
            };
        }
    }
}
