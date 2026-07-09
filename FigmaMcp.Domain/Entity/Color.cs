namespace FigmaMcp.Domain.Entity
{
    public readonly record struct FigmaColor
    {
        public float R { get; init; }
        public float G { get; init; }
        public float B { get; init; }
        public float A { get; init; }

        public string ToHex()
        {
            int r = (int)(R * 255);
            int g = (int)(G * 255);
            int b = (int)(B * 255);

            return $"#{r:X2}{g:X2}{b:X2}";
        }
    }
}
