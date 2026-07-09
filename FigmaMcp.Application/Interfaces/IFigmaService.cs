namespace FigmaMcp.Application.Interfaces
{
    public interface IFigmaService
    {
        Task<string> GetFileAsync(string fileKey);

        Task<string> GetNodeAsync(string fileKey, string nodeId);

        Task<string> GetImagesAsync(string fileKey, string nodeId);
    }
}
