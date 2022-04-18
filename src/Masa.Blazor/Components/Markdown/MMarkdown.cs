namespace Masa.Blazor
{
    public class MMarkdown : BMarkdown, IAsyncDisposable
    {
        [Parameter]
        public override string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    _wattingUpdate = true;
                }
                SetValue(value);
            }
        }

        [Parameter]
        public Dictionary<string, object> Options { get; set; }

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

        private bool _editorRendered = false;
        private bool _wattingUpdate = false;
        private string _value;

        private DotNetObjectReference<MMarkdown> ObjRef { get; set; }

        private IJSObjectReference VditorHelper { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Watcher
                .Watch<string>(nameof(Value), async val =>
                {
                    if (_wattingUpdate && _editorRendered)
                    {
                        _wattingUpdate = false;
                        await SetValueAsync(_value, true);
                    }
                });
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
            ObjRef = DotNetObjectReference.Create(this);
            VditorHelper = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/js/vditor/vditor-helper.js");
            await VditorHelper.InvokeVoidAsync("init", Ref, ObjRef, Value, Options, BeforeAllUpload.HasDelegate);
        }

        public async ValueTask<string> GetValueAsync()
        {
            return await VditorHelper.InvokeAsync<string>("getValue", Ref);
        }

        public async ValueTask<string> GetHtmlAsync()
        {
            return await VditorHelper.InvokeAsync<string>("getHtml", Ref);
        }

        public async Task SetValueAsync(string value, bool clearStack = false)
        {
            await VditorHelper.InvokeVoidAsync("setValue", Ref, value, clearStack);
        }

        public async Task InsertValueAsync(string value, bool render = true)
        {
            await VditorHelper.InvokeVoidAsync("insertValue", Ref, value, render);
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
        }

        [JSInvokable]
        public async Task HandleInputAsync(string value)
        {
            _value = value;
            _wattingUpdate = false;

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

        public async ValueTask DisposeAsync()
        {
            try
            {
                await VditorHelper.InvokeVoidAsync("destroy", Ref);
                if (ObjRef != null)
                {
                    ObjRef.Dispose();
                }
                if (VditorHelper != null)
                {
                    await VditorHelper.DisposeAsync();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
