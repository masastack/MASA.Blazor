namespace Masa.Blazor
{
    public partial class MTooltip : BTooltip, ITooltip
    {
        [Parameter]
        public string? ContentStyle { get; set; }

        protected override string DefaultAttachSelector => Permanent ? ".m-application__permanent" : ".m-application";

        ElementReference ITooltip.ContentElement
        {
            set => ContentElement = value;
        }

        bool IMenuable.IsBooted => IsBooted;

        protected override void SetComponentClass()
        {
            var prefix = "m-tooltip";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--top", () => Top)
                        .AddIf($"{prefix}--right", () => Right)
                        .AddIf($"{prefix}--bottom", () => Bottom)
                        .AddIf($"{prefix}--left", () => Left)
                        .AddIf($"{prefix}--attached", () => Attach is not { AsT1: true });
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content")
                        .AddIf("menuable__content__active", () => IsActive)
                        .AddIf($"{prefix}__content--fixed", () => ActivatorFixed)
                        .Add(ContentClass)
                        .AddBackgroundColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add(ContentStyle)
                        .AddMaxWidth(MaxWidth)
                        .AddMinWidth(MinWidth)
                        .Add($"left:{CalculatedLeft}px")
                        .Add($"top:{CalculatedTop}px")
                        .Add($"opacity:{(IsActive ? 0.9 : 0)}")
                        .Add($"z-index:{ComputedZIndex}")
                        .AddBackgroundColor(Color);
                });

            AbstractProvider
                .ApplyTooltipDefault();
        }
    }
}