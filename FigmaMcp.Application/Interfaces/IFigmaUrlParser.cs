namespace FigmaMcp.Application.Interfaces
{
    public interface IFigmaUrlParser
    {
        (string FileKey, string? NodeId) Parse(string url);
    }
}