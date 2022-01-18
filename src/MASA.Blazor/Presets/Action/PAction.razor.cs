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
        [Inject]
        public DomEventJsInterop DomEventJsInterop { get; set; }

        [CascadingParameter]
        public PActions Actions { get; set; }

        [CascadingParameter(Name = "_p_action_data")]
        public Action Data { get; set; }

        [Parameter] 
        public ActionTypes? Type { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Depressed { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool Large { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Plain { get; set; }

        [Parameter]
        public bool Rounded { get; set; }

        [Parameter]
        public bool Small { get; set; }

        [Parameter]
        public bool Text { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public string Tip { get; set; }

        [Parameter]
        public bool Visible
        {
            get => _visible;
            set
            {
                if (_visible != value) _visibleChanged = true;

                _visible = value;
            }
        }

        [Parameter]
        public bool XSmall { get; set; }

        [Parameter]
        public bool XLarge { get; set; }
        
        private bool _loading;
        private bool _set;
        private bool _visible = true;
        private bool _visibleChanged = true;

        private ElementReference ButtonRef { get; set; }
        private MButton ButtonForwardRef { get; set; }
        private ElementReference IconRef { get; set; }
        private MIcon IconForwardRef { get; set; }
        private ElementReference LabelRef { get; set; }

        internal double BtnWidth { get; set; }
        internal double IconWidth { get; set; }
        internal double LabelWidth { get; set; }
        internal double SpaceWidth { get; set; }

        internal double IconBtnWidth => IconWidth + SpaceWidth;

        internal double LabelBtnWidth => LabelWidth + SpaceWidth;

        private ActionTypes ActionType => Actions?.Type ?? Type ?? ActionTypes.IconLabel;

        private string ComputedColor => Data?.Color ?? Color;

        private bool ComputedDark => Data?.Dark ?? Dark;

        private bool ComputedDepressed => Data?.Depressed ?? Depressed;

        private bool ComputedDisabled => Data?.Disabled ?? Disabled;

        private string ComputedIcon => Data?.Icon ?? Icon;

        private bool ComputedLight => Data?.Light ?? Light;

        private string ComputedLabel => Data?.Label ?? Label;

        private bool ComputedLarge => Data?.Large ?? Large;

        private EventCallback<MouseEventArgs> ComputedOnClick => Data?.OnClick ?? OnClick;

        private bool ComputedOutlined => Data?.Outlined ?? Outlined;

        private bool ComputedPlain => Data?.Plain ?? Plain;

        private bool ComputedRounded => Data?.Rounded ?? Rounded;

        private bool ComputedSmall => Data?.Small ?? Small;

        private bool ComputedText => Data?.Text ?? Text;

        private bool ComputedTile => Data?.Tile ?? Tile;

        private string ComputedTip => Data?.Tip ?? Tip;

        private bool ComputedXSmall => Data?.XSmall ?? XSmall;

        private bool ComputedXLarge => Data?.XLarge ?? XLarge;

        private bool ShowIcon => ActionType == ActionTypes.Icon || ActionType == ActionTypes.IconLabel;

        private bool ShowLabel => ActionType == ActionTypes.Label || ActionType == ActionTypes.IconLabel;

        private bool TooltipDisabled => ActionType != ActionTypes.Icon || Disabled;

        protected override void OnInitialized()
        {
            if (Visible)
                Actions?.Register(this);
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (Visible)
                Actions?.Register(this);
            else
                Actions?.Unregister(this);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (Actions != default && _visibleChanged)
            {
                if (ButtonRef.Context == null && ButtonForwardRef != null)
                {
                    ButtonRef = ButtonForwardRef.Ref;
                }

                if (IconRef.Context == null && IconForwardRef != null)
                {
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

                _visibleChanged = false;
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

        private async Task HandleOnClick(MouseEventArgs args)
        {
            if (ComputedOnClick.HasDelegate)
            {
                _loading = true;

                try
                {
                    await ComputedOnClick.InvokeAsync();
                }
                finally
                {
                    _loading = false;
                }
            }
        }
    }
}