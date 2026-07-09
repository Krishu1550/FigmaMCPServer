using FigmaMcp.Application.DTOs;

namespace FigmaMcp.Application.Interfaces
{
    public interface IDesignParser
    {
        FigmaFileDto Parse(string figmaJson);
    }
}
