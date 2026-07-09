namespace FigmaMcp.Application.UseCases.GetFigmaDesign
{
    public class GetFigmaDesignRequest
    {
        public string FileKey { get; set; } = string.Empty;

        public string? NodeId { get; set; }

        /// <summary>
        /// Caller's Figma personal-access-token for this specific request.
        /// When set, overrides any server-wide token in configuration.
        /// </summary>
        public string? FigmaAccessToken { get; set; }
    }
}
