using System;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    public partial class MMenu : BMenu
    {
        private double _clientX;
        private double _clientY;
        private double _minWidth;
        private BoundingClientRect _rect;

        [Parameter]
        public bool Dark { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var _rect = await JsInvokeAsync<BoundingClientRect>("MasaBlazor.getBoundingClientRect", Ref);

                System.Console.WriteLine(JsonSerializer.Serialize(_rect));

                _clientX = 0;
                _clientY = 0;

                if (OffsetY)
                {
                    _clientY += _rect.Height;
                }

                if (OffsetX)
                {
                    _clientY = _rect.Width;
                }

                if (!Absolute)
                {
                    _minWidth = _rect.Width;
                }
            }
        }

        protected override void SetComponentClass()
        {
            bool value = true;

            SlotProvider
                .Apply<BPopover, MPopover>(props =>
                {
                    props[nameof(MPopover.OriginalClass)] = "m-menu__content menuable__content__active";
                    props[nameof(MPopover.Visible)] = value;
                    props[nameof(MPopover.ClientX)] = (StringOrNumber)_clientX;
                    props[nameof(MPopover.ClientY)] = (StringOrNumber)_clientY;
                    props[nameof(MPopover.MinWidth)] = (StringOrNumber)_minWidth;
                    props[nameof(MPopover.ChildContent)] = ChildContent;
                })
                .Apply<BOverlay, MOverlay>(props =>
                {
                    props[nameof(MOverlay.Value)] = value;
                    //props[nameof(MOverlay.ValueChanged)] = EventCallback.Factory.Create<bool>(this, _value => value = _value);
                    props[nameof(MOverlay.Click)] = EventCallback.Factory.Create<MouseEventArgs>(this, () => { _visible = false; });
                    props[nameof(MOverlay.Opacity)] = (StringOrNumber)0;
                });
        }

        protected override void Click(MouseEventArgs args)
        {
            if (Disabled) return;

            if (Absolute)
            {
                Console.WriteLine(JsonSerializer.Serialize(args));
                _clientX = args.OffsetX;
                _clientY = args.OffsetY;
            }

            _visible = true;
        }

        protected override void MouseOver(MouseEventArgs args)
        {
            if (OpenOnHover && !_visible)
            {
                Console.WriteLine("over");

                Click(args);

                _visible = true;
            }
        }
    }
}
