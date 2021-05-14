using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MTooltip : BTooltip
    {
        [Parameter]
        public bool Top { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Attached { get; set; }

        public bool ActivatorFixed { get; set; }

        [Parameter]
        public int MaxWidth { get; set; }

        [Parameter]
        public int MinWidth { get; set; }

        public bool Unknown => !Bottom && !Left && !Top && !Right;

        protected double OffsetLeft { get; set; }

        protected double OffsetTop { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-tooltip";
            CssProvider
                .AsProvider<BTooltip>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--top", () => Top)
                        .AddIf($"{prefix}--right", () => Right)
                        .AddIf($"{prefix}--bottom", () => Bottom)
                        .AddIf($"{prefix}--left", () => Left)
                        .AddIf($"{prefix}--attached", () => Attached);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content")
                        .AddIf("menuable__content__active", () => IsActive)
                        .AddIf($"{prefix}__content--fixed", () => ActivatorFixed);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add(() => $"left:{OffsetLeft}px")
                        .Add(() => $"top:{OffsetTop}px")
                        .Add($"opacity:{(IsActive ? 0.9 : 0)}")
                        .Add("z-index:8")
                        .AddIf("display:none", () => Disabled);
                })
                .Apply("activator", styleAction: styleBuilder =>
                 {
                     styleBuilder
                         .Add("display:inline-block")
                         .Add(ActivatorStyle);
                 });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var activator = await JsInvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, Ref);
                var content = await JsInvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, ContentRef);

                if (Top || Bottom || Unknown)
                {
                    OffsetLeft = activator.Left + (activator.Width / 2) - (content.Width / 2);
                }
                else if (Left || Right)
                {
                    OffsetLeft = activator.Left + (Right ? activator.Width : -(activator.Width * 3 / 2)) + (Right ? 10 : -5);
                }

                if (Top || Bottom)
                {
                    OffsetTop = activator.Top + (Bottom ? activator.Height : -content.Height) + (Bottom ? 10 : -10);
                }
                else if (Left || Right)
                {
                    OffsetTop = activator.Top + (activator.Height / 2) - (content.Height / 2);
                }

                await JsInvokeAsync(JsInteropConstants.AddElementToBody, ContentRef);
                StateHasChanged();
            }
        }
    }
}
