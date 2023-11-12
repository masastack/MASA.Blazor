using Masa.Blazor.Components.Editor;
using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor
{
    public class MEditor : BEditor, IAsyncDisposable
    {
        [Parameter]
        public bool Markdown { get; set; }

        [Parameter]
        public bool ReadOnly { get; set; }

        [Parameter]
        [MassApiParameter("snow")]
        public string Theme { get; set; } = "snow";

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
        public MEditorUpload? Upload { get; set; }

        /// <summary>
        /// 编辑器内容便变更回调
        /// </summary>
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnBlur { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnReady { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnFocus { get; set; }

        [Parameter]
        public Func<List<EditorUploadFileItem>, Task<bool>>? BeforeAllUploadAsync { get; set; }

        /// <summary>
        /// Multiple modules are separated by commas
        /// </summary>
        [Parameter]
        public string? AdditionalModules { get; set; }

        private string? _value;
        private bool _waitingUpdate = false;
        private bool _editorRendered = false;
        private IJSObjectReference? _quillHelper;
        private DotNetObjectReference<MEditor>? _objRef;

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<string>(nameof(Value), ValueChangeCallback);
        }

        private async void ValueChangeCallback(string _)
        {
            if (_waitingUpdate && _editorRendered)
            {
                _waitingUpdate = false;
                await SetHtmlAsync(_value);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (IsDisposed || !firstRender)
            {
                return;
            }

            await CreateEditorAsync();
        }

        public async Task CreateEditorAsync()
        {
            _objRef = DotNetObjectReference.Create(this);
            _quillHelper = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/js/quill/quill-helper.js");

            await _quillHelper.InvokeVoidAsync("init",
                ContentRef,
                _objRef,
                ToolbarRef,
                AdditionalModules,
                ReadOnly,
                Placeholder,
                Theme,
                Markdown,
                Upload);
            if (!string.IsNullOrEmpty(Value)) await SetHtmlAsync(Value);
        }

        public override async Task<string?> GetTextAsync()
        {
            return await _quillHelper.TryInvokeAsync<string>("getText", ContentRef);
        }

        public override async Task<string?> GetHtmlAsync()
        {
            return await _quillHelper.TryInvokeAsync<string>("getHtml", ContentRef);
        }

        public override async Task<string?> GetContentAsync()
        {
            return await _quillHelper.TryInvokeAsync<string>("getContent", ContentRef);
        }

        public async Task SetContentAsync(string? content)
        {
            await _quillHelper.TryInvokeVoidAsync("setContent", ContentRef, content);
        }

        public async Task SetHtmlAsync(string? quillHtml)
        {
            await _quillHelper.TryInvokeVoidAsync("setHtml", ContentRef, quillHtml);
        }

        public async Task InsertImageAsync(string? imageUrl, int? editorIndex = null)
        {
            await _quillHelper.TryInvokeVoidAsync("insertImage", ContentRef, imageUrl, editorIndex);
        }

        public async Task EnableAsync()
        {
            await _quillHelper.TryInvokeVoidAsync("enableEditor", ContentRef, true);
        }

        public async Task DisableAsync()
        {
            await _quillHelper.TryInvokeVoidAsync("enableEditor", ContentRef, false);
        }

        [JSInvokable]
        public async Task HandleRenderedAsync()
        {
            _editorRendered = true;
            if (OnReady.HasDelegate)
            {
                await OnReady.InvokeAsync();
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
        }

        [JSInvokable]
        public async Task HandleOnBlurAsync()
        {
            if (OnBlur.HasDelegate)
            {
                await OnBlur.InvokeAsync();
            }
        }

        [JSInvokable]
        public async Task HandleOnFocusAsync()
        {
            if (OnFocus.HasDelegate)
            {
                await OnFocus.InvokeAsync();
            }
        }

        [JSInvokable]
        public async Task HandleFileChanged(List<EditorUploadFileItem> flist)
        {
            if (BeforeAllUploadAsync != null)
            {
                await BeforeAllUploadAsync.Invoke(flist);
                return;
            }

            if (Upload != default)
            {
                await _quillHelper.TryInvokeVoidAsync("uploadFilePic", ContentRef, Ref, Upload, 0);
            }
            else
            {
                foreach (var item in flist)
                {
                    await InsertImageAsync("https://cdn.masastack.com/stack/images/website/masa-blazor/logo.png");
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            _objRef?.Dispose();
            base.Dispose(disposing);
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            try
            {
                if (_quillHelper != null)
                    await _quillHelper.DisposeAsync();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
