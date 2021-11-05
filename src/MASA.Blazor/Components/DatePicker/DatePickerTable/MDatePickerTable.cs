using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MDatePickerTable<TValue> : BDatePickerTable, IThemeable, IDatePickerTable
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public Func<DateOnly, bool> AllowedDates { get; set; }

        [Parameter]
        public DateOnly? Min { get; set; }

        [Parameter]
        public DateOnly? Max { get; set; }

        [Parameter]
        public bool Range { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool Scrollable { get; set; }

        [Parameter]
        public TValue Value { get; set; }

        [Parameter]
        public DateOnly Current { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        [Parameter]
        public Func<DateOnly, string> Format { get; set; }

        [Parameter]
        public EventCallback<DateOnly> OnInput { get; set; }

        [Inject]
        public GlobalConfig GlobalConfig { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return Themeable != null && Themeable.IsDark;
            }
        }

        public int DisplayedMonth => TableDate.Month - 1;

        public int DisplayedYear => TableDate.Year;

        public virtual Func<DateOnly, string> Formatter { get; }

        protected bool IsReversing { get; set; }

        public Dictionary<string, object> GetButtonAttrs(DateOnly value) => new()
        {
            { "type", "button" },
            { "disabled", Disabled || !IsDateAllowed(value, Min, Max, AllowedDates) },
            { "onclick", CreateEventCallback<MouseEventArgs>(args => HandleOnClickAsync(value)) }
        };

        private async Task HandleOnClickAsync(DateOnly value)
        {
            if (Disabled)
            {
                return;
            }

            if (IsDateAllowed(value, Min, Max, AllowedDates) && !Readonly)
            {
                if (OnInput.HasDelegate)
                {
                    await OnInput.InvokeAsync(value);
                }
            }
        }

        private bool IsDateAllowed(DateOnly date, DateOnly? min, DateOnly? max, Func<DateOnly, bool> allowedFunc)
        {
            return (allowedFunc == null || allowedFunc(date)) && (min == null || date >= min) && (max == null || date <= max);
        }

        protected virtual bool IsSelected(DateOnly value)
        {
            if (Value is DateOnly date)
            {
                return date == value;
            }
            else if (Value is IList<DateOnly> dates)
            {
                if (Range && dates.Count == 2)
                {
                    return dates.Min() <= value && value <= dates.Max();
                }

                return dates.Contains(value);
            }

            return false;
        }

        protected virtual bool IsCurrent(DateOnly value)
        {
            return value == Current;
        }

        protected override string ComputedTransition
        {
            get
            {
                return IsReversing == !GlobalConfig.RTL ? "tab-reverse-transition" : "tab-transition";
            }
        }

        public override DateOnly TableDate
        {
            get
            {
                return GetValue<DateOnly>();
            }
            set
            {
                SetValue(value);
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Watcher
                .Watch<DateOnly>(nameof(TableDate), (newVal, oldVal) =>
                {
                    IsReversing = newVal < oldVal;
                });
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                       .AddIf("m-date-picker-table--disabled", () => Disabled)
                       .AddTheme(IsDark);
                })
                .Apply("btn", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-btn");

                    var (value, isFloating, isOtherMonth) = ((DateOnly, bool, bool))cssBuilder.Data;
                    var isAllowed = IsDateAllowed(value, Min, Max, AllowedDates);
                    var isSelected = IsSelected(value) && isAllowed;
                    var isCurrent = IsCurrent(value);
                    var color = (isSelected || isCurrent) ? (Color ?? "accent") : "";

                    if (isSelected)
                    {
                        cssBuilder
                            .AddBackgroundColor(color);
                    }
                    else
                    {
                        cssBuilder
                            .AddTextColor(color);
                    }

                    isAllowed = isAllowed && !isOtherMonth;
                    cssBuilder
                        .AddIf("m-size--default", () => !isFloating)
                        .AddIf("m-date-picker-table__current", () => isCurrent)
                        .AddIf("m-btn--active", () => isSelected)
                        .AddIf("m-btn--flat", () => !isAllowed || Disabled)
                        .AddIf("m-btn--text", () => isSelected == isCurrent)
                        .AddIf("m-btn--rounded", () => isFloating)
                        .AddIf("m-btn--disabled", () => !isAllowed || Disabled)
                        .AddIf("m-btn--outlined", () => isCurrent && !isSelected)
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    var (value, isFloating, isOtherMonth) = ((DateOnly, bool, bool))styleBuilder.Data;
                    var isAllowed = IsDateAllowed(value, Min, Max, AllowedDates);
                    var isSelected = IsSelected(value) && isAllowed;
                    var isCurrent = value == Current;
                    var color = (isSelected || isCurrent) ? (Color ?? "accent") : "";

                    if (isSelected)
                    {
                        styleBuilder
                            .AddBackgroundColor(color);
                    }
                    else
                    {
                        styleBuilder
                            .AddTextColor(color);
                    }
                })
                .Apply("btn-content", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-btn__content");
                });

            AbstractProvider
                .ApplyDatePickerTableDefault();
        }
    }
}
