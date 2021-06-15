using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace MASA.Blazor.Presets
{
    public partial class Confirm
    {
        private bool _loading;
        private string _icon;
        private string _iconColor;
        private string _okColor;

        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public EventCallback<bool> VisibleChanged { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Icon
        {
            get
            {
                return string.IsNullOrEmpty(_icon)
                    ? Type switch
                    {
                        AlertType.Success => "mdi-check-circle",
                        AlertType.Info => "mdi-information",
                        AlertType.Warning => "mdi-exclamation",
                        AlertType.Error => "mdi-alert",
                        _ => null,
                    }
                    : _icon;
            }
            set
            {
                _icon = value;
            }
        }

        [Parameter]
        public string IconColor
        {
            get
            {
                return string.IsNullOrEmpty(_iconColor)
                    ? Type switch
                    {
                        AlertType.Success => "success",
                        AlertType.Info => "info",
                        AlertType.Warning => "warning",
                        AlertType.Error => "error",
                        _ => null
                    }
                    : _iconColor;
            }
            set
            {
                _iconColor = value;
            }
        }

        [Parameter]
        public AlertType Type { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string OkText { get; set; } = "确定";

        [Parameter]
        public string OkColor
        {
            get
            {
                return string.IsNullOrEmpty(_okColor)
                    ? Type switch
                    {
                        AlertType.Success => "success",
                        AlertType.Info => "info",
                        AlertType.Warning => "warning",
                        AlertType.Error => "error",
                        _ => "primary"
                    }
                    : _okColor ?? "primary";
            }
            set
            {
                _okColor = value;
            }
        }

        [Parameter]
        public string CancelText { get; set; } = "取消";

        [Parameter]
        public string CancelColor { get; set; } = "default";

        [Parameter]
        public EventCallback<MouseEventArgs> OnCancel { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnOk { get; set; }

        [Parameter]
        public RenderFragment Actions { get; set; }

        private async Task HandleOnCancel(MouseEventArgs args)
        {
            await OnCancel.InvokeAsync(args);

            if (VisibleChanged.HasDelegate)
            {
                await VisibleChanged.InvokeAsync(false);
            }
        }

        private async Task HandleOnOk(MouseEventArgs args)
        {
            _loading = true;
            await OnOk.InvokeAsync(args);
            _loading = false;

            if (VisibleChanged.HasDelegate)
            {
                await VisibleChanged.InvokeAsync(false);
            }
        }
    }
}
