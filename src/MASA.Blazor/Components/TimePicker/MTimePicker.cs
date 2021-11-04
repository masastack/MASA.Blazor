using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MTimePicker : BTimePicker, ITimePicker
    {
        [Parameter]
        public string HeaderColor { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public StringNumber Elevation { get; set; }

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool FullWidth { get; set; }

        [Parameter]
        public bool Landscape { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public StringNumber Width { get; set; } = 290;

        [Parameter]
        public TimeFormat Format { get; set; } = TimeFormat.AmPm;

        [Parameter]
        public bool AmPmInTitle { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool UseSeconds { get; set; }

        [Parameter]
        public EventCallback<TimePeriod> OnPeriodUpdate { get; set; }

        [Parameter]
        public OneOf<Func<int, bool>, List<int>> AllowedHours { get; set; }

        [Parameter]
        public OneOf<Func<int, bool>, List<int>> AllowedMinutes { get; set; }

        [Parameter]
        public OneOf<Func<int, bool>, List<int>> AllowedSeconds { get; set; }

        [Parameter]
        public TimeOnly? Min { get; set; }

        [Parameter]
        public TimeOnly? Max { get; set; }

        [Parameter]
        public bool Scrollable { get; set; }

        [Parameter]
        public TimeOnly? Value
        {
            get
            {
                return GetValue<TimeOnly?>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public EventCallback<TimeOnly?> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<TimeOnly?> OnChange { get; set; }

        public SelectingTimes Selecting { get; set; } = SelectingTimes.Hour;

        protected int? InputHour { get; set; }

        protected int? InputMinute { get; set; }

        protected int? InputSecond { get; set; }

        protected TimePeriod Period { get; set; } = TimePeriod.Am;

        public bool IsAmPm => Format == TimeFormat.AmPm;

        protected Func<int, bool> IsAllowedHourCb
        {
            get
            {
                var cb = (Func<int, bool>)null;

                if (AllowedHours.IsT1)
                {
                    cb = val => AllowedHours.AsT1.Contains(val);
                }
                else
                {
                    cb = AllowedHours.AsT0;
                }

                if (Min == null && Max == null)
                {
                    return cb;
                }

                var minHour = Min != null ? Min.Value.Hour : 0;
                var maxHour = Max != null ? Max.Value.Hour : 23;

                return val => val >= minHour && val <= maxHour && (cb == null || cb(val));
            }
        }

        protected Func<int, bool> IsAllowedMinuteCb
        {
            get
            {
                var cb = (Func<int, bool>)null;

                var isHourAllowed = IsAllowedHourCb == null || InputHour == null || IsAllowedHourCb(InputHour.Value);
                if (AllowedMinutes.IsT1)
                {
                    cb = val => AllowedMinutes.AsT1.Contains(val);
                }
                else
                {
                    cb = AllowedMinutes.AsT0;
                }

                if (Min == null && Max == null)
                {
                    return isHourAllowed ? cb : val => false;
                }

                var (minHour, minMinute) = Min != null ? (Min.Value.Hour, Min.Value.Minute) : (0, 0);
                var (maxHour, maxMinute) = Max != null ? (Max.Value.Hour, Max.Value.Minute) : (23, 59);
                var minTime = minHour * 60 + minMinute;
                var maxTime = maxHour * 60 + maxMinute;

                return val =>
                {
                    var time = 60 * InputHour + val;
                    return time >= minTime && time <= maxTime && isHourAllowed && (cb == null || cb(val));
                };
            }
        }

        protected Func<int, bool> IsAllowedSecondCb
        {
            get
            {
                var cb = (Func<int, bool>)null;

                var isHourAllowed = IsAllowedHourCb == null || InputHour == null || IsAllowedHourCb(InputHour.Value);
                var isMinuteAllowed = isHourAllowed && (IsAllowedMinuteCb == null || InputMinute == null || IsAllowedMinuteCb(InputMinute.Value));

                if (AllowedSeconds.IsT1)
                {
                    cb = val => AllowedSeconds.AsT1.Contains(val);
                }
                else
                {
                    cb = AllowedSeconds.AsT0;
                }

                if (Min == null && Max == null)
                {
                    return isMinuteAllowed ? cb : val => false;
                }

                var (minHour, minMinute, minSecond) = Min != null ? (Min.Value.Hour, Min.Value.Minute, Min.Value.Second) : (0, 0, 0);
                var (maxHour, maxMinute, maxSecond) = Max != null ? (Max.Value.Hour, Max.Value.Minute, Max.Value.Second) : (23, 59, 59);
                var minTime = minHour * 3600 + minMinute * 60 + minSecond;
                var maxTime = maxHour * 3600 + maxMinute * 60 + maxSecond;

                return val =>
                {
                    var time = 3600 * InputHour + 60 * InputMinute + val;
                    return time >= minTime && time <= maxTime && isMinuteAllowed && (cb == null || cb(val));
                };
            }
        }

        public int? LazyInputHour { get; private set; }
        public int? LazyInputMinute { get; private set; }
        public int? LazyInputSecond { get; private set; }

        private static string Convert24To12(int hour)
        {
            return $"{(hour > 0 ? ((hour - 1) % 12 + 1) : 12)}";
        }

        private static int Convert12To24(int hour, TimePeriod period)
        {
            return hour % 12 + (period == TimePeriod.Pm ? 12 : 0);
        }

        private string Pad(int val)
        {
            return val.ToString().PadLeft(2, '0');
        }

        private async Task HandleOnInputAsync(int value)
        {
            if (Selecting == SelectingTimes.Hour)
            {
                InputHour = IsAmPm ? Convert12To24(value, Period) : value;
            }
            else if (Selecting == SelectingTimes.Minute)
            {
                InputMinute = value;
            }
            else
            {
                InputSecond = value;
            }

            await EmitValueAsync();
        }

        private async Task EmitValueAsync()
        {
            var value = GenValue();
            if (value != null)
            {
                if (ValueChanged.HasDelegate)
                {
                    await ValueChanged.InvokeAsync(value);
                }
            }
        }

        private TimeOnly? GenValue()
        {
            if (InputHour != null && InputMinute != null && (!UseSeconds || InputSecond != null))
            {
                return new TimeOnly(InputHour.Value, InputMinute.Value, UseSeconds ? InputSecond.Value : 0);
            }

            return null;
        }

        private async Task HandleOnChangeAsync(int value)
        {
            var emitChange = Selecting == (UseSeconds ? SelectingTimes.Second : SelectingTimes.Minute);

            if (Selecting == SelectingTimes.Hour)
            {
                Selecting = SelectingTimes.Minute;
            }
            else if (UseSeconds && Selecting == SelectingTimes.Minute)
            {
                Selecting = SelectingTimes.Second;
            }

            if (InputHour == LazyInputHour && InputMinute == LazyInputMinute && (!UseSeconds || InputSecond == LazyInputSecond))
            {
                return;
            }

            var time = GenValue();
            if (time == null)
            {
                return;
            }

            LazyInputHour = InputHour;
            LazyInputMinute = InputMinute;
            LazyInputSecond = InputSecond;

            if (emitChange)
            {
                if (OnChange.HasDelegate)
                {
                    await OnChange.InvokeAsync(time);
                }
            }
        }

        public async Task HandleOnAmClickAsync(MouseEventArgs args)
        {
            if (Period == TimePeriod.Am || Disabled || Readonly)
            {
                return;
            }

            await SetPeriodAsync(TimePeriod.Am);

            if (OnPeriodUpdate.HasDelegate)
            {
                await OnPeriodUpdate.InvokeAsync(TimePeriod.Am);
            }
        }

        public async Task HandleOnPmClickAsync(MouseEventArgs args)
        {
            if (Period == TimePeriod.Pm || Disabled || Readonly)
            {
                return;
            }

            await SetPeriodAsync(TimePeriod.Pm);

            if (OnPeriodUpdate.HasDelegate)
            {
                await OnPeriodUpdate.InvokeAsync(TimePeriod.Pm);
            }
        }

        private async Task SetPeriodAsync(TimePeriod period)
        {
            Period = period;
            if (InputHour != null)
            {
                var newHour = InputHour.Value + (period == TimePeriod.Am ? -12 : 12);
                InputHour = FirstAllowed(SelectingTimes.Hour, newHour);
                await EmitValueAsync();
            }
        }

        private int? FirstAllowed(SelectingTimes hour, int value)
        {
            //TODO:
            return value;
        }

        protected override void OnInitialized()
        {
            SetInputData(Value);

            Watcher
                .Watch<TimeOnly?>(nameof(Value), val =>
                {
                    SetInputData(val);
                });
        }

        private void SetInputData(TimeOnly? value)
        {
            InputHour = value?.Hour;
            InputMinute = value?.Minute;
            InputSecond = value?.Second;

            Period = (InputHour == 0 || InputHour < 12) ? TimePeriod.Am : TimePeriod.Pm;
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply("m-picker--time")
                .Apply("container", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-time-picker-clock__container");
                })
                .Apply("ampm", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-time-picker-clock__ampm")
                        .AddTextColor(Color ?? "primary");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddTextColor(Color ?? "primary");
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
                });

            AbstractProvider
                .ApplyTimePickerDefault()
                .Apply(typeof(BPicker), typeof(MPicker), props =>
                {
                    props[nameof(MPicker.Color)] = HeaderColor ?? Color;
                    props[nameof(MPicker.Dark)] = Dark;
                    props[nameof(MPicker.Elevation)] = Elevation;
                    props[nameof(MPicker.Flat)] = Flat;
                    props[nameof(MPicker.FullWidth)] = FullWidth;
                    props[nameof(MPicker.Landscape)] = Landscape;
                    props[nameof(MPicker.Light)] = Light;
                    props[nameof(MPicker.Width)] = Width;
                    props[nameof(MPicker.NoTitle)] = NoTitle;
                })
                .Apply(typeof(BTimePickerTitle), typeof(MTimePickerTitle), props =>
                {
                    props[nameof(MTimePickerTitle.AmPm)] = IsAmPm;
                    props[nameof(MTimePickerTitle.AmPmReadonly)] = IsAmPm && !AmPmInTitle;
                    props[nameof(MTimePickerTitle.Disabled)] = Disabled;
                    props[nameof(MTimePickerTitle.Hour)] = InputHour;
                    props[nameof(MTimePickerTitle.Minute)] = InputMinute;
                    props[nameof(MTimePickerTitle.Second)] = InputSecond;
                    props[nameof(MTimePickerTitle.Period)] = Period;
                    props[nameof(MTimePickerTitle.Readonly)] = Readonly;
                    props[nameof(MTimePickerTitle.UseSeconds)] = UseSeconds;
                    props[nameof(MTimePickerTitle.Selecting)] = Selecting;
                    props[nameof(MTimePickerTitle.OnSelectingUpdate)] = CreateEventCallback<SelectingTimes>(value =>
                    {
                        Selecting = value;
                    });
                    props[nameof(MTimePickerTitle.OnPeriodUpdate)] = CreateEventCallback<TimePeriod>(async value =>
                    {
                        await SetPeriodAsync(value);

                        if (OnPeriodUpdate.HasDelegate)
                        {
                            await OnPeriodUpdate.InvokeAsync(value);
                        }
                    });
                })
                .Apply(typeof(BTimePickerClock), typeof(MTimePickerClock), props =>
                {
                    props[nameof(MTimePickerClock.AllowedValues)] = Selecting == SelectingTimes.Hour ? IsAllowedHourCb : (Selecting == SelectingTimes.Minute ? IsAllowedMinuteCb : IsAllowedSecondCb);
                    props[nameof(MTimePickerClock.Color)] = Color;
                    props[nameof(MTimePickerClock.Dark)] = Dark;
                    props[nameof(MTimePickerClock.Disabled)] = Disabled;
                    props[nameof(MTimePickerClock.Double)] = Selecting == SelectingTimes.Hour && !IsAmPm;
                    Func<int, string> format = Selecting == SelectingTimes.Hour ? (IsAmPm ? Convert24To12 : val => $"{val}") : val => Pad(val);
                    props[nameof(MTimePickerClock.Format)] = format;
                    props[nameof(MTimePickerClock.Light)] = Light;
                    props[nameof(MTimePickerClock.Max)] = Selecting == SelectingTimes.Hour ? (IsAmPm && Period == TimePeriod.Am ? 11 : 23) : 59;
                    props[nameof(MTimePickerClock.Min)] = Selecting == SelectingTimes.Hour && IsAmPm && Period == TimePeriod.Pm ? 12 : 0;
                    props[nameof(MTimePickerClock.Readonly)] = Readonly;
                    props[nameof(MTimePickerClock.Scrollable)] = Scrollable;
                    props[nameof(MTimePickerClock.Step)] = Selecting == SelectingTimes.Hour ? 1 : 5;
                    props[nameof(MTimePickerClock.Value)] = Selecting == SelectingTimes.Hour ? InputHour : (Selecting == SelectingTimes.Minute ? InputMinute : InputSecond);
                    props[nameof(MTimePickerClock.OnInput)] = CreateEventCallback<int>(HandleOnInputAsync);
                    props[nameof(MTimePickerClock.OnChange)] = CreateEventCallback<int>(HandleOnChangeAsync);
                });
        }
    }
}
