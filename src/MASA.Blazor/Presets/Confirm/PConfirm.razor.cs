using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;
using BlazorComponent;

namespace MASA.Blazor.Presets
{
    public partial class PConfirm
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
                        AlertTypes.Success => "mdi-checkbox-marked-circle-outline",
                        AlertTypes.Error => "mdi-alert-circle-outline",
                        AlertTypes.Info => "mdi-information",
                        AlertTypes.Warning => "mdi-alert-outline",
                        _ => null,
                    }
                    : _icon;
            }
            set { _icon = value; }
        }

        [Parameter]
        public string IconColor
        {
            get
            {
                return string.IsNullOrEmpty(_iconColor)
                    ? Type switch
                    {
                        AlertTypes.Success => "success",
                        AlertTypes.Info => "info",
                        AlertTypes.Warning => "warning",
                        AlertTypes.Error => "error",
                        _ => null
                    }
                    : _iconColor;
            }
            set { _iconColor = value; }
        }

        [Parameter]
        public AlertTypes Type { get; set; }

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
                        AlertTypes.Success => "success",
                        AlertTypes.Info => "info",
                        AlertTypes.Warning => "warning",
                        AlertTypes.Error => "error",
                        _ => "primary"
                    }
                    : _okColor ?? "primary";
            }
            set { _okColor = value; }
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