using FigmaMcp.Application;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace FigmaMcp.Infrastructure.Figma
{
    public class FigmaHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        // Optional server-wide fallback token (from appsettings / env var).
        // Per-call tokens supplied via FigmaCallContext always win.
        private readonly string? _configToken;

        public FigmaHttpClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _configToken = config["Figma:Token"];
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

        /// <summary>
        /// Performs an authenticated GET against the Figma REST API.
        ///
        /// Token resolution order:
        ///   1. <see cref="FigmaCallContext.Token"/> — set by the MCP tool for the current call.
        ///   2. <c>Figma:Token</c> in appsettings / environment — server-wide fallback.
        ///
        /// A per-request <see cref="HttpRequestMessage"/> is used so the token is NEVER
        /// written to <see cref="System.Net.Http.Headers.HttpRequestHeaders.Authorization"/>
        /// or <c>DefaultRequestHeaders</c>, keeping calls fully isolated from each other.
        /// </summary>
        public async Task<string> GetAsync(string url)
        {
            // Per-call token wins; fall back to the server-wide config token.
            var token = FigmaCallContext.Token ?? _configToken;

            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException(
                    "No Figma access token was provided. " +
                    "Pass your personal access token as the 'figmaAccessToken' tool argument, " +
                    "or set the Figma:Token configuration value.");

            // Build a per-request message so the header is isolated to this call only.
            // Do NOT use DefaultRequestHeaders — that would share the token across all callers.
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("X-Figma-Token", token);

            var response = await _httpClient.SendAsync(request);
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