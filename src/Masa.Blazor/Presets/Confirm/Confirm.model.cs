using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor.Presets
{
    public partial class Confirm
    {
        public class Model
        {
            public bool Visible { get; set; }

            public string Title { get; set; }

            public string Content { get; set; }

            public string Icon { get; set; }

            public string IconColor { get; set; }

            public AlertTypes Type { get; set; } = AlertTypes.None;

            public string OkText { get; set; } = "确定";

            public string OkColor { get; set; } = "primary";

            public string CancelText { get; set; } = "取消";

            public string CancelColor { get; set; } = "default";

            public EventCallback<MouseEventArgs> OnOk { get; set; }

            public EventCallback<MouseEventArgs> OnCancel { get; set; }
        }
    }
}
