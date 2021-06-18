using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MDialog : BDialog
    {
        private bool _animated = false;

        protected override async Task OnFirstAfterRenderAsync()
        {
            await JsInvokeAsync(JsInteropConstants.AddElementTo, Ref, ".m-application");

            await base.OnFirstAfterRenderAsync();
        }

        protected override void SetComponentClass()
        {
            var prefix = "m-dialog";

            CssProvider
                .AsProvider<BDialog>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content")
                        .AddIf($"{prefix}__content--active", () => Visible);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add("z-index: 202");
                })
                .Apply("body", cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--active", () => Visible)
                        .AddIf($"{prefix}--persistent", () => Persistent)
                        .AddIf($"{prefix}--scrollable", () => Scrollable)
                        .AddIf($"{prefix}--animated", () => _animated);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add("transform-origin: center center")
                        .AddWidth(Width)
                        .AddMaxWidth(MaxWidth)
                        .AddIf("display: none", () => !Visible);
                });

            AbstractProvider
                .Apply<BPopover, MPopover>(props =>
                {
                    props[nameof(MPopover.Visible)] = Visible;
                })
                .Apply<BOverlay, MOverlay>(props =>
                {
                    props[nameof(MOverlay.Value)] = Visible;
                    props[nameof(MOverlay.Click)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                    {
                        if (Persistent)
                        {
                            _animated = true;
                            await Task.Delay(100);
                            _animated = false;
                        }
                        else
                        {
                            if (VisibleChanged.HasDelegate)
                                await VisibleChanged.InvokeAsync(false);
                        }

                        if (OutsideClick.HasDelegate)
                            await OutsideClick.InvokeAsync();
                    });
                });
        }
    }
}