namespace Masa.Blazor.Presets;

public partial class PDateTimePickerBase<TValue> : PDateTimePickerView<TValue>, IDisposable
{
    [Inject] private I18n I18n { get; set; } = null!;

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public RenderFragment<ActivatorProps>? ActivatorContent { get; set; }

    [Parameter] [MassApiParameter(DateTimePickerViewType.Auto)]
    public DateTimePickerViewType ViewType { get; set; }

    [Parameter] public string? TabItemTransition { get; set; }

    [Parameter] public EventCallback OnConfirm { get; set; }

    private bool _menu;
    private DateTimePickerViewType _prevViewType;

    private string Class => BasePickerBlock.Modifier("compact", IsCompact).Build();

    private bool IsCompact { get; set; }

    private bool IsDialog { get; set; }

    internal TValue? InternalDateTime { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        MasaBlazor.MobileChanged += MasaBlazorOnMobileChanged;

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
                IsCompact  = mobile;
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

    private async Task HandleOnConfirm()
    {
        _menu = false;
        await ValueChanged.InvokeAsync(InternalDateTime);
        await OnConfirm.InvokeAsync();
    }

    public void Dispose()
    {
        MasaBlazor.MobileChanged -= MasaBlazorOnMobileChanged;
    }
}
