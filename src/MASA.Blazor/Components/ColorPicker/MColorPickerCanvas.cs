using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    public partial class MColorPickerCanvas : BColorPickerCanvas, IColorPickerCanvas
    {
        private BoundingClientRect _boundingClientRect;
        private bool _updateCanvas;
        private ColorPickerColor _color = ColorUtils.FromRGBA(new RGBA { R = 255, G = 0, B = 0, A = 0 });

        public Dictionary<string, object> CanvasAttrs => new()
        {
            { "width", Width },
            { "height", Height }
        };

        [Parameter]
        public ColorPickerColor Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (_color.Hue!=value.Hue)
                {
                    _updateCanvas = true;
                }
                _color = value;
            }
        }

        public ElementReference CanvasRef { get; set; }

        [Parameter]
        public StringNumber Width { get; set; } = 300;

        [Parameter]
        public StringNumber Height { get; set; } = 150;

        [Parameter]
        public StringNumber DotSize { get; set; } = 10;

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public EventCallback<ColorPickerColor> OnColorUpdate { get; set; }

        [CascadingParameter]
        public MColorPicker ColorPicker { get; set; }

        [Inject]
        public Document Document { get; set; }

        [Inject]
        public DomEventJsInterop DomEventJsInterop { get; set; }

        public (double X, double Y) Dot
        {
            get
            {
                if (Color == null)
                    return (0, 0);

                double x = Color.Hsva.S * Convert.ToInt32(Width.ToDouble().ToString(), 10);
                double y = (1 - Color.Hsva.V) * Convert.ToInt32(Height.ToDouble().ToString(), 10);

                return (x, y);
            }
        }

        protected override void SetComponentClass()
        {
            var prefix = "m-color-picker__canvas";
            CssProvider
                .Apply("canvas", cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add(() => $"width:{Width.ToUnit()}")
                        .Add(() => $"height:{Height.ToUnit()}");
                })
                .Apply("dot", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}-dot")
                        .AddIf($"{prefix}-dot--disabled", () => Disabled);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add($"width:{DotSize.ToUnit()}")
                        .Add($"height:{DotSize.ToUnit()}")
                        .Add($"transform:translate({Dot.X - (DotSize.ToDouble() / 2)}px,{Dot.Y - (DotSize.ToDouble() / 2)}px)");
                });

            AbstractProvider
                .ApplyColorPickerCanvasDefault();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (Color != null)
                {
                    await JsInvokeAsync(JsInteropConstants.UpdateCanvas, CanvasRef, Color.Hue);
                }
            }

            if (_updateCanvas)
            {
                await JsInvokeAsync(JsInteropConstants.UpdateCanvas, CanvasRef, Color.Hue);
            }
        }

        public override async Task HandleOnClickAsync(MouseEventArgs args)
        {
            if (!Disabled)
            {
                var el = Document.QuerySelector(Ref);
                _boundingClientRect = await el.GetBoundingClientRectAsync();

                await EmitColor(args.ClientX, args.ClientY);
            }
        }

        public async Task HandleMouseMoveAsync(MouseEventArgs args)
        {
            if (!Disabled)
            {
                await EmitColor(args.ClientX, args.ClientY);
            }
        }

        public async Task HandleMouseUp(MouseEventArgs args)
        {
            var window = Document.QuerySelector("window");

            await window.RemoveEventListenerAsync("mousemove");
            await window.RemoveEventListenerAsync("mouseup");
        }

        public override async Task HandleOnMouseDownAsync(MouseEventArgs args)
        {
            if (!Disabled)
            {
                var el = Document.QuerySelector(Ref);
                _boundingClientRect = await el.GetBoundingClientRectAsync();

                var window = Document.QuerySelector("window");
                await window.AddEventListenerAsync("mousemove", CreateEventCallback<MouseEventArgs>(HandleMouseMoveAsync), false);
                await window.AddEventListenerAsync("mouseup", CreateEventCallback<MouseEventArgs>(HandleMouseUp), false);
            }
        }

        private async Task EmitColor(double x, double y)
        {
            var hsva = ColorUtils.FromHSVA(new HSVA
            {
                H = Color.Hue,
                S = Clamp(x - _boundingClientRect.Left, 0, _boundingClientRect.Width) / _boundingClientRect.Width,
                V = 1 - Clamp(y - _boundingClientRect.Top, 0, _boundingClientRect.Height) / _boundingClientRect.Height,
                A = Color.Alpha
            });

            if (OnColorUpdate.HasDelegate)
            {
                if (Color.Hue != hsva.Hue)
                {
                    await JsInvokeAsync(JsInteropConstants.UpdateCanvas, CanvasRef, Color.Hue);
                }

                await OnColorUpdate.InvokeAsync(hsva);
            }
        }

        public static double Clamp(double value, double min = 0, double max = 1)
        {
            return Math.Max(min, Math.Min(max, value));
        }
    }
}
