namespace FigmaMcp.Application.DTOs
{
    public class FigmaFileDto
    {
        public string Name { get; set; } = string.Empty;

        public string FileKey { get; set; } = string.Empty;

        public List<FigmaNodeDto> Nodes { get; set; } = new();
    }
}
