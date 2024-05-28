namespace Masa.Blazor;

public partial class MDivider : MasaComponentBase
{
    [Parameter] public bool Inset { get; set; }

    [Parameter] public bool Vertical { get; set; }

    [Parameter] public bool Center { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] [Obsolete("This parameter is little usefulness, it will be removed in the future.")]
    public int Height { get; set; }

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

    private static Block _block = new("m-divider");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private ModifierBuilder _wrapperModifierBuilder = _block.Element("wrapper").CreateModifierBuilder();
    private ModifierBuilder _contentModifierBuilder = _block.Element("content").CreateModifierBuilder();

    private string GetWrapperClass()
    {
        return _wrapperModifierBuilder
            .Add(HasContent, Left, Right)
            .Add("center", IsCenter)
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

    private string GetHRClass()
    {
        return _modifierBuilder
            .Add(Inset, Vertical)
            .AddClass(Class, !HasContent)
            .AddTheme(IsDark, IndependentTheme)
            .ToString();
    }

    private string? GetHRStyle()
    {
        return !HasContent ? Style : null;
    }
}