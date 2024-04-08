namespace Masa.Blazor;

public class MMarkdown : Container
{
    [Parameter] public string? Value { get; set; }

    [Parameter] public Dictionary<string, object>? Options { get; set; }

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public EventCallback<string> ValueChanged { get; set; }

    [Parameter] public string? Html { get; set; }

    [Parameter] public EventCallback<string> HtmlChanged { get; set; }

    [Parameter] public EventCallback<string> OnFocus { get; set; }

    [Parameter] public EventCallback<string> OnBlur { get; set; }

    [Parameter] public EventCallback<string> OnEscPress { get; set; }

    [Parameter] public EventCallback<string> OnCtrlEnterPress { get; set; }

    [Parameter] public EventCallback<string> OnSelect { get; set; }

    [Parameter] public EventCallback<string> OnToolbarButtonClick { get; set; }

    [Parameter] public EventCallback OnAfter { get; set; }

    [Parameter] public EventCallback BeforeAllUpload { get; set; }

    private string? _prevValue;
    private DotNetObjectReference<MMarkdown>? _objRef;
    private IJSObjectReference? _vditorHelper;
    private CancellationTokenSource? _inputCancellationTokenSource;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _prevValue = Value;
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (_prevValue != Value)
        {
            _prevValue = Value;

            _ = SetValueAsync(Value, true);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (IsDisposed || !firstRender)
        {
            return;
        }

        await CreateMarkdownAsync();
    }

    public async Task CreateMarkdownAsync()
    {
        _objRef = DotNetObjectReference.Create(this);
        _vditorHelper =
            await Js.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/js/vditor/vditor-helper.js");
        await _vditorHelper.InvokeVoidAsync("init", Ref, _objRef, Value, Options, BeforeAllUpload.HasDelegate);
    }

    public async ValueTask<string?> GetValueAsync()
    {
        return await _vditorHelper.TryInvokeAsync<string>("getValue", Ref);
    }

    public async ValueTask<string?> GetHtmlAsync()
    {
        return await _vditorHelper.TryInvokeAsync<string>("getHtml", Ref);
    }

    public async Task SetValueAsync(string? value, bool clearStack = false)
    {
        await _vditorHelper.TryInvokeVoidAsync("setValue", Ref, value, clearStack);
    }

    public async Task InsertValueAsync(string value, bool render = true)
    {
        await _vditorHelper.TryInvokeVoidAsync("insertValue", Ref, value, render);
    }

    [JSInvokable]
    public async Task HandleRenderedAsync()
    {
        if (OnAfter.HasDelegate)
        {
            await OnAfter.InvokeAsync();
        }

        if (!string.IsNullOrWhiteSpace(Value) && HtmlChanged.HasDelegate)
        {
            Html = await GetHtmlAsync();
            await HtmlChanged.InvokeAsync(Html);
        }

        if (Readonly)
        {
            await _vditorHelper.TryInvokeVoidAsync("disabled", Ref);
            await _vditorHelper.TryInvokeVoidAsync("preview", Ref);
        }
    }

    [JSInvokable]
    public async Task HandleInputAsync(string value)
    {
        _inputCancellationTokenSource?.Cancel();
        _inputCancellationTokenSource = new CancellationTokenSource();

        await RunTaskInMicrosecondsAsync(async () =>
        {
            _prevValue = value;

            await ValueChanged.InvokeAsync(value);

            if (HtmlChanged.HasDelegate)
            {
                Html = await GetHtmlAsync();
                await HtmlChanged.InvokeAsync(Html);
            }
        }, 500, _inputCancellationTokenSource.Token);
    }

    [JSInvokable]
    public async Task HandleFocusAsync(string value)
    {
        if (OnFocus.HasDelegate)
        {
            await OnFocus.InvokeAsync(value);
        }
    }

    [JSInvokable]
    public async Task HandleBlurAsync(string value)
    {
        if (OnBlur.HasDelegate)
        {
            await OnBlur.InvokeAsync(value);
        }
    }

    [JSInvokable]
    public async Task HandleEscPressAsync(string value)
    {
        if (OnEscPress.HasDelegate)
        {
            await OnEscPress.InvokeAsync(value);
        }
    }

    [JSInvokable]
    public async Task HandleCtrlEnterPressAsync(string value)
    {
        if (OnCtrlEnterPress.HasDelegate)
        {
            await OnCtrlEnterPress.InvokeAsync(value);
        }
    }

    [JSInvokable]
    public async Task HandleSelectAsync(string value)
    {
        if (OnSelect.HasDelegate)
        {
            await OnSelect.InvokeAsync(value);
        }
    }

    [JSInvokable]
    public async Task HandleToolbarButtonClickAsync(string btnName)
    {
        if (OnToolbarButtonClick.HasDelegate)
        {
            await OnToolbarButtonClick.InvokeAsync(btnName);
        }
    }

    [JSInvokable]
    public async Task HandleFileChanged()
    {
        if (BeforeAllUpload.HasDelegate)
        {
            await BeforeAllUpload.InvokeAsync();
        }
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        await _vditorHelper.TryInvokeVoidAsync("destroy", Ref);

        _objRef?.Dispose();

        await _vditorHelper.TryDisposeAsync();
    }
}