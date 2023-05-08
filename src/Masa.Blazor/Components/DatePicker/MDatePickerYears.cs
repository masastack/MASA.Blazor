﻿namespace Masa.Blazor
{
    public partial class MDatePickerYears : BDatePickerYears, IDatePickerYears
    {
        [Parameter]
        public string? Color { get; set; }

        [Parameter]
        public DateOnly? Min { get; set; }

        [Parameter]
        public DateOnly? Max { get; set; }

        [Parameter]
        public int Value { get; set; }

        [Parameter]
        public EventCallback<int> OnInput { get; set; }

        [Parameter]
        public Func<DateOnly, string>? Format { get; set; }

        [Parameter]
        public CultureInfo Locale { get; set; } = null!;

        public Func<DateOnly, string> Formatter
        {
            get
            {
                if (Format != null)
                {
                    return Format;
                }

                return DateFormatters.Year(Locale);
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
                    var active = (bool)(cssBuilder.Data ?? false);
                    var color = active ? Color ?? "primary" : "";
                    cssBuilder
                        .AddIf("active", () => active)
                        .AddTextColor(color);
                }, styleBuilder =>
                {
                    var active = (bool)(styleBuilder.Data ?? false);
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
