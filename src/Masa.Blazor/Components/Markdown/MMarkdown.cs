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
    public class MMarkdown : BMarkdown, IAsyncDisposable
    {
        [Parameter]
        public string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    _wattingUpdate = true;
                }
            }
        }
        
        [Parameter]
        public string Html { get; set; }

        [Parameter]
        public Dictionary<string, object> Options { get; set; }

        [Parameter] public EventCallback<string> ValueChanged { get; set; }

        [Parameter] public EventCallback<string> HtmlChanged { get; set; }

        [Parameter] public EventCallback<string> OnFocus { get; set; }

        [Parameter] public EventCallback<string> OnBlur { get; set; }

        [Parameter] public EventCallback<string> OnEscPress { get; set; }

        [Parameter] public EventCallback<string> OnCtrlEnterPress { get; set; }

        [Parameter] public EventCallback<string> OnSelect { get; set; }

        [Parameter] public EventCallback<string> OnToolbarButtonClick { get; set; }

        private bool _editorRendered = false;
        private bool _wattingUpdate = false;
        private string _value;

        private bool _afterFirstRender = false;

        private DotNetObjectReference<MMarkdown> ObjRef { get; set; }
        private IJSObjectReference VditorHelper { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Watcher
                .Watch<string>(nameof(Value), async val =>
                {
                    if (_wattingUpdate && _editorRendered)
                    {
                        _wattingUpdate = false;
                        await SetValue(_value, true);
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
            _afterFirstRender = true;
            await CreateMarkdown();
        }

        public async Task CreateMarkdown()
        {
            ObjRef = DotNetObjectReference.Create(this);
            VditorHelper = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/js/vditor/vditor-helper.js");
            await VditorHelper.InvokeVoidAsync("init", Ref, ObjRef, Value, Options);
        }

        public ValueTask<string> GetValue()
        {
            return VditorHelper.InvokeAsync<string>("getValue", Ref);
        }

        public ValueTask<string> GetHtml()
        {
            return VditorHelper.InvokeAsync<string>("getHtml", Ref);
        }

        public async Task SetValue(string value, bool clearStack = false)
        {
            await VditorHelper.InvokeVoidAsync("setValue", Ref, value, clearStack);
        }

        public async Task InsertValue(string value, bool render = true)
        {
            await VditorHelper.InvokeVoidAsync("insertValue", Ref, value, render);
        }

        public async Task Destroy()
        {
            await VditorHelper.InvokeVoidAsync("destroy", Ref);
        }

        [JSInvokable]
        public async Task HandleRendered()
        {
            _editorRendered = true;
            if (!string.IsNullOrWhiteSpace(Value) && HtmlChanged.HasDelegate)
            {
                Html = await GetHtml();
                await HtmlChanged.InvokeAsync(Html);
            }
        }

        [JSInvokable]
        public async Task HandleInput(string value)
        {
            _value = value;
            _wattingUpdate = false;

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }

            if (HtmlChanged.HasDelegate)
            {
                Html = await GetHtml();
                await HtmlChanged.InvokeAsync(Html);
            }
        }

        [JSInvokable]
        public void HandleFocus(string value)
        {
            if (OnFocus.HasDelegate)
            {
                OnFocus.InvokeAsync(value);
            }
        }

        [JSInvokable]
        public void HandleBlur(string value)
        {
            if (OnBlur.HasDelegate)
            {
                OnBlur.InvokeAsync(value);
            }
        }

        [JSInvokable]
        public void HandleEscPress(string value)
        {
            if (OnEscPress.HasDelegate)
            {
                OnEscPress.InvokeAsync(value);
            }
        }

        [JSInvokable]
        public void HandleCtrlEnterPress(string value)
        {
            if (OnCtrlEnterPress.HasDelegate)
            {
                OnCtrlEnterPress.InvokeAsync(value);
            }
        }

        [JSInvokable]
        public void HandleSelect(string value)
        {
            if (OnSelect.HasDelegate)
            {
                OnSelect.InvokeAsync(value);
            }
        }

        [JSInvokable]
        public void HandleToolbarButtonClick(string btnName)
        {
            if (OnToolbarButtonClick.HasDelegate)
            {
                OnToolbarButtonClick.InvokeAsync(btnName);
            }
        }
        public async ValueTask DisposeAsync()
        {
            try
            {
                await Destroy();
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
