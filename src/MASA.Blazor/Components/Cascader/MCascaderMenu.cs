using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MCascaderMenu : MMenu
    {
        [Parameter]
        public ElementReference ActiverRef { get; set; }

        [Parameter]
        public string ContentStyle { get; set; }

        [Parameter]
        public double OffsetLeft { get; set; }

        protected StringNumber PositionLeft { get; set; }

        public StringNumber PositionTop { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInvokeAsync(JsInteropConstants.AddElementTo, ContentRef, ".m-application");
                StateHasChanged();
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            Task.Run(async () =>
            {
                var rect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, ActiverRef);
                if (PositionLeft != rect.AbsoluteLeft + OffsetLeft || PositionTop != rect.AbsoluteTop + rect.ClientHeight + 1)
                {
                    PositionLeft = rect.AbsoluteLeft + OffsetLeft;
                    PositionTop = rect.AbsoluteTop + rect.ClientHeight + 1;
                    InvokeStateHasChanged();
                }
            });
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            AbstractProvider
                .Merge<BPopover>(props =>
                {
                    props[nameof(Style)] = ContentStyle;
                    props[nameof(BPopover.ClientX)] = PositionLeft;
                    props[nameof(BPopover.ClientY)] = PositionTop;
                });
        }
    }
}
