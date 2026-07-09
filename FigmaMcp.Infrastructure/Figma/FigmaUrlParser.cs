using System.Text.RegularExpressions;
using FigmaMcp.Application.Interfaces;

namespace FigmaMcp.Infrastructure.Figma
{
    public class FigmaUrlParser : IFigmaUrlParser
    {
        public (string FileKey, string? NodeId) Parse(string url)
        {
            // Example:
            // https://www.figma.com/design/ABC123/MyApp?node-id=12-34

            var fileKeyMatch = Regex.Match(url, @"design\/([a-zA-Z0-9]+)");

            if (!fileKeyMatch.Success)
                throw new Exception("Invalid Figma URL");

            var fileKey = fileKeyMatch.Groups[1].Value;

            string? nodeId = null;

            var nodeMatch = Regex.Match(url, @"node-id=([0-9\-]+)");

            if (nodeMatch.Success)
            {
                nodeId = nodeMatch.Groups[1].Value.Replace("-", ":");
            }

            return (fileKey, nodeId);
        }
    }
}