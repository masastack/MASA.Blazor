using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor.Presets
{
    public partial class Drawer
    {
        private bool _loading;
        private string _style;
        private readonly string _defaultStyle = "position: fixed; z-index: 201; ";

        private StringNumber _minWidth = (StringNumber)256;

        private string Style { get => _defaultStyle + _style; set => _style = value; }

        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public string Title { get; set; }


        [Parameter]
        public bool Persistent { get; set; }

        [Parameter]
        public StringNumber Width { get; set; } = (StringNumber)256;

        [Parameter]
        public StringNumber MinWidth
        {
            get
            {
                return _minWidth;
            }
            set
            {
                var (isNumber, number) = _minWidth.TryGetNumber();
                _style = isNumber ? $"min-width: {number}px" : $"min-width: {_minWidth}";

                _minWidth = value;
            }
        }

        [Parameter]
        public bool ShowActions { get; set; } = true;

        [Parameter]
        public EventCallback<bool> LoadingChanged { get; set; }

        [Parameter]
        public string OkText { get; set; } = "确定";

        [Parameter]
        public string CancelText { get; set; } = "取消";

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnOk { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnCancel { get; set; }

        private void HandleOverlayClick(MouseEventArgs args)
        {
            if (!Persistent)
            {
                HandleOnCancel(args);
            }
        }

        private void HandleOnCancel(MouseEventArgs args)
        {
            if (OnCancel.HasDelegate)
            {
                OnCancel.InvokeAsync(args);
            }
        }

        private void HandleOnOk(MouseEventArgs args)
        {
            if (OnOk.HasDelegate)
            {
                _loading = true;
                OnOk.InvokeAsync(args);
                _loading = false;
            }
        }
    }
}
