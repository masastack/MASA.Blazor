namespace Masa.Blazor.Components.TimePicker;

public partial class MTimePickerTitle : MasaComponentBase
{
    [Inject] private I18n I18n { get; set; } = null!;

    [Parameter] public bool AmPmReadonly { get; set; }

    [Parameter] public TimePeriod Period { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public EventCallback<TimePeriod> OnPeriodUpdate { get; set; }

    [Parameter] public int? Hour { get; set; }

    [Parameter] public int? Minute { get; set; }

    [Parameter] public int? Second { get; set; }

    [Parameter] public bool UseSeconds { get; set; }

    [Parameter] public SelectingTime Selecting { get; set; }

    [Parameter] public EventCallback<SelectingTime> OnSelectingUpdate { get; set; }

    [Parameter] public bool AmPm { get; set; }

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

    public string DisplayMinute => Minute == null ? "--" : Pad(Minute.Value);

    public string DisplaySecond => Second == null ? "--" : Pad(Second.Value);

    public string? AmText { get; protected set; }

    public string? PmText { get; protected set; }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        AmText = I18n.T("$masaBlazor.timePicker.am");
        PmText = I18n.T("$masaBlazor.timePicker.pm");

        return base.SetParametersAsync(parameters);
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
        if (Selecting == SelectingTime.Hour || Disabled)
        {
            return;
        }

        if (OnSelectingUpdate.HasDelegate)
        {
            await OnSelectingUpdate.InvokeAsync(SelectingTime.Hour);
        }
    }

    public async Task HandleOnMinuteClickAsync(MouseEventArgs args)
    {
        if (Selecting == SelectingTime.Minute || Disabled)
        {
            return;
        }

        if (OnSelectingUpdate.HasDelegate)
        {
            await OnSelectingUpdate.InvokeAsync(SelectingTime.Minute);
        }
    }

    public async Task HandleOnSecondClickAsync(MouseEventArgs args)
    {
        if (Selecting == SelectingTime.Second || Disabled)
        {
            return;
        }

        if (OnSelectingUpdate.HasDelegate)
        {
            await OnSelectingUpdate.InvokeAsync(SelectingTime.Second);
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

    private Block _block = new("m-time-picker-title");

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _block.Name;
    }
}