namespace FigmaMcp.Application.DTOs
{
    public class UiModelDto
    {
        public string ScreenName { get; set; } = string.Empty;

        public double Width { get; set; }

        public double Height { get; set; }

        public List<FigmaNodeDto> Components { get; set; } = new();
    }
}
