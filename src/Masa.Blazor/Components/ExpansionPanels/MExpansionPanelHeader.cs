namespace Masa.Blazor
{
    public partial class MExpansionPanelHeader : BExpansionPanelHeader
    {
        [Parameter]
        public bool Ripple { get; set; }

        protected override void OnParametersSet()
        {
            Attributes["ripple"] = Ripple;

            base.OnParametersSet();
        }

        protected override void SetComponentClass()
        {
            ExpandIcon ??= "mdi-chevron-down";

            CssProvider
                .Apply(css =>
                {
                    css.Add("m-expansion-panel-header")
                        .AddIf("m-expansion-panel-header--active", () => IsActive)
                        .AddIf("m-expansion-panel-header--mousedown", () => HasMouseDown)
                        .AddIf("m-expansion-panel--disabled m-btn--disabled", () => IsDisabled)
                        .AddBackgroundColor(Color);
                }, style => style.AddBackgroundColor(Color))
                .Apply("headerIcon", css =>
                {
                    css.Add("m-expansion-panel-header__icon")
                        .AddIf("m-expansion-panel-header__icon--disable-rotate", () => DisableIconRotate);
                });

            AbstractProvider
                .Apply<BIcon, MIcon>();
        }
    }
}