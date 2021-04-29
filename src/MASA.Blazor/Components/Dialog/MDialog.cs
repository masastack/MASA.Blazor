using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Xml.Schema;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    public partial class MDialog : BDialog
    {
        private bool _animated = false;

        protected override void SetComponentClass()
        {
            var prefix = "m-dialog";

            CssBuilder
                .Add($"{prefix}__content")
                .AddIf($"{prefix}__content--active", () => Visible);
            StyleBuilder
                .Add("z-index: 202");

            BodyCssBuilder
                .Add(prefix)
                .AddIf($"{prefix}--active", () => Visible)
                .AddIf($"{prefix}--persistent", () => Persistent)
                .AddIf($"{prefix}--scrollable", () => Scrollable)
                .AddIf($"{prefix}--animated", () => _animated);
            BodyStyleBuilder
                .Add("transform-origin: center center")
                .AddIf(() => $"width: {Width.TryGetNumber().number}px", () => Width != null)
                .AddIf(() => $"max-width: {MaxWidth.TryGetNumber().number}px", () => MaxWidth != null)
                .AddIf("display: none", () => !Visible);

            SlotProvider
                .Apply<BPopover, MPopover>(props => { props[nameof(MPopover.Visible)] = Visible; })
                .Apply<BOverlay, MOverlay>(props =>
                {
                    props[nameof(MOverlay.Value)] = Visible;
                    if (Persistent)
                    {
                        props[nameof(MOverlay.Click)] =
                            EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                            {
                                _animated = true;
                                await Task.Delay(100);
                                _animated = false;
                                await InvokeStateHasChangedAsync();
                            });
                    }
                    else
                    {
                        props[nameof(MOverlay.Click)] =
                            EventCallback.Factory.Create<MouseEventArgs>(this, () => { Visible = false; });
                    }
                });
        }
    }
}