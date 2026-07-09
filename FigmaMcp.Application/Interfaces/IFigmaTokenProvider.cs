namespace FigmaMcp.Application.Interfaces
{
    /// <summary>
    /// Resolves the Figma personal access token to use for the current MCP call.
    ///
    /// This exists so a single hosted server instance can serve many users:
    /// each caller supplies their own token (e.g. via a request header), and this
    /// abstraction is what lets that token flow down to the Figma API client
    /// without ever being shared between callers.
    /// </summary>
    public interface IFigmaTokenProvider
    {
        /// <summary>
        /// Gets the Figma personal access token for the current request/session,
        /// or <c>null</c>/empty if none could be resolved.
        /// </summary>
        string? GetToken();
    }
}
