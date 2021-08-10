using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Presets
{
    public partial class PAction : BDomComponentBase
    {
        private ActionTypes _actionType = ActionTypes.Icon;

        private bool TooltipDisabled => _actionType != ActionTypes.Icon || Disabled;

        private int LabelByteCount => Encoding.Default.GetByteCount(Label ?? string.Empty);

        // button's left-padding and right-padding are 16px
        private double LabelPx => (LabelByteCount * GetSingleCharPx()) + (GetButtonPadding() * 2);

        // icon's width is 18px
        // icon's margin-right is 8px
        private double FullPx => LabelPx + 18 + 8;

        private string ActivatorStyle => _actionType == ActionTypes.Icon ? string.Empty : "width:100%";

        [Inject]
        public DomEventJsInterop DomEventJsInterop { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the tip.
        /// Default sets the Label as a tip if tip is null or empty.
        /// </summary>
        [Parameter]
        public string Tip { get; set; }

        /// <summary>
        /// Determine whether <see cref="PAction"/> is non-responsive.
        /// </summary>
        [Parameter]
        public bool Persistent { get; set; }

        #region Button props

        [Parameter]
        public string Color { get; set; } = "primary";

        [Parameter]
        public bool Fab { get; set; }

        [Parameter]
        public bool Plain { get; set; }

        [Parameter]
        public bool Text { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public bool Rounded { get; set; }

        [Parameter]
        public bool Depressed { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Loading { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool XSmall { get; set; }

        [Parameter]
        public bool Small { get; set; }

        [Parameter]
        public bool Large { get; set; }

        [Parameter]
        public bool XLarge { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        #endregion

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (!Persistent)
                {
                    DomEventJsInterop.ResizeObserver<Dimensions[]>(Ref, ObserveSizeChange);
                }
            }

            return base.OnAfterRenderAsync(firstRender);
        }

        private void ObserveSizeChange(Dimensions[] entries)
        {
            if (entries.Length > 0)
            {
                var dimension = entries[0];

                if (dimension.Width <= LabelPx)
                {
                    _actionType = ActionTypes.Icon;
                }
                else if (dimension.Width <= FullPx)
                {
                    _actionType = ActionTypes.Label;
                }
                else
                {
                    _actionType = ActionTypes.IconLabel;
                }

                StateHasChanged();
            }
        }

        private async Task HandleClick(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync();
            }
        }

        private double GetButtonPadding()
        {
            if (XLarge) return 23.111;
            if (Large) return 19.556;
            if (Small) return 12.444;
            if (XSmall) return 8.889;
            return 16;
        }

        private double GetSingleCharPx()
        {
            if (XLarge) return 9.72;
            if (Small) return 7.27;
            if (XSmall) return 7.09;
            return 8.46;
        }
    }

    internal enum ActionTypes
    {
        Icon = 1,

        Label,

        IconLabel
    }
}
