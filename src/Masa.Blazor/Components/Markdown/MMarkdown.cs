namespace Masa.Blazor
{
    public class MMarkdown : BMarkdown, IAsyncDisposable
    {
        [Parameter]
        public override string? Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    _waitingUpdate = true;
                }

                SetValue(value);
            }
        }

        [Parameter]
        public Dictionary<string, object>? Options { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<string> HtmlChanged { get; set; }

        [Parameter]
        public EventCallback<string> OnFocus { get; set; }

        [Parameter]
        public EventCallback<string> OnBlur { get; set; }

        [Parameter]
        public EventCallback<string> OnEscPress { get; set; }

        [Parameter]
        public EventCallback<string> OnCtrlEnterPress { get; set; }

        [Parameter]
        public EventCallback<string> OnSelect { get; set; }

        [Parameter]
        public EventCallback<string> OnToolbarButtonClick { get; set; }

        [Parameter]
        public EventCallback BeforeAllUpload { get; set; }

        private string? _value;
        private bool _editorRendered;
        private bool _waitingUpdate;

        private DotNetObjectReference<MMarkdown>? _objRef;
        private IJSObjectReference? _vditorHelper;

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<string?>(nameof(Value), ValueChangeCallback);
        }

        private async void ValueChangeCallback(string? val)
        {
            if (_waitingUpdate && _editorRendered)
            {
                _waitingUpdate = false;
                await SetValueAsync(_value, true);
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
            _vditorHelper = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/js/vditor/vditor-helper.js");
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
            _editorRendered = true;
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
            _value = value;
            _waitingUpdate = false;

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }

            if (HtmlChanged.HasDelegate)
            {
                Html = await GetHtmlAsync();
                await HtmlChanged.InvokeAsync(Html);
            }
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

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            try
            {
                await _vditorHelper.TryInvokeVoidAsync("destroy", Ref);

                _objRef?.Dispose();

                await _vditorHelper.TryDisposeAsync();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
