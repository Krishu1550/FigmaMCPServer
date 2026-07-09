using FigmaMcp.Application.DTOs;
using FigmaMcp.Application.Interfaces;

namespace FigmaMcp.Infrastructure.Figma
{
    public class UiModelBuilder : IUiModelBuilder
    {
        public UiModelDto Build(FigmaFileDto file)
        {
            var screenBounds = CalculateScreenBounds(file.Nodes);

            var model = new UiModelDto
            {
                ScreenName = file.Name,
                Width = screenBounds.width,
                Height = screenBounds.height,
                Components = MapComponents(file.Nodes)
            };

            return model;
        }

        private static (double width, double height) CalculateScreenBounds(List<FigmaNodeDto> nodes)
        {
            double maxX = 0, maxY = 0;

            CalculateExtents(nodes, ref maxX, ref maxY);

            return (maxX > 0 ? maxX : 375, maxY > 0 ? maxY : 812);
        }

        private static void CalculateExtents(List<FigmaNodeDto> nodes, ref double maxX, ref double maxY)
        {
            foreach (var node in nodes)
            {
                var right = node.X + node.Width;
                var bottom = node.Y + node.Height;

                if (right > maxX) maxX = right;
                if (bottom > maxY) maxY = bottom;

                if (node.Children.Count > 0)
                {
                    CalculateExtents(node.Children, ref maxX, ref maxY);
                }
            }
        }

        private static List<FigmaNodeDto> MapComponents(List<FigmaNodeDto> nodes)
        {
            var result = new List<FigmaNodeDto>();

            foreach (var node in nodes)
            {
                result.Add(MapComponent(node));
            }

            return result;
        }

        private static FigmaNodeDto MapComponent(FigmaNodeDto node)
        {
            var dto = new FigmaNodeDto
            {
                Id = node.Id,
                Name = node.Name,
                Type = node.Type,
                X = node.X,
                Y = node.Y,
                Width = node.Width,
                Height = node.Height,
                Text = node.Text,
                Color = node.Color,
                Children = new List<FigmaNodeDto>()
            };

            foreach (var child in node.Children)
            {
                dto.Children.Add(MapComponent(child));
            }

            return dto;
        }
    }
}