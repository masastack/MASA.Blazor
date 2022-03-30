namespace Masa.Blazor
{
    public class MMenu : BMenu
    {
        protected override string AttachSelector => Attach ?? ".m-application";

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Transition ??= "m-menu-transition";
            Origin ??= "top left";
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-menu");
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder.Add("m-menu__content")
                        .AddIf("m-menu__content--auto", () => Auto)
                        .AddIf("m-menu__content--fixed", () => ActivatorFixed)
                        .AddIf("menuable__content__active", () => IsActive)
                        .AddRounded(Tile ? "0" : Rounded)
                        .Add(ContentClass)
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf($"max-height:{CalculatedMaxHeight}", () => CalculatedMaxHeight != null)
                        .AddIf($"min-width:{CalculatedMinWidth}", () => CalculatedMinWidth != null)
                        .AddIf($"max-width:{CalculatedMaxWidth}", () => CalculatedMaxWidth != null)
                        .AddIf($"top:{CalculatedTop.ToUnit()}", () => CalculatedTop != null)
                        .AddIf($"left:{CalculatedLeft.ToUnit()}", () => CalculatedLeft != null)
                        .Add($"transform-origin:{Origin}")
                        .Add($"z-index:{ComputedZIndex}")
                        .Add(ContentStyle);
                });
        }
    }
}