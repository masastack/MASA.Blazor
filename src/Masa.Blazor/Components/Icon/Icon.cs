namespace Masa.Blazor;

[GenerateOneOf]
public partial class Icon : OneOfBase<string, SvgPath, SvgPath[]>
{
    public bool IsAlias => IsT0 && AsT0?.StartsWith("$") is true;

    public bool IsCssFont => IsT0 && !AsT0.StartsWith("$");

    public bool IsSvg => IsT1 || IsT2;

    public SvgPath[] GetSvgPaths()
    {
        if (IsT0)
        {
            return Array.Empty<SvgPath>();
        }

        return IsT2 ? AsT2 : new[] { AsT1 };
    }
}