using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Masa.Blazor.Presets
{
    public partial class FormModal
    {
        private MForm _form;
        private Func<MouseEventArgs, Task> _debounceHandleOnOk;

        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool ShowActions { get; set; } = true;

        [Parameter]
        public string OkText { get; set; } = "确定";

        [Parameter]
        public string CancelText { get; set; } = "取消";

        [Parameter]
        public EventCallback<MouseEventArgs> OnOk { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnCancel { get; set; }

        [Parameter]
        public RenderFragment Actions { get; set; }

        [Parameter]
        public bool ScrollToTopOnHide { get; set; }

        [Parameter]
        public object Model { get; set; }

        [Parameter]
        public int DebounceInterval { get; set; } = 500;

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> Attributes { get; set; }

        protected override void OnInitialized()
        {
            _debounceHandleOnOk = DebounceEvent<MouseEventArgs>(
                async (e) => await OnOk.InvokeAsync(e),
                TimeSpan.FromMilliseconds(DebounceInterval));

            base.OnInitialized();
        }

        private async Task HandleOnOk(MouseEventArgs args)
        {
            if (_form.EditContext.Validate())
            {
                await _debounceHandleOnOk(args);
            }
        }

        private Func<T, Task> DebounceEvent<T>(Func<T, Task> action, TimeSpan interval)
        {
            return Debounce<T>(async arg =>
            {
                await InvokeAsync(async () =>
                {
                    await action(arg);
                    StateHasChanged();
                });
            }, interval);
        }

        private Func<T, Task> Debounce<T>(Func<T, Task> action, TimeSpan interval)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            var last = 0;
            return async arg =>
            {
                var current = System.Threading.Interlocked.Increment(ref last);

                await Task.Delay(interval);

                if (current == last) await action(arg);
            };
        }
    }
}