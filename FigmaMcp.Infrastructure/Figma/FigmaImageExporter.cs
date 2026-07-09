using System.Text.Json;
using FigmaMcp.Application.Interfaces;

namespace FigmaMcp.Infrastructure.Figma
{
    public class FigmaImageExporter : IImageExporter
    {
        private readonly FigmaApiClient _apiClient;

        public FigmaImageExporter(FigmaApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<string> ExportAsync(
            string fileKey,
            string nodeId,
            string format)
        {
            var json = await _apiClient.GetImagesAsync(fileKey, nodeId);

            var response = JsonSerializer.Deserialize<FigmaImagesResponse>(json);

            if (response?.Images == null || !response.Images.TryGetValue(nodeId, out var url) || url == null)
            {
                throw new InvalidOperationException(
                    $"Failed to export image for node {nodeId} in file {fileKey}.");
            }

            return url;
        }
    }
}