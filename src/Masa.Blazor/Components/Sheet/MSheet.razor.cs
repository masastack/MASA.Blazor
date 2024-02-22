using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor
{
    public partial class MSheet : MasaComponentBase
    {
#if NET8_0_OR_GREATER
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;
#endif
        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] [MasaApiParameter("div")] public virtual string Tag { get; set; } = "div";

        [Parameter] public bool? Show { get; set; }

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

        [Parameter] public bool Outlined { get; set; }

        [Parameter] public bool Shaped { get; set; }

        [Parameter] public StringNumber? Elevation { get; set; }

        [Parameter] public StringBoolean? Rounded { get; set; }

        [Parameter] public bool Tile { get; set; }

        [Parameter] public string? Color { get; set; }

        [Parameter] public StringNumber? Width { get; set; }

        [Parameter] public StringNumber? MaxWidth { get; set; }

        [Parameter] public StringNumber? MinWidth { get; set; }

        [Parameter] public StringNumber? Height { get; set; }

        [Parameter] public StringNumber? MaxHeight { get; set; }

        [Parameter] public StringNumber? MinHeight { get; set; }

        private bool IndependentTheme =>
            (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

#if NET8_0_OR_GREATER
        if (MasaBlazor.IsSsr && !IndependentTheme)
        {
            CascadingIsDark = MasaBlazor.Theme.Dark;
        }
#endif
        }

        private Block _block = new("m-sheet");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return _block.Modifier(Outlined).And(Shaped).AddTheme(IsDark, IndependentTheme).AddElevation(Elevation)
                .AddRounded(Rounded, Tile).AddBackgroundColor(Color).GenerateCssClasses();
        }

        protected override IEnumerable<string> BuildComponentStyle()
        {
            return new StyleBuilder()
                .AddHeight(Height)
                .AddWidth(Width)
                .AddMinWidth(MinWidth)
                .AddMaxWidth(MaxWidth)
                .AddMinHeight(MinHeight)
                .AddMaxHeight(MaxHeight)
                .AddBackgroundColor(Color)
                .GenerateCssStyles();
        }
    }
}