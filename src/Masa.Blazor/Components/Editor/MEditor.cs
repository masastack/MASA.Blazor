using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Blazor
{
    public class MEditor : BEditor, IDisposable
    {

        [Parameter]
        public object Options { get; set; } = new { };
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
        /// 是否开启Markdown
        /// </summary>
        [Parameter]
        public bool IsMarkdown { get; set; }
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
            }
        }
        /// <summary>
        /// 编辑器内容便变更回调
        /// </summary>
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnReady { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnBlur { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnFocus { get; set; }
        private string _value;
        private bool _waitingUpdate = false;
        private bool _editorRendered = false;

        private IJSObjectReference QuillHelper;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Watcher
                .Watch<string>(nameof(_value), async val =>
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
            if (IsDisposed|| !firstRender)
            {
                return;
            }
            await CreateEditor();
        }
        //protected override async Task OnParametersSetAsync()
        //{
        //    await base.OnParametersSetAsync();
        //    if (_waitingUpdate && _editorRendered)
        //    {
        //        _waitingUpdate = false;
        //        await SetHtmlAsync(_value);
        //    }
        //}
        public async Task CreateEditor()
        {
            var objRef = DotNetObjectReference.Create(this);
            QuillHelper = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/js/quill-helper.js");
            await QuillHelper.InvokeVoidAsync("init",
                EditorElement,
                objRef,
                ToolBar,
                ReadOnly,
                Placeholder,
                Theme,
                IsMarkdown);
            if(!string.IsNullOrEmpty(Value)) await SetHtmlAsync(Value);
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
        public override async Task<string> GetTextAsync()
        {
            return await QuillHelper.InvokeAsync<string>("getText", EditorElement);
        }
        public override async Task<string> GetHtmlAsync()
        {
            return await QuillHelper.InvokeAsync<string>("getHtml", EditorElement);
        }
        public override async Task<string> GetContentAsync()
        {
            return await QuillHelper.InvokeAsync<string>("getContent", EditorElement);
        }

        public async Task SetContentAsync(string Content)
        {
            await QuillHelper.InvokeAsync<string>("setContent", EditorElement, Content);
        }

        public async Task SetHtmlAsync(string quillHtml)
        {
            await QuillHelper.InvokeAsync<string>("setHtml", EditorElement, quillHtml);
        }

        public async Task InsertImageAsync(string imageURL)
        {
            await QuillHelper.InvokeAsync<string>("insertImage", EditorElement, imageURL);
        }

        public async Task EnableEditorAsync(bool mode)
        {
            await QuillHelper.InvokeAsync<string>("enableEditor", EditorElement, mode);
        }
    }
}
