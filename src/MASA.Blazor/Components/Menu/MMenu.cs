using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MMenu : BMenu
    {
        private double _clientX;
        private double _clientY;
        private double _minWidth;

        private HtmlElement _activatorRect = new HtmlElement();
        private HtmlElement _contentRect = new HtmlElement();

        [Parameter]
        public bool Dark { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await JsInvokeAsync(JsInteropConstants.AddElementToBody, ContentRef);

            if (_activatorRect.ClientWidth == 0)
                _activatorRect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, Ref);

            if (_contentRect.ClientWidth == 0)
                _contentRect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, ContentRef);

            if (Absolute) return;

            _clientX = _activatorRect.AbsoluteLeft;
            _clientY = _activatorRect.AbsoluteTop;

            if (Top)
            {
                _clientY -= _contentRect.ClientHeight - _activatorRect.ClientHeight;

                if (OffsetY) _clientY -= _activatorRect.ClientHeight;
            }
            else
            {
                if (OffsetY) _clientY += _activatorRect.ClientHeight;
            }

            if (Left)
            {
                if (OffsetX) _clientX -= _activatorRect.ClientWidth;
            }
            else
            {
                if (OffsetX) _clientX += _activatorRect.ClientWidth;
            }

            if (NudgeTop != null) _clientY -= NudgeTop.TryGetNumber().number;

            if (NudgeBottom != null) _clientY += NudgeBottom.TryGetNumber().number;

            if (NudgeLeft != null) _clientX -= NudgeLeft.TryGetNumber().number;

            if (NudgeRight != null) _clientX -= NudgeRight.TryGetNumber().number;

            _minWidth = _activatorRect.ClientWidth;
            if (NudgeWidth != null) _minWidth += NudgeWidth.TryGetNumber().number;
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .AsProvider<BMenu>()
                .Apply(styleAction: styleBuilder =>
                {
                    styleBuilder
                        .Add("position:relative; display:inline-block");
                });

            AbstractProvider
                .Apply<BPopover, MPopover>(props =>
                {
                    props[nameof(MPopover.Class)] = "m-menu__content menuable__content__active";
                    props[nameof(MPopover.Visible)] = _visible;
                    props[nameof(MPopover.ClientX)] = (StringNumber)_clientX;
                    props[nameof(MPopover.ClientY)] = (StringNumber)_clientY;
                    props[nameof(MPopover.MinWidth)] = (StringNumber)_minWidth;
                    props[nameof(MPopover.ChildContent)] = ChildContent;
                    props[nameof(MPopover.Click)] = EventCallback.Factory.Create<MouseEventArgs>(this, () =>
                    {
                        if (CloseOnContentClick)
                        {
                            _visible = false;
                        }
                    });
                })
                .Apply<BOverlay, MOverlay>(props =>
                {
                    props[nameof(MOverlay.Value)] = (_visible && !OpenOnHover);
                    props[nameof(MOverlay.Click)] = EventCallback.Factory.Create<MouseEventArgs>(this, () =>
                    {
                        if (CloseOnClick)
                        {
                            _visible = false;
                        }
                    });
                    props[nameof(MOverlay.Opacity)] = (StringNumber)0;
                });
        }

        protected override void Click(MouseEventArgs args)
        {
            if (Disabled) return;

            if (Absolute)
            {
                Console.WriteLine(JsonSerializer.Serialize(args));

                _clientX = _activatorRect.AbsoluteLeft + args.OffsetX;
                _clientY = _activatorRect.AbsoluteTop + args.OffsetY;
            }

            _visible = true;
        }

        protected override void MouseOver(MouseEventArgs args)
        {
            if (OpenOnHover && !_visible)
            {
                Click(args);

                _visible = true;
            }
        }
    }
}
