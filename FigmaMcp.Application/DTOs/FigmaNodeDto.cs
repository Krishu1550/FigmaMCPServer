namespace FigmaMcp.Application.DTOs
{
    public class FigmaNodeDto
    {
        public string Id { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public double X { get; set; }

        public double Y { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public string? Text { get; set; }

        public string? Color { get; set; }

        public List<FigmaNodeDto> Children { get; set; } = new();
    }
}
