using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MDatePickerYears : BDatePickerYears, IDatePickerYears
    {
        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public DateOnly? Min { get; set; }

        [Parameter]
        public DateOnly? Max { get; set; }

        [Parameter]
        public int Value { get; set; }

        [Parameter]
        public EventCallback<int> OnInput { get; set; }

        [Parameter]
        public Func<int, string> Format { get; set; }

        public Func<int, string> Formatter
        {
            get
            {
                if (Format != null)
                {
                    return Format;
                }

                return year => $"{year}";
            }
        }

        public async Task HandleOnYearItemClickAsync(int year)
        {
            if (OnInput.HasDelegate)
            {
                await OnInput.InvokeAsync(year);
            }
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-date-picker-years");
                })
                .Apply("year-item", cssBuilder =>
                {
                    var active = (bool)cssBuilder.Data;
                    var color = active ? Color ?? "primary" : "";
                    cssBuilder
                        .AddIf("active", () => active)
                        .AddTextColor(color);
                }, styleBuilder =>
                {
                    var active = (bool)styleBuilder.Data;
                    var color = active ? Color ?? "primary" : "";
                    styleBuilder
                        .AddTextColor(color);
                });

            AbstractProvider
                .ApplyDatePickerYearsDefault();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInvokeAsync(JsInteropConstants.ScrollToActiveElement, Ref);
            }
        }

    }
}
