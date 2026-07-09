namespace FigmaMcp.Domain.Entity
{
    public class FigmaComponent : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Key { get; set; } = string.Empty;

        public FigmaNode Root { get; set; } = new();
    }
}
