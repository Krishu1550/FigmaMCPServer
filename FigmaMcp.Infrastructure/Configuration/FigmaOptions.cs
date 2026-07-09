namespace FigmaMcp.Infrastructure.Configuration
{
    public class FigmaOptions
    {
        public string Token { get; set; } = string.Empty;

        public string BaseUrl { get; set; } = "https://api.figma.com/v1";

        public string ClientId { get; set; } = string.Empty;

        public string ClientSecret { get; set; } = string.Empty;
    }
}