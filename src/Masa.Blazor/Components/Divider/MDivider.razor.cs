namespace Masa.Blazor;

public partial class MDivider : MasaComponentBase
{
    [Parameter] public bool Inset { get; set; }

    [Parameter] public bool Vertical { get; set; }

    [Parameter] public bool Center { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public int Height { get; set; }

    [Parameter] public bool Left { get; set; }

    [Parameter] public bool Right { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    public bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            if (Light)
            {
                return false;
            }

            return CascadingIsDark;
        }
    }

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

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif

    private Block _block = new("m-divider");

    private string GetWrapperClass()
    {
        return _block.Element("wrapper")
            .Modifier(HasContent)
            .And("center", IsCenter)
            .And(Left)
            .And(Right)
            .ToString();
    }

    private string? GetWrapperStyle()
    {
        StringBuilder stringBuilder = new();

        if (!HasContent)
        {
            stringBuilder.Append("display:contents; ");
        }

        if (PaddingY > 0)
        {
            stringBuilder.Append("padding: {PaddingY}px 0;");
        }

        return stringBuilder.Length > 0 ? stringBuilder.ToString() : null;
    }

    private string? GetHRClass()
    {
        return _block.Modifier(Inset)
            .And(Vertical)
            .AddClass(Class, !HasContent)
            .AddTheme(IsDark, IndependentTheme)
            .ToString();
    }

    private string? GetHRStyle()
    {
        return !HasContent ? Style : null;
    }
}