using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace MASA.Blazor.Presets
{
    public partial class Modal
    {
        private bool _loading;
        private string _bodySyle;
        private readonly string _defaultBodyStyle = "padding:24px;";

        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public string BodyStyle
        {
            get => _defaultBodyStyle + _bodySyle;
            set => _bodySyle = value;
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
    }
}
