using FigmaMcp.Application.DTOs;

namespace FigmaMcp.Application.Interfaces
{
    public interface IUiModelBuilder
    {
        UiModelDto Build(FigmaFileDto file);
    }
}
