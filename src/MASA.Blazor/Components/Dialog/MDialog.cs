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

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Fullscreen { get; set; } //TODO:watch fullscreen =>(hideScroll or showScroll) and (removeOverlay or genOverlay)

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public bool Scrollable { get; set; }

        [Parameter]
        public string Transition { get; set; } = "dialog-transition";

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

        protected override void SetComponentClass()
        {
            var prefix = "m-dialog";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__container")
                        .AddIf($"{prefix}__container--attached", () => Attach != null);
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
                            await UpdateValue(false);
                        }

                        if (OnOutsideClick.HasDelegate)
                            await OnOutsideClick.InvokeAsync();
                    });
                })
                .ApplyDialogDefault();
        }

        public Dictionary<string, object> ContentAttrs
        {
            get
            {
                var attrs = new Dictionary<string, object>
                {
                    {"role", "document"}
                };
                if (Value)
                {
                    attrs.Add("tabindex", 0);
                }

                return attrs;
            }
        }
    }
}