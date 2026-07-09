using FigmaMcp.Application.DTOs;

namespace FigmaMcp.Application.UseCases.BuildUiModel
{
    public class BuildUiModelResponse
    {
        public UiModelDto UiModel { get; set; } = new();
    }
}
