using FigmaMcp.Application.Interfaces;

namespace FigmaMcp.Application.UseCases.BuildUiModel
{
    public class BuildUiModelHandler
    {
        private readonly IDesignParser _parser;
        private readonly IUiModelBuilder _builder;

        public BuildUiModelHandler(
            IDesignParser parser,
            IUiModelBuilder builder)
        {
            _parser = parser;
            _builder = builder;
        }

        public BuildUiModelResponse Handle(BuildUiModelRequest request)
        {
            var file = _parser.Parse(request.RawFigmaJson);

            var ui = _builder.Build(file);

            return new BuildUiModelResponse
            {
                UiModel = ui
            };
        }
    }
}
