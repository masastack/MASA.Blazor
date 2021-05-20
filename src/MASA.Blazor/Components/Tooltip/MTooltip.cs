using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MTooltip : BTooltip
    {
        private BoundingClientRect _activatorRect = new();
        private BoundingClientRect _contentRect = new();

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
                        .Add("z-index:1100")
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
                await JsInvokeAsync(JsInteropConstants.AddElementToBody, ContentRef);

            if (_activatorRect.Width == 0)
                _activatorRect = await JsInvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, Ref);

            if (_contentRect.Width == 0)
                _contentRect = await JsInvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, ContentRef);

            if (Top || Bottom || Unknown)
            {
                OffsetLeft = _activatorRect.Left + (_activatorRect.Width / 2) - (_contentRect.Width / 2);
            }
            else if (Left || Right)
            {
                OffsetLeft = _activatorRect.Left + (Right ? _activatorRect.Width : -(_activatorRect.Width * 3 / 2)) + (Right ? 10 : -5);
            }

            if (Top || Bottom)
            {
                OffsetTop = _activatorRect.Top + (Bottom ? _activatorRect.Height : -_contentRect.Height) + (Bottom ? 10 : -10);
            }
            else if (Left || Right)
            {
                OffsetTop = _activatorRect.Top + (_activatorRect.Height / 2) - (_contentRect.Height / 2);
            }
        }
    }
}
