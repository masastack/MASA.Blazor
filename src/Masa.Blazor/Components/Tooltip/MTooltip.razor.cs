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

        /// <summary>
        /// The content that will be displayed in the tooltip.
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] [MasaApiParameter("span")] public string Tag { get; set; } = "span";

        [Parameter] public string? Transition { get; set; }

        [Parameter] public string? ContentStyle { get; set; }

        /// <summary>
        /// The text that will be displayed in the tooltip.
        /// If the <see cref="ChildContent"/> is set, this will be ignored. 
        /// </summary>
        [Parameter]
        [MasaApiParameter(ReleasedOn = "v1.9.0")]
        public string? Text { get; set; }

        protected override string DefaultAttachSelector => Permanent ? ".m-application__permanent" : ".m-application";

        private static Block _block = new("m-tooltip");
        private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
        private ModifierBuilder _contentModifierBuilder = _block.Element("content").CreateModifierBuilder();
        
        private Position ComputedPosition
        {
            get
            {
                if (Right) return Position.Right;
                if (Bottom) return Position.Bottom;
                if (Left) return Position.Left;
                return Position.Top;
            }
        }

        protected override IEnumerable<string> BuildComponentClass()
        {
            yield return _modifierBuilder
                .Add(ComputedPosition.ToString())
                .Add("attached", Attach is not { AsT1: true })
                .Build();
        }

        protected double CalculatedLeft
        {
            get
            {
                var activator = Dimensions.Activator;
                var content = Dimensions.Content;
                if (activator == null || content == null) return 0;
                
                var activatorLeft = !IsDefaultAttach ? activator.OffsetLeft : activator.Left;
                double left = 0;

                if (ComputedPosition is Position.Top or Position.Bottom)
                {
                    left = activatorLeft + (activator.Width / 2) - (content.Width / 2);
                }
                else if (ComputedPosition is Position.Left or Position.Right)
                {
                    var right = ComputedPosition == Position.Right;
                    left = activatorLeft + (right ? activator.Width : -content.Width) + (right ? 10 : -10);
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

                if (ComputedPosition is Position.Top or Position.Bottom)
                {
                    var bottom = ComputedPosition == Position.Bottom;
                    top = activatorTop + (bottom ? activator.Height : -content.Height) + (bottom ? 10 : -10);
                }
                else if (ComputedPosition is Position.Left or Position.Right)
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

    internal enum Position
    {
        Top,
        Right,
        Bottom,
        Left
    }
}