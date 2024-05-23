namespace Masa.Blazor;

public class MSubheader : ThemeContainer
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public bool Inset { get; set; }

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

    private Block _block = new("m-subheader");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Modifier(Inset).AddTheme(IsDark, IndependentTheme).GenerateCssClasses();
    }
}