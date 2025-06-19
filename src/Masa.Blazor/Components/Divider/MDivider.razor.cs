namespace Masa.Blazor;

public partial class MDivider : ThemeComponentBase
{
    [Parameter] public bool Inset { get; set; }

    [Parameter] public bool Vertical { get; set; }

    [Parameter] public bool Center { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter]
    [Obsolete("This parameter will be removed in the future, please use CSS instead.")]
    public int Height { get; set; }

    [Parameter] public bool Left { get; set; }

    [Parameter] public bool Right { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.6.0")]
    public int Length { get; set; }

    private bool HasContent => ChildContent is not null;

    private bool IsCenter => HasContent && (!Left && !Right || Center);

    private int PaddingY
    {
        get
        {
            if (Height <= 0)
            {
                return 0;
            }

            return Height / 2;
        }
    }

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    private static Block _block = new("m-divider");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private ModifierBuilder _wrapperModifierBuilder = _block.Element("wrapper").CreateModifierBuilder();
    private ModifierBuilder _contentModifierBuilder = _block.Element("content").CreateModifierBuilder();

    private string GetWrapperClass()
    {
        return _wrapperModifierBuilder
            .Add(HasContent, Left, Right)
            .Add("center", IsCenter)
            .AddClass(Class)
            .ToString();
    }

    private string? GetWrapperStyle()
    {
        StyleBuilder styleBuilder = new();

        if (!HasContent)
        {
            styleBuilder.Add("display:contents;");
        }

        if (Length > 0)
        {
            styleBuilder.AddLength(Vertical ? "height" : "width", Length);
        }

        if (PaddingY > 0)
        {
            styleBuilder.Add($"padding: {PaddingY}px 0;");
        }

        styleBuilder.Add(Style);

        return styleBuilder.ToString();
    }

    private string GetHRClass()
    {
        return _modifierBuilder
            .Add(Inset, Vertical)
            .AddClass(Class, !HasContent)
            .AddTheme(ComputedTheme)
            .ToString();
    }

    private string? GetHRStyle()
    {
        StyleBuilder styleBuilder = new();

        if (!HasContent)
        {
            styleBuilder.Add(Style);
        }

        if (Vertical && Length > 0)
        {
            styleBuilder.AddLength("min-height", 0);
        }

        return styleBuilder.ToString();
    }
}