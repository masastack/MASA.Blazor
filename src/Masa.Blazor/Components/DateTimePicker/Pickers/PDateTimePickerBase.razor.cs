using Masa.Blazor.Components.DateTimePicker;
using Masa.Blazor.Components.DateTimePicker.Pickers;

namespace Masa.Blazor.Presets;

public partial class PDateTimePickerBase<TValue> : PDateTimePickerView<TValue>, IDateTimePicker, IFilterInput,
    IDisposable
{
    [Inject] private I18n I18n { get; set; } = null!;

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [CascadingParameter] protected MInputsFilter? InputsFilter { get; set; }

    [Parameter] public RenderFragment<ActivatorProps>? ActivatorContent { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter]
    [MasaApiParameter(DateTimePickerViewType.Auto)]
    public DateTimePickerViewType ViewType { get; set; }

    [Parameter] public string? TabItemTransition { get; set; }

    [Parameter] public EventCallback OnConfirm { get; set; }

    [Parameter] public RenderFragment<ShortcutContext>? ShortcutsContent { get; set; }

    private readonly IDictionary<string, IDictionary<string, object>> _shortcutDefaults =
        new Dictionary<string, IDictionary<string, object>>()
        {
            [nameof(MButton)] = new Dictionary<string, object>()
            {
                [nameof(MButton.Text)] = true,
                [nameof(MButton.Small)] = true,
                [nameof(MButton.Color)] = "primary",
            }
        };

    private bool _menu;
    private DateTimePickerViewType _prevViewType;
    private string? _display;

    private PDefaultDateTimePickerActivator? _defaultActivator;
    private ShortcutContext _shortcutContext = null!;

    private string Class => BasePickerModifierBuilder.Add("compact", IsCompact).Build();

    private bool IsCompact { get; set; }

    private bool IsDialog { get; set; }

    internal TValue? InternalDateTime { get; set; }


    public void UpdateActivator(PDefaultDateTimePickerActivator pDefaultDateTimePickerActivator)
    {
        _defaultActivator = pDefaultDateTimePickerActivator;

        StateHasChanged();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _shortcutContext = new((dt) => InternalDateTime = (TValue)(object)dt);

        MasaBlazor.MobileChanged += MasaBlazorOnMobileChanged;

        if (InputsFilter is not null && ValueChanged.HasDelegate)
        {
            InputsFilter.RegisterInput(this);
        }

        CheckViewType();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (_prevViewType != ViewType)
        {
            _prevViewType = ViewType;

            CheckViewType();
        }
    }

    protected override void SetDisplay(DateTime? value)
    {
        _display = value?.ToString(_defaultActivator?.Format);
    }

    private void MasaBlazorOnMobileChanged(object? sender, MobileChangedEventArgs e)
    {
        if (!CheckViewType()) return;

        InvokeAsync(StateHasChanged);
    }

    private bool CheckViewType()
    {
        var mobile = MasaBlazor.Breakpoint.Mobile;

        var prevIsCompact = IsCompact;
        var prevIsDialog = IsDialog;

        switch (ViewType)
        {
            case DateTimePickerViewType.Auto:
                IsCompact = mobile;
                IsDialog = mobile;
                break;
            case DateTimePickerViewType.Compact:
                IsCompact = true;
                IsDialog = mobile;
                break;
            case DateTimePickerViewType.Dialog:
                IsCompact = mobile;
                IsDialog = true;
                break;
            case DateTimePickerViewType.Desktop:
                IsCompact = false;
                IsDialog = false;
                break;
            case DateTimePickerViewType.Mobile:
                IsCompact = true;
                IsDialog = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return prevIsCompact != IsCompact || prevIsDialog != IsDialog;
    }

    private void OnMenuChanged(bool val)
    {
        _menu = val;

        if (_menu)
        {
            InternalDateTime = Value;
        }
    }

    private async Task DisplayChanged(string? val)
    {
        if (string.IsNullOrEmpty(val))
        {
            _display = null;
            await ValueChanged.InvokeAsync(default);
            InternalDateTime = default;
        }
        else if (DateTime.TryParse(val, out var dateTime))
        {
            _display = dateTime.ToString(_defaultActivator?.Format);
            await ValueChanged.InvokeAsync((TValue)(object)dateTime);
            InternalDateTime = (TValue)(object)dateTime;
        }
    }

    private async Task HandleOnConfirm()
    {
        _menu = false;
        await ValueChanged.InvokeAsync(InternalDateTime);
        await OnConfirm.InvokeAsync();

        _display = InternalDateTime == null
            ? null
            : ((DateTime)(object)InternalDateTime).ToString(_defaultActivator?.Format);

        InputsFilter?.NotifyFieldChange(null); // TODO: support for form
    }

    public void ResetFilter()
    {
        InternalDateTime = default;
        _ = ValueChanged.InvokeAsync(default);
    }

    public void Dispose()
    {
        MasaBlazor.MobileChanged -= MasaBlazorOnMobileChanged;
    }
}