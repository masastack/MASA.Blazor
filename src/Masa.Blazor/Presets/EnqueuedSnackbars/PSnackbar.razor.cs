using BlazorComponent.Abstracts;

namespace Masa.Blazor.Presets;

public partial class PSnackbar
{
    [CascadingParameter] private PEnqueuedSnackbars? EnqueuedSnacks { get; set; }

    #region parameters from MSnackbar

    [Parameter] public bool Value { get; set; } = true;

    [Parameter] public EventCallback<bool> ValueChanged { get; set; }

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public string? ContentClass { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public StringNumber? Elevation { get; set; }

    [Parameter] public bool Light { get; set; }

    [Parameter] public bool MultiLine { get; set; }

    [Parameter] public EventCallback OnClosed { get; set; }

    [Parameter] public bool Outlined { get; set; }

    [Parameter] public StringBoolean? Rounded { get; set; }

    [Parameter] public bool Shaped { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public bool Text { get; set; }

    [Parameter] public bool Tile { get; set; }

    [Parameter] public int Timeout { get; set; } = 5000;

    [Parameter] public bool Vertical { get; set; }

    [Parameter] public StringNumber? Width { get; set; }

    [Parameter] public bool Left { get; set; }

    [Parameter] public bool Right { get; set; }

    [Parameter] public bool Bottom { get; set; }

    [Parameter] public bool Top { get; set; }

    #endregion

    [Parameter] public Guid EnqueueId { get; set; }

    [Parameter] public string? Title { get; set; }

    [Parameter] public string? Content { get; set; }

    [Parameter] public AlertTypes Type { get; set; }

    [Parameter] public string? ActionColor { get; set; }

    [Parameter] public string? ActionText { get; set; }

    [Parameter] public Func<Task>? OnAction { get; set; }

    [Parameter] public EventCallback OnClose { get; set; }

    [Parameter] public bool Closeable { get; set; }

    private bool _actionLoading;
    private ComponentCssProvider? _cssProvider;

    private string? ComputedColor
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(Color))
            {
                return Color;
            }

            return Type switch
            {
                AlertTypes.Success => "success",
                AlertTypes.Info    => "info",
                AlertTypes.Warning => "warning",
                AlertTypes.Error   => "error",
                _                  => null
            };
        }
    }

    private string? IconColor => Text || Outlined ? ComputedColor : null;

    private string? ComputedIcon
    {
        get
        {
            return Type switch
            {
                AlertTypes.Success => "$success",
                AlertTypes.Error   => "$error",
                AlertTypes.Info    => "$info",
                AlertTypes.Warning => "$warning",
                _                  => null
            };
        }
    }

    private int ComputedTimeout
    {
        get
        {
            var timeout = EnqueuedSnacks?.Timeout ?? Timeout;
            return timeout >= 0 ? timeout : 0;
        }
    }

    private bool ComputedCloseable => EnqueuedSnacks?.Closeable ?? Closeable;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _cssProvider = new ComponentCssProvider(() => Class, () => Style)
                       .Apply("wrapper", css =>
                       {
                           css.Add("m-alert__wrapper");
                       })
                       .Apply("icon", css =>
                       {
                           css.Add("m-alert__icon");
                       })
                       .Apply("title", css =>
                       {
                           css.Add("m-alert__title");
                       });
    }

    private async Task HandleOnAction()
    {
        _actionLoading = true;

        if (OnAction != null)
        {
            await OnAction();
        }

        _actionLoading = false;

        Value = false;
    }

    private async Task HandleOnClosed()
    {
        if (OnClosed.HasDelegate)
        {
            await OnClosed.InvokeAsync();
        }

        EnqueuedSnacks?.RemoveSnackbar(EnqueueId);
    }

    private void HandleOnClose()
    {
        Value = false;

        _ = OnClose.InvokeAsync();
    }
}
