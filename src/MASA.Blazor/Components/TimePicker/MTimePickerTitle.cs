using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    public class MTimePickerTitle : BTimePickerTitle, ITimePickerTitle
    {
        [Parameter]
        public bool AmPmReadonly { get; set; }

        [Parameter]
        public TimePeriod Period { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public EventCallback<TimePeriod> OnPeriodUpdate { get; set; }

        [Parameter]
        public int? Hour { get; set; }

        [Parameter]
        public int? Minute { get; set; }

        [Parameter]
        public int? Second { get; set; }

        [Parameter]
        public bool UseSeconds { get; set; }

        [Parameter]
        public SelectingTimes Selecting { get; set; }

        [Parameter]
        public EventCallback<SelectingTimes> OnSelectingUpdate { get; set; }

        public string DisplayHour
        {
            get
            {
                var hour = Hour;
                if (AmPm)
                {
                    hour = hour != 0 ? ((hour - 1) % 12 + 1) : 12;
                }

                return hour == null ? "--" : AmPm ? $"{hour}" : Pad(hour.Value);
            }
        }

        public string DisplayMinute
        {
            get
            {
                return Minute == null ? "--" : Pad(Minute.Value);
            }
        }

        public string DisplaySecond
        {
            get
            {
                return Second == null ? "--" : Pad(Second.Value);
            }
        }

        private static string Pad(int value)
        {
            return value.ToString().PadLeft(2, '0');
        }

        public async Task HandleOnAmClickAsync(MouseEventArgs args)
        {
            if (Period == TimePeriod.Am || Disabled || Readonly)
            {
                return;
            }

            if (OnPeriodUpdate.HasDelegate)
            {
                await OnPeriodUpdate.InvokeAsync(TimePeriod.Am);
            }
        }

        public async Task HandleOnHourClickAsync(MouseEventArgs args)
        {
            if (Selecting == SelectingTimes.Hour || Disabled)
            {
                return;
            }

            if (OnSelectingUpdate.HasDelegate)
            {
                await OnSelectingUpdate.InvokeAsync(SelectingTimes.Hour);
            }
        }

        public async Task HandleOnMinuteClickAsync(MouseEventArgs args)
        {
            if (Selecting == SelectingTimes.Minute || Disabled)
            {
                return;
            }

            if (OnSelectingUpdate.HasDelegate)
            {
                await OnSelectingUpdate.InvokeAsync(SelectingTimes.Minute);
            }
        }

        public async Task HandleOnSecondClickAsync(MouseEventArgs args)
        {
            if (Selecting == SelectingTimes.Second || Disabled)
            {
                return;
            }

            if (OnSelectingUpdate.HasDelegate)
            {
                await OnSelectingUpdate.InvokeAsync(SelectingTimes.Second);
            }
        }

        public async Task HandleOnPmClickAsync(MouseEventArgs args)
        {
            if (Period == TimePeriod.Pm || Disabled || Readonly)
            {
                return;
            }

            if (OnPeriodUpdate.HasDelegate)
            {
                await OnPeriodUpdate.InvokeAsync(TimePeriod.Pm);
            }
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-time-picker-title");
                })
                .Apply("ampm", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-time-picker-title__ampm")
                        .AddIf("m-time-picker-title__ampm--readonly", () => AmPmReadonly);
                })
                .Apply("picker-button-am", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-picker__title__btn")
                        .AddIf("m-picker__title__btn--active", () => Period == TimePeriod.Am)
                        .AddIf("m-picker__title__btn--readonly", () => Disabled || Readonly);
                })
                .Apply("picker-button-pm", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-picker__title__btn")
                        .AddIf("m-picker__title__btn--active", () => Period == TimePeriod.Pm)
                        .AddIf("m-picker__title__btn--readonly", () => Disabled || Readonly);
                })
                .Apply("picker-button-hour", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-picker__title__btn")
                        .AddIf("m-picker__title__btn--active", () => Selecting == SelectingTimes.Hour)
                        .AddIf("m-picker__title__btn--readonly", () => Disabled);
                })
                .Apply("picker-button-minute", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-picker__title__btn")
                        .AddIf("m-picker__title__btn--active", () => Selecting == SelectingTimes.Minute)
                        .AddIf("m-picker__title__btn--readonly", () => Disabled);
                })
                .Apply("picker-button-second", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-picker__title__btn")
                        .AddIf("m-picker__title__btn--active", () => Selecting == SelectingTimes.Second)
                        .AddIf("m-picker__title__btn--readonly", () => Disabled);
                })
                .Apply("time", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-time-picker-title__time");
                });

            AbstractProvider
                .ApplyTimePickerTitleDefault();
        }
    }
}
