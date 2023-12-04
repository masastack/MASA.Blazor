﻿namespace Masa.Blazor
{
    public partial class MSheet : BSheet, ISheet
    {
#if NET8_0_OR_GREATER
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;
#endif
        
        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public StringNumber? Elevation { get; set; }

        [Parameter]
        public StringBoolean? Rounded { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public string? Color { get; set; }

        [Parameter]
        public StringNumber? Width { get; set; }

        [Parameter]
        public StringNumber? MaxWidth { get; set; }

        [Parameter]
        public StringNumber? MinWidth { get; set; }

        [Parameter]
        public StringNumber? Height { get; set; }

        [Parameter]
        public StringNumber? MaxHeight { get; set; }

        [Parameter]
        public StringNumber? MinHeight { get; set; }

        private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

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

        protected override void SetComponentClass()
        {
            var prefix = "m-sheet";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--outlined", () => Outlined)
                        .AddIf($"{prefix}--shaped", () => Shaped)
                        .AddTheme(IsDark, IndependentTheme)
                        .AddElevation(Elevation)
                        .AddRounded(Rounded, Tile)
                        .AddBackgroundColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(Height)
                        .AddWidth(Width)
                        .AddMinWidth(MinWidth)
                        .AddMaxWidth(MaxWidth)
                        .AddMinHeight(MinHeight)
                        .AddMaxHeight(MaxHeight)
                        .AddBackgroundColor(Color);
                });

            AbstractProvider
                .Apply(typeof(BSheetBody<>), typeof(BSheetBody<ISheet>));
        }
    }
}
