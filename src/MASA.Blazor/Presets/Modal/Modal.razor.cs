using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Masa.Blazor.Presets
{
    public partial class Modal
    {
        private bool _loading;
        private string _bodyStyle;
        private readonly string _defaultBodyStyle = "padding:24px;";

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        private MCardText BodyRef { get; set; }

        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public string BodyStyle
        {
            get => _defaultBodyStyle + _bodyStyle;
            set => _bodyStyle = value;
        }

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

        private async Task HandleOnOk(MouseEventArgs args)
        {
            if (OnOk.HasDelegate)
            {
                _loading = true;
                await OnOk.InvokeAsync(args);
                _loading = false;
            }
        }

        private async Task HandleOnCancel(MouseEventArgs args)
        {
            if (OnCancel.HasDelegate)
            {
                await OnCancel.InvokeAsync(args);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Visible == false)
            {
                await ScrollToTop();
            }
        }

        private async Task ScrollToTop()
        {
            if (ScrollToTopOnHide && BodyRef?.Ref != null)
            {
                await JsRuntime.InvokeVoidAsync(JsInteropConstants.ScrollToPosition, BodyRef.Ref, 0);
            }
        }
    }
}
