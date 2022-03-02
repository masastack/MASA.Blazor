using BlazorComponent;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Element = BlazorComponent.Web.Element;

namespace Masa.Blazor
{
    public partial class MDialog : BDialog, IDialog, IThemeable
    {
        private bool _animated = false;

        protected override string AttachSelector => Attach ?? ".m-application";

        [Parameter]
        public string ContentClass { get; set; }

        [Parameter]
        public string Origin { get; set; } = "center center";

        [Parameter]
        public bool Scrollable { get; set; }

        [Parameter]
        public string Transition { get; set; } = "dialog-transition";

        public Dictionary<string, object> ContentAttrs
        {
            get
            {
                var attrs = new Dictionary<string, object>
                {
                    { "role", "document" }
                };
                if (IsActive)
                {
                    attrs.Add("tabindex", 0);
                }

                return attrs;
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
                        .AddIf($"{prefix}__content--active", () => IsActive)
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
                        .Add(ContentClass)
                        .AddIf($"{prefix}--active", () => IsActive)
                        .AddIf($"{prefix}--persistent", () => Persistent)
                        .AddIf($"{prefix}--fullscreen", () => Fullscreen)
                        .AddIf($"{prefix}--scrollable", () => Scrollable)
                        .AddIf($"{prefix}--animated", () => _animated);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add($"transform-origin: {Origin}")
                        .AddWidth(Width)
                        .AddMaxWidth(MaxWidth);
                });

            AbstractProvider
                .Apply<BOverlay, MOverlay>(attrs =>
                {
                    attrs[nameof(MOverlay.Value)] = ShowOverlay && IsActive;
                    attrs[nameof(MOverlay.ZIndex)] = ZIndex - 1;
                })
                .ApplyDialogDefault();
        }

        private async Task AfterShowContent()
        {
            await JsInvokeAsync(JsInteropConstants.AddOutsideClickEventListener,
                DotNetObjectReference.Create(new Invoker<object>(OutsideClick)),
                new[] { Document.GetElementByReference(DialogRef).Selector },
                new[] { Document.GetElementByReference(OverlayRef!.Value).Selector });
        }

        private async Task AnimateClick()
        {
            _animated = true;
            await InvokeStateHasChangedAsync();

            await Task.Delay(150);

            _animated = false;
            await InvokeStateHasChangedAsync();
        }

        public async Task Keydown(KeyboardEventArgs args)
        {
            if (args.Key == "Escape")
            {
                await CloseAsync();
            }
        }

        protected override async Task CloseAsync()
        {
            if (Persistent)
            {
                await AnimateClick();
                return;
            }

            await base.CloseAsync();
        }

        private bool CloseConditional()
        {
            return IsActive;
        }

        protected async Task OutsideClick(object _)
        {
            if (!CloseConditional()) return;

            if (OnOutsideClick.HasDelegate)
                await OnOutsideClick.InvokeAsync();

            if (Persistent)
            {
                await AnimateClick();
                return;
            }

            await SetIsActiveAsync(false);

            await InvokeStateHasChangedAsync();
        }

        protected override async Task ShowLazyContent()
        {
            if (!ShowContent && IsActive)
            {
                ShowContent = true;
                IsActive = false;

                await InvokeStateHasChangedAsync();
                await Task.Delay(BROWSER_RENDER_INTERVAL);

                await AfterShowContent();
                IsActive = true;

                await MoveContentTo();
                await InvokeStateHasChangedAsync();
            }
        }

        private async Task MoveContentTo()
        {
            await JsInvokeAsync(JsInteropConstants.AddElementTo, OverlayRef, AttachSelector);
            await JsInvokeAsync(JsInteropConstants.AddElementTo, ContentRef, AttachSelector);
        }
    }
}