using BlazorComponent;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Element = BlazorComponent.Web.Element;

namespace MASA.Blazor
{
    public partial class MDialog : BDialog, IDialog, IThemeable
    {
        private bool _animated = false;
        private int _stackMinZIndex = 200;
        private bool _isNotFirstRender = true;

        public ElementReference ContentRef { get; set; }

        public ElementReference DialogRef { get; set; }

        [Parameter]
        public bool Attach { get; set; }

        [Parameter]
        public bool Value { get; set; }

        [Obsolete("Use Value instead.")]
        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public string Transition { get; set; } = "dialog-transition";

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment<ActivatorProps> ActivatorContent { get; set; }//TODO:ActivatorContent

        protected int ZIndex { get; set; }

        public Guid ActivatorId { get; set; }

        public Dictionary<string, object> ActivatorAttrs { get; set; }

        public Dictionary<string, object> ContentAttrs
        {
            get
            {
                var attrs = new Dictionary<string, object>
                {
                    { "role", "document" }
                };
                if (Value)
                {
                    attrs.Add("tabindex", 0);
                }

                return attrs;
            }
        }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        [Parameter]
        public bool Persistent { get; set; }

        [Parameter]
        public bool Scrollable { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnOutsideClick { get; set; }

        [Parameter]
        public bool Fullscreen { get; set; }//TODO:watch fullscreen =>(hideScroll or showScroll) and (removeOverlay or genOverlay)

        public bool Dark { get; set; }

        public bool Light { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return Themeable != null && Themeable.IsDark;
            }
        }

        [Inject]
        public Document Document { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!_isNotFirstRender)
            {
                if (ZIndex == default)
                {
                    ZIndex = await ActiveZIndex();
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _isNotFirstRender = false;
                await JsInvokeAsync(JsInteropConstants.AddElementTo, Ref, ".m-application");
            }
        }

        protected override void SetComponentClass()
        {
            var prefix = "m-dialog";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__container")
                        .AddIf($"{prefix}__container--attached", () => Attach);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content")
                        .AddIf($"{prefix}__content--active", () => Value)
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add($"z-index: {ZIndex}");
                })
                .Apply("innerContent", cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--active", () => Value)
                        .AddIf($"{prefix}--persistent", () => Persistent)
                        .AddIf($"{prefix}--fullscreen", () => Fullscreen)
                        .AddIf($"{prefix}--scrollable", () => Scrollable)
                        .AddIf($"{prefix}--animated", () => _animated);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add("transform-origin: center center")
                        .AddWidth(Width)
                        .AddMaxWidth(MaxWidth);
                });

            AbstractProvider
                .Apply<BOverlay, MOverlay>(props =>
                {
                    props[nameof(MOverlay.Value)] = Value;
                    props[nameof(MOverlay.ZIndex)] = ZIndex - 1;
                    props[nameof(MOverlay.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                    {
                        if (Persistent)
                        {
                            _animated = true;
                            await Task.Delay(150);
                            _animated = false;
                        }
                        else
                        {
                            if (ValueChanged.HasDelegate)
                                await ValueChanged.InvokeAsync(false);
                        }

                        if (OnOutsideClick.HasDelegate)
                            await OnOutsideClick.InvokeAsync();
                    });
                })
                .ApplyDialogDefault();
        }

        private async Task<int> ActiveZIndex()
        {
            int zIndex;
            if (!Value)
            {
                zIndex = await JsInvokeAsync<int>(JsInteropConstants.GetZIndex, ContentRef);
            }
            else
            {
                zIndex = await GetMaxZIndex() + 2;
            }

            return zIndex;
        }

        private async Task<int> GetMaxZIndex()
        {
            var maxZindex = await JsInvokeAsync<int>(JsInteropConstants.GetMenuOrDialogMaxZIndex, new List<ElementReference> { ContentRef }, Ref);

            return maxZindex > _stackMinZIndex ? maxZindex : _stackMinZIndex;
        }
    }
}