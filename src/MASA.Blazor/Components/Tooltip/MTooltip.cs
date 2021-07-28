using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MTooltip : BTooltip
    {
        private HtmlElement _activatorRect = new();
        private HtmlElement _contentRect = new();

        public bool Top => !(Bottom || Left || Right);

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Attached { get; set; }

        [Parameter]
        public int MaxWidth { get; set; }

        [Parameter]
        public int MinWidth { get; set; }

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
                        .AddIf($"{prefix}__content--fixed", () => Fixed);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add(() => $"left:{OffsetLeft}px")
                        .Add(() => $"top:{OffsetTop}px")
                        .Add($"opacity:{(IsActive ? 0.9 : 0)}")
                        .Add("z-index:1100")
                        .AddIf("display:none", () => !IsActive || Disabled);
                })
                .Apply("activator", styleAction: styleBuilder =>
                 {
                     styleBuilder
                         .Add("display:inline-block; width:initial;")
                         .Add(ActivatorStyle);
                 });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInvokeAsync(JsInteropConstants.AddElementTo, ContentRef, ".m-application");
            }

            if (_activatorRect.ClientWidth == 0)
                _activatorRect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, Ref);

            if (_contentRect.ClientWidth == 0)
                _contentRect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, ContentRef);

            if (Top || Bottom)
            {
                OffsetLeft = (Fixed ? _activatorRect.RelativeLeft : _activatorRect.AbsoluteLeft) +
                    (_activatorRect.ClientWidth / 2) -
                    (_contentRect.ClientWidth / 2);
            }
            else if (Left || Right)
            {
                OffsetLeft = (Fixed ? _activatorRect.RelativeLeft : _activatorRect.AbsoluteLeft) +
                    (Right ? _activatorRect.ClientWidth : -_contentRect.ClientWidth) +
                    (Right ? 10 : -5);
            }

            if (Top || Bottom)
            {
                OffsetTop = (Fixed ? _activatorRect.RelativeTop : _activatorRect.AbsoluteTop) +
                    (Bottom ? _activatorRect.ClientHeight : -_contentRect.ClientHeight) +
                    (Bottom ? 10 : -10);
            }
            else if (Left || Right)
            {
                OffsetTop = (Fixed ? _activatorRect.RelativeTop : _activatorRect.AbsoluteTop) +
                    (_activatorRect.ClientHeight / 2) -
                    (_contentRect.ClientHeight / 2);
            }
        }

        protected override async Task OnMouseEnter()
        {
            await base.OnMouseEnter();
            _activatorRect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, Ref);
        }
    }
}
