
using FigmaMcp.Application.Interfaces;

namespace FigmaMcp.Infrastructure.Figma
{
    /// <summary>
    /// Adapts the low-level, caching <see cref="FigmaApiClient"/> to the
    /// <see cref="IFigmaService"/> contract consumed by the application layer.
    /// </summary>
    public class FigmaService : IFigmaService
    {
        private readonly FigmaApiClient _apiClient;

        public FigmaService(FigmaApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Task<string> GetFileAsync(string fileKey)
        {
            return _apiClient.GetFileAsync(fileKey);
        }

        public Task<string> GetNodeAsync(string fileKey, string nodeId)
        {
            return _apiClient.GetNodesAsync(fileKey, nodeId);
        }

        public Task<string> GetImagesAsync(string fileKey, string nodeId)
        {
            return _apiClient.GetImagesAsync(fileKey, nodeId);
        }
    }
}
