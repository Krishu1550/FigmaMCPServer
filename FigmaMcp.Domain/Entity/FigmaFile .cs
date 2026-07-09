namespace FigmaMcp.Domain.Entity
{
    public class FigmaFile : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string FileKey { get; set; } = string.Empty;

        public List<FigmaPage> Pages { get; set; } = new();
    }
}
