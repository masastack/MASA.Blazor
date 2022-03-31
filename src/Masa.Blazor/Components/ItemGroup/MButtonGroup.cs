namespace Masa.Blazor
{
    public partial class MButtonGroup : MItemGroup
    {
        public MButtonGroup() : base(GroupType.ButtonGroup)
        {
        }

        [Parameter]
        public bool Borderless { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool Group { get; set; }

        [Parameter]
        public bool Rounded { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public string BackgroundColor { get; set; }

        [Parameter]
        public string Color { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-btn-toggle")
                        .AddIf("m-btn-toggle--borderless", () => Borderless)
                        .AddIf("m-btn-toggle--dense", () => Dense)
                        .AddIf("m-btn-toggle--group", () => Group)
                        .AddIf("m-btn-toggle--rounded", () => Rounded)
                        .AddIf("m-btn-toggle--shaped", () => Shaped)
                        .AddIf("m-btn-toggle--tile", () => Tile)
                        .AddTextColor(Color)
                        .AddBackgroundColor(BackgroundColor, () => !Group);
                });
        }
    }
}
