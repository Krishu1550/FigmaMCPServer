using FigmaMcp.Application.UseCases.BuildUiModel;
using ModelContextProtocol.Server;

namespace FigmaMCP.Core.Tools
{
    [McpServerToolType]
    public sealed class GenerateUiModelTool
    {
        private readonly BuildUiModelHandler _handler;

        public GenerateUiModelTool(
            BuildUiModelHandler handler)
        {
            _handler = handler;
        }

        [McpServerTool(
            Name = "generate_ui_model",
            Title = "Generate UI Model"
        )]
        public BuildUiModelResponse Generate(
            string figmaJson)
        {
            return _handler.Handle(
                new BuildUiModelRequest
                {
                    RawFigmaJson = figmaJson
                });
        }
    }
}
