using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public class MBorder : Container
{
    [Parameter]
    [MasaApiParameter(Borders.Left)]
    public Borders Border { get; set; } = Borders.Left;

    [Parameter]
    [MasaApiParameter("primary")]
    public string? Color { get; set; } = "primary";

    [Parameter] public bool Offset { get; set; }

    [Parameter] [MasaApiParameter(true)] public bool Value { get; set; } = true;

    [Obsolete("Use Size instead.")]
    [Parameter]
    [MasaApiParameter(8)]
    public StringNumber Width { get; set; } = 8;

    [Parameter] [MasaApiParameter(8)] public StringNumber Size { get; set; } = 8;

    private Block _block = new("m-border");

    private bool Active => Value && Border != Borders.None;

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier(Border.ToString())
            .And("active", Active)
            .GenerateCssClasses();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        var size = Size;
#pragma warning disable CS0618 // Type or member is obsolete
        if (Width != 8)
        {
            size = Width;
        }
#pragma warning restore CS0618 // Type or member is obsolete

        var styleBuilder = StyleBuilder.Create()
            .Add("--m-border-size", size.ToUnit())
            .Add("--m-border-offset", Offset ? $"-{size.ToUnit()}" : "0");

        if (Color != null)
        {
            if (Color.StartsWith("#") || Color.StartsWith("rgb"))
            {
                styleBuilder.Add("--m-border-color", Color);
            }
            else
            {
                styleBuilder.Add("--m-border-color", $"var(--m-theme-{Color})");
            }
        }

        return styleBuilder.GenerateCssStyles();
    }
}