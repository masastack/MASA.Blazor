using Masa.Blazor.Components.Editor;
using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor
{
    public class MEditor : BEditor, IAsyncDisposable
    {
        /// <summary>
        /// 是否开启Markdown
        /// </summary>
        [Parameter]
        public bool Markdown { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        [Parameter]
        public bool ReadOnly { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [Parameter]
        public string Theme { get; set; } = "snow";

        /// <summary>
        /// 数据双向绑定
        /// </summary>
        [Parameter]
        public override string Value
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
        public MEditorUpload Upload { get; set; }

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
        public Func<List<EditorUploadFileItem>, Task<bool>> BeforeAllUploadAsync { get; set; }

        private string _value { get; set; }
        private bool _waitingUpdate { get; set; } = false;
        private bool _editorRendered { get; set; } = false;

        private IJSObjectReference QuillHelper { get; set; }
        private DotNetObjectReference<MEditor> ObjRef { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Watcher
                .Watch<string>(nameof(Value), async val =>
                {
                    if (_waitingUpdate && _editorRendered)
                    {
                        _waitingUpdate = false;
                        await SetHtmlAsync(_value);
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
            await CreateEditorAsync();
        }

        public async Task CreateEditorAsync()
        {
            ObjRef = DotNetObjectReference.Create(this);
            QuillHelper = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/js/quill/quill-helper.js");
            await QuillHelper.InvokeVoidAsync("init",
                ContentRef,
                ObjRef,
                ToolbarRef,
                ReadOnly,
                Placeholder,
                Theme,
                Markdown,
                Upload);
            if (!string.IsNullOrEmpty(Value)) await SetHtmlAsync(Value);
        }

        public override async Task<string> GetTextAsync()
        {
            return await QuillHelper.InvokeAsync<string>("getText", ContentRef);
        }

        public override async Task<string> GetHtmlAsync()
        {
            return await QuillHelper.InvokeAsync<string>("getHtml", ContentRef);
        }

        public override async Task<string> GetContentAsync()
        {
            return await QuillHelper.InvokeAsync<string>("getContent", ContentRef);
        }

        public async Task SetContentAsync(string Content)
        {
            await QuillHelper.InvokeVoidAsync("setContent", ContentRef, Content);
        }

        public async Task SetHtmlAsync(string quillHtml)
        {
            await QuillHelper.InvokeVoidAsync("setHtml", ContentRef, quillHtml);
        }

        public async Task InsertImageAsync(string imageURL, int? editorIndex = null)
        {
            await QuillHelper.InvokeVoidAsync("insertImage", ContentRef, imageURL, editorIndex);
        }

        public async Task EnableAsync()
        {
            await QuillHelper.InvokeVoidAsync("enableEditor", ContentRef, true);
        }

        public async Task DisableAsync()
        {
            await QuillHelper.InvokeVoidAsync("enableEditor", ContentRef, false);
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
                await QuillHelper.InvokeVoidAsync("uploadFilePic", ContentRef, Ref, Upload, 0);
            }
            else
            {
                foreach (var item in flist)
                {
                    await InsertImageAsync("https://cdn.masastack.com/stack/images/website/masa-blazor/logo.png");
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (ObjRef != null)
                {
                    ObjRef.Dispose();
                }
                if (QuillHelper != null)
                {
                    await QuillHelper.DisposeAsync();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
