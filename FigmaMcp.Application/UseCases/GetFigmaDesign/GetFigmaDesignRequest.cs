namespace FigmaMcp.Application.UseCases.GetFigmaDesign
{
    public class GetFigmaDesignRequest
    {
        public string FileKey { get; set; } = string.Empty;

        public string? NodeId { get; set; }
    }
}
