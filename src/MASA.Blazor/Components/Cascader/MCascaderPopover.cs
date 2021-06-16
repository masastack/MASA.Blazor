using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MCascaderPopover : MPopover
    {
        [Parameter]
        public ElementReference ActivatorRef { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInvokeAsync(JsInteropConstants.AddElementTo, Ref, ".m-application");
                StateHasChanged();
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            Task.Run(async () =>
            {
                if (ActivatorRef.Id != null && ActivatorRef.Context != null)
                {
                    var rect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, ActivatorRef);
                    if (ClientX != rect.AbsoluteLeft || ClientY != rect.AbsoluteTop + rect.ClientHeight + 1)
                    {
                        ClientX = rect.AbsoluteLeft;
                        ClientY = rect.AbsoluteTop + rect.ClientHeight + 1;
                        InvokeStateHasChanged();
                    }
                }
            });
        }
    }
}
