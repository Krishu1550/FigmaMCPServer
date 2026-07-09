using System.Text.Json.Serialization;

namespace FigmaMcp.Infrastructure.Figma
{
    /// <summary>
    /// Root response from GET /v1/files/{fileKey}
    /// </summary>
    public class FigmaFileResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("document")]
        public FigmaDocumentNode Document { get; set; } = new();

        [JsonPropertyName("components")]
        public Dictionary<string, FigmaComponentDefinition>? Components { get; set; }

        [JsonPropertyName("styles")]
        public Dictionary<string, FigmaStyleDefinition>? Styles { get; set; }
    }

    /// <summary>
    /// Root response from GET /v1/files/{fileKey}/nodes?ids={nodeIds}
    /// </summary>
    public class FigmaNodesResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("nodes")]
        public Dictionary<string, FigmaNodeDocument>? Nodes { get; set; }
    }

    public class FigmaNodeDocument
    {
        [JsonPropertyName("document")]
        public FigmaDocumentNode Document { get; set; } = new();

        [JsonPropertyName("components")]
        public Dictionary<string, FigmaComponentDefinition>? Components { get; set; }
    }

    public class FigmaDocumentNode
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("visible")]
        public bool Visible { get; set; } = true;

        [JsonPropertyName("opacity")]
        public float Opacity { get; set; } = 1;

        [JsonPropertyName("absoluteBoundingBox")]
        public FigmaBoundingBox? AbsoluteBoundingBox { get; set; }

        [JsonPropertyName("fills")]
        public List<FigmaPaint>? Fills { get; set; }

        [JsonPropertyName("strokes")]
        public List<FigmaPaint>? Strokes { get; set; }

        [JsonPropertyName("strokeWeight")]
        public FigmaStrokeWeight? StrokeWeight { get; set; }

        [JsonPropertyName("cornerRadius")]
        public float? CornerRadius { get; set; }

        [JsonPropertyName("children")]
        public List<FigmaDocumentNode>? Children { get; set; }

        [JsonPropertyName("characters")]
        public string? Characters { get; set; }

        [JsonPropertyName("style")]
        public FigmaTypeStyle? Style { get; set; }

        [JsonPropertyName("layoutMode")]
        public string? LayoutMode { get; set; }

        [JsonPropertyName("primaryAxisAlignItems")]
        public string? PrimaryAxisAlignItems { get; set; }

        [JsonPropertyName("counterAxisAlignItems")]
        public string? CounterAxisAlignItems { get; set; }

        [JsonPropertyName("itemSpacing")]
        public float? ItemSpacing { get; set; }

        [JsonPropertyName("paddingLeft")]
        public float? PaddingLeft { get; set; }

        [JsonPropertyName("paddingRight")]
        public float? PaddingRight { get; set; }

        [JsonPropertyName("paddingTop")]
        public float? PaddingTop { get; set; }

        [JsonPropertyName("paddingBottom")]
        public float? PaddingBottom { get; set; }

        [JsonPropertyName("componentId")]
        public string? ComponentId { get; set; }
    }

    public class FigmaBoundingBox
    {
        [JsonPropertyName("x")]
        public double X { get; set; }

        [JsonPropertyName("y")]
        public double Y { get; set; }

        [JsonPropertyName("width")]
        public double Width { get; set; }

        [JsonPropertyName("height")]
        public double Height { get; set; }
    }

    public class FigmaPaint
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("visible")]
        public bool Visible { get; set; } = true;

        [JsonPropertyName("opacity")]
        public float? Opacity { get; set; }

        [JsonPropertyName("color")]
        public FigmaColor? Color { get; set; }
    }

    public class FigmaColor
    {
        [JsonPropertyName("r")]
        public float R { get; set; }

        [JsonPropertyName("g")]
        public float G { get; set; }

        [JsonPropertyName("b")]
        public float B { get; set; }

        [JsonPropertyName("a")]
        public float A { get; set; } = 1;
    }

    public class FigmaStrokeWeight
    {
        [JsonPropertyName("top")]
        public float? Top { get; set; }

        [JsonPropertyName("right")]
        public float? Right { get; set; }

        [JsonPropertyName("bottom")]
        public float? Bottom { get; set; }

        [JsonPropertyName("left")]
        public float? Left { get; set; }
    }

    public class FigmaTypeStyle
    {
        [JsonPropertyName("fontFamily")]
        public string? FontFamily { get; set; }

        [JsonPropertyName("fontPostScriptName")]
        public string? FontPostScriptName { get; set; }

        [JsonPropertyName("fontSize")]
        public float? FontSize { get; set; }

        [JsonPropertyName("fontWeight")]
        public int? FontWeight { get; set; }

        [JsonPropertyName("textAlignHorizontal")]
        public string? TextAlignHorizontal { get; set; }

        [JsonPropertyName("textAlignVertical")]
        public string? TextAlignVertical { get; set; }

        [JsonPropertyName("letterSpacing")]
        public float? LetterSpacing { get; set; }

        [JsonPropertyName("lineHeightPx")]
        public float? LineHeightPx { get; set; }
    }

    public class FigmaComponentDefinition
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    public class FigmaStyleDefinition
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("styleType")]
        public string StyleType { get; set; } = string.Empty;
    }

    /// <summary>
    /// Response from GET /v1/images/{fileKey}?ids={nodeIds}
    /// </summary>
    public class FigmaImagesResponse
    {
        [JsonPropertyName("images")]
        public Dictionary<string, string?> Images { get; set; } = new();
    }
}