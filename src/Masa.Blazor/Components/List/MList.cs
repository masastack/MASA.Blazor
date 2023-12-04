﻿namespace Masa.Blazor
{
    public partial class MList : BList, IThemeable
    {
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;
        
        /// <summary>
        /// Removes elevation (box-shadow) and adds a thin border.
        /// </summary>
        [Parameter]
        public bool Outlined { get; set; }

        /// <summary>
        /// Provides an alternative active style for MListItem
        /// </summary>
        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public StringNumber? Elevation { get; set; }

        /// <summary>
        /// Lowers max height of list tiles
        /// </summary>
        [Parameter]
        public bool Dense { get; set; }

        /// <summary>
        /// Disables all children MListItem components
        /// </summary>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// Remove the highlighted background on active MListItems
        /// </summary>
        [Parameter]
        public bool Flat { get; set; }

        /// <summary>
        /// An alternative styling that reduces MListItem width and rounds the corners. Typically used with MNavigationDrawer
        /// </summary>
        [Parameter]
        public bool Nav { get; set; }

        /// <summary>
        /// Rounds the MListItem edges
        /// </summary>
        [Parameter]
        public bool Rounded { get; set; }

        /// <summary>
        /// Removes top padding. Used when previous sibling is a header
        /// </summary>
        [Parameter]
        public bool Subheader { get; set; }

        /// <summary>
        /// Increases list-item height for two lines. This prop uses line-clamp and is not supported in all browsers.
        /// </summary>
        [Parameter]
        public bool TwoLine { get; set; }

        /// <summary>
        /// Increases list-item height for three lines. This prop uses line-clamp and is not supported in all browsers.
        /// </summary>
        [Parameter]
        public bool ThreeLine { get; set; }

        /// <summary>
        /// Sets the height for the component.
        /// </summary>
        [Parameter]
        public StringNumber? Height { get; set; }

        /// <summary>
        /// Sets the maximum height for the component.
        /// </summary>
        [Parameter]
        public StringNumber? MinHeight { get; set; }

        /// <summary>
        /// Sets the minimum width for the component.
        /// </summary>
        [Parameter]
        public StringNumber? MinWidth { get; set; }

        /// <summary>
        /// Sets the maximum height for the component.
        /// </summary>
        [Parameter]
        public StringNumber? MaxHeight { get; set; }

        /// <summary>
        /// Sets the maximum width for the component.
        /// </summary>
        [Parameter]
        public StringNumber? MaxWidth { get; set; }

        /// <summary>
        /// Sets the width for the component.
        /// </summary>
        [Parameter]
        public StringNumber? Width { get; set; }

        private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

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

        protected override void SetComponentClass()
        {
            var prefix = "m-list";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-sheet")
                        .AddIf("m-sheet--outlined", () => Outlined)
                        .AddIf("m-sheet--shaped", () => Shaped)
                        .AddTheme(IsDark, IndependentTheme)
                        .Add("m-list")
                        .AddIf(() => $"elevation-{Elevation!.Value}", () => Elevation != null)
                        .AddIf($"{prefix}--dense", () => Dense)
                        .AddIf($"{prefix}--disabled", () => Disabled)
                        .AddIf($"{prefix}--flat", () => Flat)
                        .AddIf($"{prefix}--nav", () => Nav)
                        .AddIf($"{prefix}--rounded", () => Rounded)
                        .AddIf($"{prefix}--subheader", () => Subheader)
                        .AddIf($"{prefix}--two-line", () => TwoLine)
                        .AddIf($"{prefix}--three-line", () => ThreeLine);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(Height)
                        .AddWidth(Width)
                        .AddMinWidth(MinWidth)
                        .AddMaxWidth(MaxWidth)
                        .AddMinHeight(MinHeight)
                        .AddMaxHeight(MaxHeight);
                });
        }
    }
}
