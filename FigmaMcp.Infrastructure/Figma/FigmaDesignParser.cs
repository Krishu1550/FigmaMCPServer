using System.Text.Json;
using FigmaMcp.Application.DTOs;
using FigmaMcp.Application.Interfaces;

namespace FigmaMcp.Infrastructure.Figma
{
    public class FigmaDesignParser : IDesignParser
    {
        public FigmaFileDto Parse(string figmaJson)
        {
            var response = JsonSerializer.Deserialize<FigmaFileResponse>(figmaJson);

            if (response == null)
                throw new InvalidOperationException("Failed to parse Figma JSON response.");

            var dto = new FigmaFileDto
            {
                Name = response.Name,
                FileKey = string.Empty,
                Nodes = new List<FigmaNodeDto>()
            };

            if (response.Document?.Children != null)
            {
                foreach (var child in response.Document.Children)
                {
                    dto.Nodes.Add(MapNode(child));
                }
            }

            return dto;
        }

        private static FigmaNodeDto MapNode(FigmaDocumentNode node)
        {
            var dto = new FigmaNodeDto
            {
                Id = node.Id,
                Name = node.Name,
                Type = node.Type,
                X = node.AbsoluteBoundingBox?.X ?? 0,
                Y = node.AbsoluteBoundingBox?.Y ?? 0,
                Width = node.AbsoluteBoundingBox?.Width ?? 0,
                Height = node.AbsoluteBoundingBox?.Height ?? 0,
                Text = node.Characters,
                Color = ExtractHexColor(node.Fills),
                Children = new List<FigmaNodeDto>()
            };

            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    dto.Children.Add(MapNode(child));
                }
            }

            return dto;
        }

        private static string? ExtractHexColor(List<FigmaPaint>? fills)
        {
            if (fills == null || fills.Count == 0)
                return null;

            var solidFill = fills.Find(f =>
                f.Type == "SOLID" && f.Visible && f.Color != null);

            if (solidFill?.Color == null)
                return null;

            var c = solidFill.Color;
            var r = (int)(c.R * 255);
            var g = (int)(c.G * 255);
            var b = (int)(c.B * 255);
            return $"#{r:X2}{g:X2}{b:X2}";
        }
    }
}