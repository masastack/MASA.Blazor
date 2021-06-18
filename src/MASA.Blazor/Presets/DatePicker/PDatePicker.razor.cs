using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MASA.Blazor.Presets
{
    public partial class PDatePicker
    {
        private static readonly int[] _hours = Enumerable.Range(0, 24).ToArray();
        private static readonly int[] _minutes = Enumerable.Range(0, 60).ToArray();
        private static readonly int[] _seconds = Enumerable.Range(0, 60).ToArray();

        private bool _menuVisible = false;

        private int _hour;
        private int _minute;
        private int _second;

        private DateTime _value;
        private DateTime _dateTime;

        private string InputValue
        {
            get
            {
                if (Value == default) return string.Empty;

                return Format == default
                    ? Time
                        ? Value.ToString("yyyy-MM-dd HH:mm:ss")
                        : Value.ToString("yyyy-MM-dd")
                    : Value.ToString(Format);
            }
        }

        private string OnNowText => Time ? "此刻" : "今日";

        [Parameter]
        public DateTime Value
        {
            get => _value;
            set
            {
                _dateTime = value == default ? DateTime.Now : value;
                SetTime(_dateTime);

                _value = value;
            }
        }

        [Parameter]
        public EventCallback<DateTime> ValueChanged { get; set; }

        /// <summary>
        /// Determinate whether to show the time picker
        /// </summary>
        [Parameter]
        public bool Time { get; set; }

        /// <summary>
        /// The template to format <see cref="DateTime"/>, defualt set "yyyy-MM-dd HH:mm:ss"
        /// </summary>
        [Parameter]
        public string Format { get; set; }

        [Parameter]
        public bool Dialog { get; set; }

        #region TextField Parameters

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Readonly { get; set; } = true;

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Filled { get; set; }

        [Parameter]
        public bool Solo { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        #endregion

        private async Task InputValueChanged(string inputValue)
        {
            if (DateTime.TryParse(inputValue, out var value))
            {
                if (ValueChanged.HasDelegate)
                    await ValueChanged.InvokeAsync(value);
            }
            else
            {
                await ValueChanged.InvokeAsync(_dateTime);
            }

            _menuVisible = false;
        }

        private void OnNow()
        {
            _dateTime = DateTime.Now;
            SetTime(_dateTime);
        }

        private void OnCancel()
        {
            _dateTime = Value == default ? DateTime.Now : Value;
            SetTime(_dateTime);

            _menuVisible = false;
        }

        private async Task OnOk()
        {
            _dateTime = new DateTime(_dateTime.Year, _dateTime.Month, _dateTime.Day, _hour, _minute, _second);

            if (ValueChanged.HasDelegate)
                await ValueChanged.InvokeAsync(_dateTime);

            _menuVisible = false;
        }

        private void SetTime(DateTime dateTime)
        {
            if (Time)
            {
                _hour = dateTime.Hour;
                _minute = dateTime.Minute;
                _second = dateTime.Second;
            }
        }

        private async Task HandleClearClick()
        {
            await ValueChanged.InvokeAsync(default);
        }
    }
}
