using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;
using Element = BlazorComponent.Web.Element;

namespace MASA.Blazor
{
    public partial class MMenu : BMenu
    {
        protected double _clientX;
        protected double _clientY;
        private double _immutableClientY; // Records clientY after OptimizPosition
        private double _minWidth;

        private double _absoluteOffsetX;
        private double _absoluteOffsetY;

        private Window _window = new();
        private Element _bodyRect = new();
        private Element _activatorRect = new();
        private Element _contentRect = new();

        [Inject]
        public DomEventJsInterop DomEventJsInterop { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public ElementReference ActivatorRef { get; set; }

        //TODO:menu将会改进
        public ElementReference ComputedRef => Input ? ActivatorRef : Ref;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInvokeAsync(JsInteropConstants.AddElementTo, ContentRef, ".m-application");

                _window = await JsInvokeAsync<Window>(JsInteropConstants.GetWindow);
                DomEventJsInterop.AddEventListener<Window>("window", "resize", OnResize, false);

                DomEventJsInterop.ResizeObserver<Dimensions[]>(ComputedRef, ObserveSizeChange);

                await OptimizPosition();
            }
            
            await base.OnAfterRenderAsync(firstRender);
        }

        private void ObserveSizeChange(Dimensions[] entries)
        {
            if (entries.Length > 0)
            {
                var dimension = entries[0];
                _clientY = _immutableClientY - _activatorRect.ClientHeight + dimension.Height;
            }
        }

        private async void OnResize(Window window)
        {
            await Task.Run(() => OptimizPosition(window));
        }

        private async Task OptimizPosition(Window window = null)
        {
            _window = window ?? _window;

            _bodyRect = await JsInvokeAsync<Element>(JsInteropConstants.GetDomInfo, "document");

            _activatorRect = await JsInvokeAsync<Element>(JsInteropConstants.GetDomInfo, ComputedRef);

            _contentRect = await JsInvokeAsync<Element>(JsInteropConstants.GetFirstChildDomInfo, ContentRef, ".m-application");

            if (Fixed)
            {
                _clientX = _activatorRect.RelativeLeft;
                _clientY = _activatorRect.RelativeTop;
            }
            else
            {
                _clientX = _activatorRect.AbsoluteLeft;
                _clientY = _activatorRect.AbsoluteTop;
            }

            if (Absolute)
            {
                _clientX += _absoluteOffsetX;
                _clientY += _absoluteOffsetY;
            }
            else
            {
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

            // beyond the edge of browser 
            if (_clientX + _minWidth > _window.InnerWidth)
            {
                _clientX = _window.InnerWidth - _minWidth - 17;
            }

            if (_clientY + _contentRect.ClientHeight > _bodyRect.ScrollTop + _bodyRect.ClientHeight)
            {
                _clientY = _bodyRect.ScrollTop + _bodyRect.ClientHeight - _contentRect.ClientHeight;
            }

            _immutableClientY = _clientY;

            await InvokeStateHasChangedAsync();
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(styleAction: styleBuilder => styleBuilder
                        .AddFirstIf(
                            ("display:block", () => Block),
                            ("display:inline-block", () => true)
                        )
                        .Add(ActivatorStyle));

            AbstractProvider
                .Apply<BPopover, MPopover>(props =>
                {
                    var css = "m-menu__content menuable__content__active";
                    if (Fixed)
                        css += " m-menu__content--fixed";

                    props[nameof(MPopover.Class)] = css;
                    props[nameof(MPopover.Visible)] = _visible;
                    props[nameof(MPopover.ClientX)] = (StringNumber)_clientX;
                    props[nameof(MPopover.ClientY)] = (StringNumber)_clientY;
                    props[nameof(MPopover.MinWidth)] = MinWidth ?? _minWidth;
                    props[nameof(MPopover.MaxHeight)] = MaxHeight;
                    props[nameof(MPopover.ChildContent)] = ChildContent;
                    props[nameof(MPopover.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                    {
                        if (CloseOnContentClick)
                        {
                            if (VisibleChanged.HasDelegate)
                                await VisibleChanged.InvokeAsync(false);
                            else
                                _visible = false;
                        }
                    });
                    props[nameof(MPopover.PreventDefault)] = Input;
                })
                .Apply<BOverlay, MOverlay>(props =>
                {
                    props[nameof(MOverlay.Value)] = (Visible && !OpenOnHover);
                    props[nameof(MOverlay.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                    {
                        if (CloseOnClick)
                        {
                            if (VisibleChanged.HasDelegate)
                                await VisibleChanged.InvokeAsync(false);
                            else
                                _visible = false;
                        }

                        if (OnOutsideClick.HasDelegate)
                            await OnOutsideClick.InvokeAsync();
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
            }

            await OptimizPosition();

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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            DomEventJsInterop.RemoveEventListerner<Window>("window", "resize", OnResize);
        }
    }
}
