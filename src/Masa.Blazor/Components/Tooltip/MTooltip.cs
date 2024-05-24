using Masa.Blazor.Mixins.Menuable;

namespace Masa.Blazor
{
    public partial class MTooltip : MMenuable
    {
        public MTooltip()
        {
            OpenOnHover = true;
            OpenOnFocus = true;
        }

        [Parameter] public string? Color { get; set; }

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] [MasaApiParameter("span")] public string Tag { get; set; } = "span";

        [Parameter] public string? Transition { get; set; }

        [Parameter] public string? ContentStyle { get; set; }

        protected override string DefaultAttachSelector => Permanent ? ".m-application__permanent" : ".m-application";

        private Block _block = new("m-tooltip");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return _block.Modifier(Top)
                .And(Right)
                .And(Bottom)
                .And(Left)
                .And("attached", Attach is not { AsT1: true })
                .GenerateCssClasses();
        }

        protected double CalculatedLeft
        {
            get
            {
                var activator = Dimensions.Activator;
                var content = Dimensions.Content;
                if (activator == null || content == null) return 0;

                var unknown = !Bottom && !Left && !Top && !Right;
                var activatorLeft = !IsDefaultAttach ? activator.OffsetLeft : activator.Left;
                double left = 0;

                if (Top || Bottom || unknown)
                {
                    left = activatorLeft + (activator.Width / 2) - (content.Width / 2);
                }
                else if (Left || Right)
                {
                    left = activatorLeft + (Right ? activator.Width : -content.Width) + (Right ? 10 : -10);
                }

                if (NudgeLeft != null)
                {
                    var (_, nudgeLeft) = NudgeLeft.TryGetNumber();
                    left -= nudgeLeft;
                }

                if (NudgeRight != null)
                {
                    var (_, nudgeRight) = NudgeRight.TryGetNumber();
                    left += nudgeRight;
                }

                return CalcXOverflow(left, content.Width);
            }
        }

        protected double CalculatedTop
        {
            get
            {
                var activator = Dimensions.Activator;
                var content = Dimensions.Content;
                if (activator == null || content == null) return 0;

                var activatorTop = !IsDefaultAttach ? activator.OffsetTop : activator.Top;
                double top = 0;

                if (Top || Bottom)
                {
                    top = activatorTop + (Bottom ? activator.Height : -content.Height) + (Bottom ? 10 : -10);
                }
                else if (Left || Right)
                {
                    top = activatorTop + (activator.Height / 2) - (content.Height / 2);
                }

                if (NudgeTop != null)
                {
                    var (_, nudgeTop) = NudgeTop.TryGetNumber();
                    top -= nudgeTop;
                }

                if (NudgeBottom != null)
                {
                    var (_, nudgeBottom) = NudgeBottom.TryGetNumber();
                    top += nudgeBottom;
                }

                if (IsDefaultAttach)
                {
                    top += PageYOffset;
                }

                return CalcYOverflow(top);
            }
        }
    }
}