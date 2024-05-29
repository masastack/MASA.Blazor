

namespace Masa.Blazor.Components.DatePicker;

public partial class MDatePickerTitle : MasaComponentBase
{
    [Parameter]
    public DateOnly Value
    {
        get => GetValue<DateOnly>();
        set => SetValue(value);
    }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public bool SelectingYear { get; set; }

    [Parameter] public EventCallback<bool> OnSelectingYearUpdate { get; set; }

    [Parameter] public string? Year { get; set; }

    [Parameter] public string? YearIcon { get; set; }

    [Parameter] public string? Date { get; set; }

    public string ComputedTransition => IsReversing ? "picker-reverse-transition" : "picker-transition";

    protected bool IsReversing { get; set; }

    public async Task HandleOnYearBtnClickAsync(MouseEventArgs arg)
    {
        var active = SelectingYear;
        if (active)
        {
            return;
        }

        if (OnSelectingYearUpdate.HasDelegate)
        {
            await OnSelectingYearUpdate.InvokeAsync(true);
        }
    }

    public async Task HandleOnTitleDateBtnClickAsync(MouseEventArgs arg)
    {
        var active = !SelectingYear;
        if (active)
        {
            return;
        }

        if (OnSelectingYearUpdate.HasDelegate)
        {
            await OnSelectingYearUpdate.InvokeAsync(false);
        }
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher
            .Watch<DateOnly>(nameof(Value), (val, prev) => { IsReversing = val < prev; });
    }

    private static Block _block = new Block("m-date-picker-title");
    private ModifierBuilder _modifierBuilder =_block.CreateModifierBuilder();
    private static Block _btnBlock = new Block("m-picker__title__btn");
    private static ModifierBuilder _btnModiferBuilder = _btnBlock.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add(Disabled).Build();
    }
}