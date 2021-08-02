using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    internal partial class MDatePickerYears : BDatePickerYears
    {
        [Parameter]
        public string Color { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-date-picker-years");
                })
                .Apply("year-active", cssBuilder =>
                {
                    cssBuilder
                        .AddTextColor(Color ?? "primary")
                        .Add("active");
                });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInvokeAsync(JsInteropConstants.ScrollToElement, Ref, ActiveRef);
            }
        }
    }
}
