using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor
{
    public class MDatePickerHeader : BDatePickerHeader, IThemeable, IDatePickerHeader
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public string Color { get; set; } = "accent";

        [Parameter]
        public DateOnly? Min { get; set; }

        [Parameter]
        public DateOnly? Max { get; set; }

        [Parameter]
        public EventCallback<DateOnly> OnInput { get; set; }

        [Parameter]
        public DateOnly Value
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

        [Parameter]
        public EventCallback OnToggle { get; set; }

        [Parameter]
        public string PrevIcon { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public string NextIcon { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public Func<DateOnly, string> Format { get; set; }

        [Parameter]
        public DatePickerType ActivePicker { get; set; }

        [Parameter]
        public string Locale { get; set; }



        [Inject]
        public MasaBlazor MasaBlazor { get; set; }

        public bool RTL => MasaBlazor.RTL;

        protected bool IsReversing { get; set; }

        public string Transition => IsReversing == !MasaBlazor.RTL ? "tab-reverse-transition" : "tab-transition";

        public Dictionary<string, object> ButtonAttrs => new()
        {
            { "type", "button" },
            {
                "onclick",
                CreateEventCallback<MouseEventArgs>(HandleOnClickAsync)
            }
        };

        private async Task HandleOnClickAsync(MouseEventArgs args)
        {
            if (OnToggle.HasDelegate)
            {
                await OnToggle.InvokeAsync();
            }
        }

        public Func<DateOnly, string> Formatter
        {
            get
            {
                if (Format != null)
                {
                    return Format;
                }

                return ActivePicker == DatePickerType.Date ? DateFormatters.Date(Locale) : DateFormatters.Year(Locale);
            }
        }


        public DateOnly CalculateChange(int sign)
        {
            if (ActivePicker == DatePickerType.Month)
            {
                var date = Value.AddYears(sign);
                return new DateOnly(date.Year, 1, 1);
            }

            return MonthChange(Value, sign);
        }

        public static DateOnly MonthChange(DateOnly value, int sign)
        {
            var date = value.AddMonths(sign);
            return new DateOnly(date.Year, date.Month, 1);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Watcher
                .Watch<DateOnly>(nameof(Value), (newVal, oldVal) =>
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
                        .Add("m-date-picker-header")
                        .AddIf("m-date-picker-header--disabled", () => Disabled)
                        .AddTheme(IsDark);
                })
                .Apply("value", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-date-picker-header__value")
                        .AddIf("m-date-picker-header__value--disabled", () => Disabled);
                })
                .Apply("header", cssBuilder =>
                {
                    var color = !Disabled ? (Color ?? "accent") : "";
                    cssBuilder
                        .AddTextColor(color);
                }, styleBuilder =>
                {
                    var color = !Disabled ? (Color ?? "accent") : "";
                    styleBuilder
                        .AddTextColor(color);
                });

            AbstractProvider
                .ApplyDatePickerHeaderDefault()
                .Apply<BButton, MButton>(attrs =>
                {
                    var change = attrs.Index;
                    var calculateChange = CalculateChange(change);
                    var disabled = Disabled || (change < 0 && Min != null && calculateChange < Min) || (change > 0 && Max != null && calculateChange > Max);

                    attrs[nameof(MButton.Dark)] = Dark;
                    attrs[nameof(MButton.Disabled)] = disabled;
                    attrs[nameof(MButton.Icon)] = true;
                    attrs[nameof(MButton.Light)] = Light;

                    attrs[nameof(MButton.StopPropagation)] = true;
                    attrs[nameof(MButton.OnClick)] = CreateEventCallback<MouseEventArgs>(async args =>
                    {
                        if (OnInput.HasDelegate)
                        {
                            await OnInput.InvokeAsync(calculateChange);
                        }
                    });
                })
                .Apply<BIcon, MIcon>();
        }
    }
}
