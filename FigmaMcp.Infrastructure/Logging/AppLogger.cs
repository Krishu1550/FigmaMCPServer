using Microsoft.Extensions.Logging;

namespace FigmaMcp.Infrastructure.Logging
{
    public class AppLogger
    {
        private readonly ILogger<AppLogger> _logger;

        public AppLogger(ILogger<AppLogger> logger)
        {
            _logger = logger;
        }

        public void Info(string message) => _logger.LogInformation(message);

        public void Error(string message, Exception ex)
            => _logger.LogError(ex, message);
    }
}
