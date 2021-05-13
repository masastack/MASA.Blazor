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

        private BoundingClientRect _activatorRect = new BoundingClientRect();
        private BoundingClientRect _contentRect = new BoundingClientRect();

        [Parameter]
        public bool Dark { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _activatorRect = await JsInvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, Ref);
                _contentRect = await JsInvokeAsync<BoundingClientRect>(JsInteropConstants.GetFirstChildBoundingClientRect, ContentRef);

                Console.WriteLine(JsonSerializer.Serialize(_activatorRect));
                await JsInvokeAsync(JsInteropConstants.AddElementToBody, ContentRef);
            }

            if (Absolute) return;

            _clientX = _activatorRect.Left;
            _clientY = _activatorRect.Top;

            if (Top)
            {
                _clientY -= _contentRect.Height - _activatorRect.Height;

                if (OffsetY) _clientY -= _activatorRect.Height;
            }
            else
            {
                if (OffsetY) _clientY += _activatorRect.Height;
            }

            if (Left)
            {
                if (OffsetX) _clientX -= _activatorRect.Width;
            }
            else
            {
                if (OffsetX) _clientX += _activatorRect.Width;
            }

            if (NudgeTop != null) _clientY -= NudgeTop.TryGetNumber().number;

            if (NudgeBottom != null) _clientY += NudgeBottom.TryGetNumber().number;

            if (NudgeLeft != null) _clientX -= NudgeLeft.TryGetNumber().number;

            if (NudgeRight != null) _clientX -= NudgeRight.TryGetNumber().number;

            _minWidth = _activatorRect.Width;
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

            SlotProvider
                .Apply<BPopover, MPopover>(props =>
                {
                    props[nameof(MPopover.Class)] = "m-menu__content menuable__content__active";
                    props[nameof(MPopover.Visible)] = _visible;
                    props[nameof(MPopover.ClientX)] = (StringOrNumber)_clientX;
                    props[nameof(MPopover.ClientY)] = (StringOrNumber)_clientY;
                    props[nameof(MPopover.MinWidth)] = (StringOrNumber)_minWidth;
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
                    props[nameof(MOverlay.Opacity)] = (StringOrNumber)0;
                });
        }

        protected override void Click(MouseEventArgs args)
        {
            if (Disabled) return;

            if (Absolute)
            {
                Console.WriteLine(JsonSerializer.Serialize(args));

                _clientX = _activatorRect.Left + args.OffsetX;
                _clientY = _activatorRect.Top + args.OffsetY;
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
