namespace Masa.Blazor
{
    public partial class MList : BList, IThemeable
    {
        /// <summary>
        /// Removes elevation (box-shadow) and adds a thin border.
        /// </summary>
        [Parameter]
        public bool Outlined { get; set; }

        /// <summary>
        /// Provides an alternative active style for v-list-item
        /// </summary>
        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public StringNumber Elevation { get; set; }

        /// <summary>
        /// Lowers max height of list tiles
        /// </summary>
        [Parameter]
        public bool Dense { get; set; }

        /// <summary>
        /// Disables all children v-list-item components
        /// </summary>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// Remove the highlighted background on active v-list-items
        /// </summary>
        [Parameter]
        public bool Flat { get; set; }

        /// <summary>
        /// An alternative styling that reduces v-list-item width and rounds the corners. Typically used with v-navigation-drawer
        /// </summary>
        [Parameter]
        public bool Nav { get; set; }

        /// <summary>
        /// Rounds the v-list-item edges
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
        public StringNumber Height { get; set; }

        /// <summary>
        /// Sets the maximum height for the component.
        /// </summary>
        [Parameter]
        public StringNumber MinHeight { get; set; }

        /// <summary>
        /// Sets the minimum width for the component.
        /// </summary>
        [Parameter]
        public StringNumber MinWidth { get; set; }

        /// <summary>
        /// Sets the maximum height for the component.
        /// </summary>
        [Parameter]
        public StringNumber MaxHeight { get; set; }

        /// <summary>
        /// Sets the maximum width for the component.
        /// </summary>
        [Parameter]
        public StringNumber MaxWidth { get; set; }

        /// <summary>
        /// Sets the width for the component.
        /// </summary>
        [Parameter]
        public StringNumber Width { get; set; }



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
                        .AddTheme(IsDark)
                        .Add("m-list")
                        .AddIf(() => $"elevation-{Elevation.Value}", () => Elevation != null)
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
