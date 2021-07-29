using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MASA.Blazor.Presets
{
    public partial class FormModal
    {
        private MForm _form;

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

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> Attributes { get; set; }

        private async Task HandleOnOk(MouseEventArgs args)
        {
            if (OnOk.HasDelegate)
            {
                var context = _form.UseEditContext();
                if (context.Validate())
                {
                    await OnOk.InvokeAsync(args);
                }
            }
        }
    }
}
