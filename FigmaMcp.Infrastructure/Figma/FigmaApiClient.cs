using FigmaMcp.Infrastructure.Caching;

namespace FigmaMcp.Infrastructure.Figma
{
    public class FigmaApiClient
    {
        private readonly FigmaHttpClient _client;
        private readonly MemoryCacheService _cache;

        public FigmaApiClient(FigmaHttpClient client, MemoryCacheService cache)
        {
            _client = client;
            _cache = cache;
        }

        // Get full file
        public async Task<string> GetFileAsync(string fileKey)
        {
            var cacheKey = $"figma:file:{fileKey}";
            
            return await _cache.GetOrCreateAsync(cacheKey, async () =>
            {
                return await _client.GetAsync($"files/{fileKey}");
            });
        }

        // Get specific nodes
        public async Task<string> GetNodesAsync(string fileKey, string nodeIds)
        {
            var cacheKey = $"figma:nodes:{fileKey}:{nodeIds}";
            
            return await _cache.GetOrCreateAsync(cacheKey, async () =>
            {
                return await _client.GetAsync($"files/{fileKey}/nodes?ids={nodeIds}");
            });
        }

        // Get images
        public async Task<string> GetImagesAsync(string fileKey, string nodeIds)
        {
            var cacheKey = $"figma:images:{fileKey}:{nodeIds}";
            
            return await _cache.GetOrCreateAsync(cacheKey, async () =>
            {
                return await _client.GetAsync($"images/{fileKey}?ids={nodeIds}&format=png");
            });
        }
    }
}