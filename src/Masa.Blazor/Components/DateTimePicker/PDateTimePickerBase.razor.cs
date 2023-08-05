namespace Masa.Blazor.Presets;

public partial class PDateTimePickerBase<TValue> : IDisposable
{
    [Inject] private I18n I18n { get; set; } = null!;

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public RenderFragment<ActivatorProps>? ActivatorContent { get; set; }

    [Parameter] public bool? Compact { get; set; }

    [Parameter] public bool? Dialog { get; set; }

    [Parameter] public string? TabItemTransition { get; set; }

    private bool _menu;

    private string Class => BasePickerBlock.Modifier("compact", IsCompact).AddTheme(IsDark).Build();

    private bool IsCompact { get; set; }

    private bool IsDialog { get; set; }

    internal TValue? InternalDateTime { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        MasaBlazor.MobileChanged += MasaBlazorOnMobileChanged;

        CheckIfCompactOrDialogChanged();
    }

    private void MasaBlazorOnMobileChanged(object? sender, MobileChangedEventArgs e)
    {
        if (!CheckIfCompactOrDialogChanged()) return;

        InvokeAsync(StateHasChanged);
    }

    private bool CheckIfCompactOrDialogChanged()
    {
        var mobile = MasaBlazor.Breakpoint.Mobile;
        var changed = false;

        if (Compact is null)
        {
            IsCompact = mobile;
            changed = true;
        }

        if (Dialog is null)
        {
            IsDialog = mobile;
            changed = true;
        }

        return changed;
    }

    private void OnMenuChanged(bool val)
    {
        _menu = val;

        if (_menu)
        {
            InternalDateTime = Value;
        }
    }

    private async Task OnConfirm()
    {
        _menu = false;
        await ValueChanged.InvokeAsync(InternalDateTime);
    }

    public void Dispose()
    {
        MasaBlazor.MobileChanged -= MasaBlazorOnMobileChanged;
    }
}
