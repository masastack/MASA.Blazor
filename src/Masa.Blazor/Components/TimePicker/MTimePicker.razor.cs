namespace Masa.Blazor;

public partial class MTimePicker : MasaComponentBase
{
    [Inject] private I18n I18n { get; set; } = null!;

    [Parameter]
    public TimePickerType ActivePicker
    {
        get => GetValue<TimePickerType>();
        set => SetValue(value);
    }

    [Parameter] public EventCallback<TimePickerType> ActivePickerChanged { get; set; }

    [Parameter] public string? HeaderColor { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public StringNumber? Elevation { get; set; }

    [Parameter] public bool Flat { get; set; }

    [Parameter] public bool FullWidth { get; set; }

    [Parameter] public bool Landscape { get; set; }

    [Parameter] public bool Light { get; set; }

    [Parameter] [MasaApiParameter(290)] public StringNumber? Width { get; set; } = 290;

    [Parameter]
    [MasaApiParameter(TimeFormat.AmPm)]
    public TimeFormat Format { get; set; } = TimeFormat.AmPm;

    [Parameter] public bool AmPmInTitle { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public bool UseSeconds { get; set; }

    [Parameter] public EventCallback<TimePeriod> OnPeriodUpdate { get; set; }

    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedHours { get; set; }

    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedMinutes { get; set; }

    [Parameter] public OneOf<Func<int, bool>, List<int>> AllowedSeconds { get; set; }

    [Parameter] public TimeOnly? Min { get; set; }

    [Parameter] public TimeOnly? Max { get; set; }

    [Parameter] public bool Scrollable { get; set; }

    [Parameter]
    public TimeOnly? Value
    {
        get { return GetValue<TimeOnly?>(); }
        set { SetValue(value); }
    }

    [Parameter] public EventCallback<TimeOnly?> ValueChanged { get; set; }

    [Parameter] public EventCallback<TimeOnly?> OnChange { get; set; }

    [Parameter] public EventCallback<int> OnHourClick { get; set; }

    [Parameter] public EventCallback<int> OnMinuteClick { get; set; }

    [Parameter] public EventCallback<int> OnSecondClick { get; set; }

    [Parameter] public bool NoTitle { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    public string? AmText { get; set; }

    public string? PmText { get; set; }

    public SelectingTimes Selecting
    {
        get => GetValue(SelectingTimes.Hour);
        set => SetValue(value);
    }

    protected int? InputHour { get; set; }

    protected int? InputMinute { get; set; }

    protected int? InputSecond { get; set; }

    public TimePeriod Period { get; internal set; } = TimePeriod.Am;

    public bool IsAmPm => Format == TimeFormat.AmPm;

    protected Func<int, bool>? IsAllowedHourCb
        => TimeHelper.IsAllowedHour24(AllowedHours, Max, Min);

    protected Func<int, bool>? IsAllowedMinuteCb
        => TimeHelper.IsAllowedMinute(IsAllowedHourCb, AllowedMinutes, Max, Min, InputHour);

    protected Func<int, bool>? IsAllowedSecondCb
        => TimeHelper.IsAllowedSecond(IsAllowedHourCb, IsAllowedMinuteCb, AllowedSeconds, Max, Min, InputHour,
            InputMinute);

    public int? LazyInputHour { get; private set; }

    public int? LazyInputMinute { get; private set; }

    public int? LazyInputSecond { get; private set; }

    private string Pad(int val)
    {
        return val.ToString().PadLeft(2, '0');
    }

    private async Task HandleOnInputAsync(int value)
    {
        if (Selecting == SelectingTimes.Hour)
        {
            InputHour = IsAmPm ? TimeHelper.Convert12To24(value, Period) : value;
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
        switch (Selecting)
        {
            case SelectingTimes.Hour:
                await OnHourClick.InvokeAsync(value);
                break;
            case SelectingTimes.Minute:
                await OnMinuteClick.InvokeAsync(value);
                break;
            case SelectingTimes.Second:
                await OnSecondClick.InvokeAsync(value);
                break;
        }

        var emitChange = Selecting == (UseSeconds ? SelectingTimes.Second : SelectingTimes.Minute);

        if (Selecting == SelectingTimes.Hour)
        {
            Selecting = SelectingTimes.Minute;
        }
        else if (UseSeconds && Selecting == SelectingTimes.Minute)
        {
            Selecting = SelectingTimes.Second;
        }

        if (InputHour == LazyInputHour && InputMinute == LazyInputMinute &&
            (!UseSeconds || InputSecond == LazyInputSecond))
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

    public override Task SetParametersAsync(ParameterView parameters)
    {
        AmText = I18n.T("$masaBlazor.timePicker.am");
        PmText = I18n.T("$masaBlazor.timePicker.pm");

        return base.SetParametersAsync(parameters);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        SetInputData(Value);
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher
            .Watch<TimeOnly?>(nameof(Value), SetInputData)
            .Watch<SelectingTimes>(nameof(Selecting), EmitPicker)
            .Watch<TimePickerType>(nameof(ActivePicker), SetPicker);
    }

    private void SetInputData(TimeOnly? value)
    {
        InputHour = value?.Hour;
        InputMinute = value?.Minute;
        InputSecond = value?.Second;

        Period = (InputHour == 0 || InputHour < 12) ? TimePeriod.Am : TimePeriod.Pm;
    }

    private void EmitPicker(SelectingTimes selecting)
    {
        var activePicker = (TimePickerType)(selecting);
        ActivePickerChanged.InvokeAsync(activePicker);
    }

    private void SetPicker(TimePickerType picker)
    {
        Selecting = (SelectingTimes)picker;
        StateHasChanged();
    }

    private Block _block = new("m-time-picker-clock");

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-picker--time";
    }
}