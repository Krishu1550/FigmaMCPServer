namespace FigmaMcp.Domain.Entity
{
    public class FigmaNode
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public NodeType Type { get; set; }

        public FigmaPosition Position { get; set; }

        public FigmaSize Size { get; set; }

        public LayoutMode LayoutMode { get; set; }

        public List<FigmaNode> Children { get; set; } = new();

        // Styling (simplified for AI)
        public string? FillColor { get; set; }

        public string? Text { get; set; }

        public int? FontSize { get; set; }

        public string? FontFamily { get; set; }

        public bool IsVisible { get; set; } = true;
    }
}