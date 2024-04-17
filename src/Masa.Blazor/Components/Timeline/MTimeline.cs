namespace Masa.Blazor
{
    public partial class MTimeline : ThemeContainer
    {
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter] public bool AlignTop { get; set; }

        [Parameter] public bool Dense { get; set; }

        [Parameter] public bool Reverse { get; set; }

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

        private Block _block = new("m-timeline");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return _block.Modifier(AlignTop)
                .And(Dense)
                .And(Reverse)
                .AddTheme(IsDark, IndependentTheme)
                .GenerateCssClasses();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<bool>>(0);
            builder.AddAttribute(1, "Value", Reverse);
            builder.AddAttribute(2, "Name", "Reverse");
            builder.AddAttribute(3, "ChildContent", (RenderFragment)base.BuildRenderTree);
            builder.CloseComponent();
        }
    }
}