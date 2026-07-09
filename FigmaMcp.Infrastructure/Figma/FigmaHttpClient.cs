using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace FigmaMcp.Infrastructure.Figma
{
    public class FigmaHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public FigmaHttpClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;

            var token = config["Figma:Token"];

            if (!string.IsNullOrEmpty(token))
            {
                // Figma personal access tokens are authenticated solely via the
                // X-Figma-Token header. Do NOT also set Authorization: Bearer here —
                // Figma's API treats that as an OAuth token and returns 401 even
                // when a valid X-Figma-Token header is also present.
                _httpClient.DefaultRequestHeaders.Add("X-Figma-Token", token);
            }
        }

        /// <summary>
        /// Exchanges an OAuth authorization code for an access token (and optionally a refresh token).
        /// Requires Figma:ClientId and Figma:ClientSecret to be configured.
        /// </summary>
        public async Task<FigOAuthTokenResponse> ExchangeCodeForTokenAsync(
            string authorizationCode,
            string redirectUri)
        {
            var clientId = _config["Figma:ClientId"]
                ?? throw new InvalidOperationException("Figma:ClientId is not configured.");

            var clientSecret = _config["Figma:ClientSecret"]
                ?? throw new InvalidOperationException("Figma:ClientSecret is not configured.");

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("redirect_uri", redirectUri),
                new KeyValuePair<string, string>("code", authorizationCode),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
            });

            var response = await _httpClient.PostAsync(
                "https://www.figma.com/api/oauth/token",
                formContent);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<FigOAuthTokenResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? throw new InvalidOperationException("Failed to deserialize OAuth token response.");
        }

        public async Task<string> GetAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }

    public class FigOAuthTokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
    }
}