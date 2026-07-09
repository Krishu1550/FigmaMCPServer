namespace FigmaMcp.Domain.Entity
{
    public class FigmaPage : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public List<FigmaNode> Nodes { get; set; } = new();
    }
}