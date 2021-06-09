using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MECharts : BECharts
    {
        [Parameter]
        public StringNumber Width { get; set; } = 600;

        [Parameter]
        public StringNumber Height { get; set; } = 400;

        [Parameter]
        public object Option { get; set; } = new { };

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(styleAction: styleBuilder =>
                {
                    styleBuilder
                        .AddWidth(Width)
                        .AddHeight(Height);
                });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                var echarts = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/MASA.Blazor/js/echarts.js");
                await echarts.InvokeVoidAsync("init", Ref, Option);
            }
            catch (Exception ex)
            {
                throw new Exception("Echarts is not found,see masa blazor doc for help", ex);
            }
        }
    }
}
