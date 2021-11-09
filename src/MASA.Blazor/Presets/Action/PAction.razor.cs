using System.Linq.Expressions;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace MASA.Blazor.Presets
{
    public partial class PAction : BDomComponentBase
    {
        private bool _loading;
        private bool _set;

        private ElementReference ButtonRef { get; set; }
        private MButton ButtonForwardRef { get; set; }
        private ElementReference IconRef { get; set; }
        private MIcon IconForwardRef { get; set; }
        private ElementReference LabelRef { get; set; }
        
        internal double BtnWidth { get; set; }
        internal double IconWidth { get; set; }
        internal double LabelWidth { get; set; }
        internal double SpaceWidth { get; set; }

        [Inject]
        public DomEventJsInterop DomEventJsInterop { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [CascadingParameter]
        public PActions Actions { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public string Tip { get; set; }

        [Parameter]
        public bool Visible { get; set; } = true;

        private ActionTypes ActionType => Actions?.Type ?? ActionTypes.IconLabel;

        private string ComputedColor => Color ?? Actions?.Color;

        private bool Depressed => Actions?.Depressed ?? false;

        private bool Large => Actions?.Large ?? false;

        private bool Outlined => Actions?.Outlined ?? false;

        private bool Plain => Actions?.Plain ?? false;

        private bool Rounded => Actions?.Rounded ?? false;
        
        private bool ShowIcon => ActionType == ActionTypes.Icon || ActionType == ActionTypes.IconLabel;

        private bool ShowLabel => ActionType == ActionTypes.Label || ActionType == ActionTypes.IconLabel;
        
        private bool Small => Actions?.Small ?? false;

        private bool Text => Actions?.Text ?? false;

        private bool Tile => Actions?.Tile ?? false;

        private bool TooltipDisabled => ActionType != ActionTypes.Icon || Disabled;

        private bool XSmall => Actions?.XSmall ?? false;

        private bool XLarge => Actions?.XLarge ?? false;

        internal double IconBtnWidth => IconWidth + SpaceWidth;
        
        internal double LabelBtnWidth => LabelWidth + SpaceWidth;

        protected override void OnInitialized()
        {
            if (Visible)
            {
                Actions.Register(this);
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (Visible)
            {
                Actions.Register(this);
            }
            else
            {
                Actions.Unregister(this);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                ButtonRef = ButtonForwardRef.Ref;
                IconRef = IconForwardRef.Ref;
            }

            if (LabelRef.Context != null)
            {
                if (!_set)
                {
                    await ResetWidths();
                    await Actions.CheckWidths();
                    _set = true;
                }
            }
        }

        private async Task ResetWidths()
        {
            IconWidth = await GetElementWidth(IconRef);
            LabelWidth = await GetElementWidth(LabelRef);
            BtnWidth = await GetElementWidth(ButtonRef);
            SpaceWidth = BtnWidth - IconWidth - LabelWidth; // padding, margin
        }

        private async Task<double> GetElementWidth(ElementReference el)
        {
            var rect = await JsInvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, el);
            return rect.Width;
        }

        private async Task HandleClick(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                _loading = true;

                try
                {
                    await OnClick.InvokeAsync();
                }
                finally
                {
                    _loading = false;
                }
            }
        }
    }

    internal enum ActionTypes
    {
        Icon = 1,

        Label,

        IconLabel
    }
}