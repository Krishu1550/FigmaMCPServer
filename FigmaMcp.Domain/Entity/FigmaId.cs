namespace FigmaMcp.Domain.Entity
{
    public readonly record struct FigmaId(string Value)
    {
        public override string ToString() => Value;
    }
}
