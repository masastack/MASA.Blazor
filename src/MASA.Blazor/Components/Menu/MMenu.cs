using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MMenu : BMenu
    {
        private double _clientX;
        private double _clientY;
        private double _minWidth;

        private double _absoluteOffsetX;
        private double _absoluteOffsetY;

        private HtmlElement _activatorRect = new();
        private HtmlElement _contentRect = new();

        [Inject]
        public DomEventJsInterop DomEventJsInterop { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInvokeAsync(JsInteropConstants.AddElementTo, ContentRef, ".m-application");

                DomEventJsInterop.AddEventListener<Window>("window", "resize", OnResize, false);
            }

            if (_activatorRect.OffsetWidth == 0)
                _activatorRect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, Ref);

            _contentRect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetFirstChildDomInfo, ContentRef, ".m-application");

            System.Console.WriteLine("activator:{0}", System.Text.Json.JsonSerializer.Serialize(_activatorRect));
            System.Console.WriteLine("content:{0}", System.Text.Json.JsonSerializer.Serialize(_contentRect));
        }

        private async void OnResize(Window window)
        {
            Console.WriteLine("  resize:{0}", System.Text.Json.JsonSerializer.Serialize(window));

            _activatorRect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, Ref);
            System.Console.WriteLine("  resized activator:{0}", System.Text.Json.JsonSerializer.Serialize(_activatorRect));

            //_contentRect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetFirstChildDomInfo, ContentRef, ".m-application");
            //System.Console.WriteLine("  resized content:{0}", System.Text.Json.JsonSerializer.Serialize(_contentRect));

            if (Absolute)
            {
                _clientX = _activatorRect.AbsoluteLeft + _absoluteOffsetX;
                _clientY = _activatorRect.AbsoluteTop + _absoluteOffsetY;
            }
            else
            {
                _clientX = _activatorRect.AbsoluteLeft;
                _clientY = _activatorRect.AbsoluteTop;

                if (MinWidth == default)
                {
                    _minWidth = _activatorRect.OffsetWidth;
                    if (NudgeWidth != null) _minWidth += NudgeWidth.TryGetNumber().number;
                }

                if (Top)
                {
                    _clientY -= _contentRect.OffsetHeight - _activatorRect.OffsetHeight;

                    if (OffsetY) _clientY -= _activatorRect.OffsetHeight;
                }
                else
                {
                    if (OffsetY) _clientY += _activatorRect.OffsetHeight;
                }

                if (Left)
                {
                    if (OffsetX)
                        _clientX -= _contentRect.OffsetWidth;
                    else
                        _clientX -= _contentRect.OffsetWidth - _activatorRect.OffsetWidth;
                }
                else
                {
                    if (OffsetX) _clientX += _activatorRect.OffsetWidth;
                }
            }

            if (NudgeTop != null) _clientY -= NudgeTop.TryGetNumber().number;
            if (NudgeBottom != null) _clientY += NudgeBottom.TryGetNumber().number;
            if (NudgeLeft != null) _clientX -= NudgeLeft.TryGetNumber().number;
            if (NudgeRight != null) _clientX -= NudgeRight.TryGetNumber().number;

            StateHasChanged();
        }
        protected override void SetComponentClass()
        {
            CssProvider
                .AsProvider<BMenu>()
                .Apply(styleAction: styleBuilder => styleBuilder
                        .Add("display:inline-block")
                        .Add(ActivatorStyle));

            AbstractProvider
                .Apply<BPopover, MPopover>(props =>
                {
                    props[nameof(MPopover.Class)] = "m-menu__content menuable__content__active";
                    props[nameof(MPopover.Visible)] = _visible;
                    props[nameof(MPopover.ClientX)] = (StringNumber)_clientX;
                    props[nameof(MPopover.ClientY)] = (StringNumber)_clientY;
                    props[nameof(MPopover.MinWidth)] = MinWidth ?? _minWidth;
                    props[nameof(MPopover.ChildContent)] = ChildContent;
                    props[nameof(MPopover.Click)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                    {
                        if (CloseOnContentClick)
                        {
                            if (VisibleChanged.HasDelegate)
                                await VisibleChanged.InvokeAsync(false);
                            else
                                _visible = false;
                        }
                    });
                })
                .Apply<BOverlay, MOverlay>(props =>
                {
                    props[nameof(MOverlay.Value)] = (Visible && !OpenOnHover);
                    props[nameof(MOverlay.Click)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                    {
                        if (CloseOnClick)
                        {
                            if (VisibleChanged.HasDelegate)
                                await VisibleChanged.InvokeAsync(false);
                            else
                                _visible = false;
                        }

                        if (OutsideClick.HasDelegate)
                            await OutsideClick.InvokeAsync();
                    });
                    props[nameof(MOverlay.Opacity)] = (StringNumber)0;
                });
        }

        protected override async Task Click(MouseEventArgs args)
        {
            if (Disabled) return;

            if (Absolute)
            {
                _absoluteOffsetX = args.OffsetX;
                _absoluteOffsetY = args.OffsetY;

                _clientX = _activatorRect.AbsoluteLeft + args.OffsetX;
                _clientY = _activatorRect.AbsoluteTop + args.OffsetY;
            }
            else
            {
                _clientX = _activatorRect.AbsoluteLeft;
                _clientY = _activatorRect.AbsoluteTop;

                if (MinWidth == default)
                {
                    _minWidth = _activatorRect.OffsetWidth;
                    if (NudgeWidth != null) _minWidth += NudgeWidth.TryGetNumber().number;
                }

                if (Top)
                {
                    _clientY -= _contentRect.OffsetHeight - _activatorRect.OffsetHeight;

                    if (OffsetY) _clientY -= _activatorRect.OffsetHeight;
                }
                else
                {
                    if (OffsetY) _clientY += _activatorRect.OffsetHeight;
                }

                if (Left)
                {
                    if (OffsetX)
                        _clientX -= _contentRect.OffsetWidth;
                    else
                        _clientX -= _contentRect.OffsetWidth - _activatorRect.OffsetWidth;
                }
                else
                {
                    if (OffsetX) _clientX += _activatorRect.OffsetWidth;
                }
            }

            if (NudgeTop != null) _clientY -= NudgeTop.TryGetNumber().number;
            if (NudgeBottom != null) _clientY += NudgeBottom.TryGetNumber().number;
            if (NudgeLeft != null) _clientX -= NudgeLeft.TryGetNumber().number;
            if (NudgeRight != null) _clientX -= NudgeRight.TryGetNumber().number;

            if (VisibleChanged.HasDelegate)
                await VisibleChanged.InvokeAsync(true);
            else
                _visible = true;
        }

        protected override async Task MouseEnter(MouseEventArgs args)
        {
            if (OpenOnHover && !Visible)
            {
                await Click(args);
            }
            else
            {
                PreventRender();
            }
        }
    }
}
