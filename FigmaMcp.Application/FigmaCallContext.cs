namespace FigmaMcp.Application
{
    /// <summary>
    /// Stores the Figma personal-access-token for the <em>current async call chain</em>.
    ///
    /// MCP stdio processes tool calls sequentially, so an <see cref="AsyncLocal{T}"/>
    /// gives every tool invocation its own isolated token without requiring HTTP context
    /// or any other ambient infrastructure.
    ///
    /// Usage (MCP tool method, before calling any Figma service):
    /// <code>
    ///   FigmaCallContext.Token = figmaAccessToken;
    /// </code>
    /// </summary>
    public static class FigmaCallContext
    {
        private static readonly AsyncLocal<string?> _token = new();

        /// <summary>
        /// Gets or sets the Figma personal-access-token for the current call.
        /// Set this at the top of each MCP tool handler before any downstream
        /// Figma API calls are made.
        /// </summary>
        public static string? Token
        {
            get => _token.Value;
            set => _token.Value = value;
        }

        /// <summary>
        /// Resets the token for the current async context.
        /// AsyncLocal isolation means clean-up is not strictly required,
        /// but can be called for explicitness.
        /// </summary>
        public static void Clear() => _token.Value = null;
    }
}
