using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Tooltip
{
    [TestClass]
    public class MTooltipTests : TestBase
    {
        [TestMethod]
        public void RenderTooltipWithAbsolute()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-tooltip");
            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RenderTooltipWithAllowOverflow()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.AllowOverflow, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAllowOverflowClass = classes.Contains("m-tooltip");
            // Assert
            Assert.IsTrue(hasAllowOverflowClass);
        }

        [TestMethod]
        public void RenderTooltipWithBottom()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.Bottom, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBottomClass = classes.Contains("m-tooltip");
            // Assert
            Assert.IsTrue(hasBottomClass);
        }

        [TestMethod]
        public void RenderTooltipWithCloseDelay()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.CloseDelay, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCloseDelayClass = classes.Contains("m-tooltip");

            // Assert
            Assert.IsTrue(hasCloseDelayClass);
        }

        [TestMethod]
        public void RenderTooltipWithDisabled()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-tooltip");
            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderTooltipWithLeft()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.Left, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLeftClass = classes.Contains("m-tooltip");
            // Assert
            Assert.IsTrue(hasLeftClass);
        }

        [TestMethod]
        public void RenderTooltipWithMaxWidth()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.MaxWidth, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMaxWidthClass = classes.Contains("m-tooltip");

            // Assert
            Assert.IsTrue(hasMaxWidthClass);
        }

        [TestMethod]
        public void RenderTooltipWithMinWidth()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.MinWidth, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMinWidthClass = classes.Contains("m-tooltip");

            // Assert
            Assert.IsTrue(hasMinWidthClass);
        }

        [TestMethod]
        public void RenderTooltipWithNudgeBottom()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.NudgeBottom, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNudgeBottomClass = classes.Contains("m-tooltip");

            // Assert
            Assert.IsTrue(hasNudgeBottomClass);
        }

        [TestMethod]
        public void RenderTooltipWithNudgeLeft()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.NudgeLeft, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNudgeLeftClass = classes.Contains("m-tooltip");

            // Assert
            Assert.IsTrue(hasNudgeLeftClass);
        }

        [TestMethod]
        public void RenderTooltipWithNudgeRight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.NudgeRight, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNudgeRightClass = classes.Contains("m-tooltip");

            // Assert
            Assert.IsTrue(hasNudgeRightClass);
        }

        [TestMethod]
        public void RenderTooltipWithNudgeTop()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.NudgeTop, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNudgeTopClass = classes.Contains("m-tooltip");

            // Assert
            Assert.IsTrue(hasNudgeTopClass);
        }

        [TestMethod]
        public void RenderTooltipWithNudgeWidth()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.NudgeWidth, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNudgeWidthClass = classes.Contains("m-tooltip");

            // Assert
            Assert.IsTrue(hasNudgeWidthClass);
        }

        [TestMethod]
        public void RenderTooltipWithOffsetOverflow()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.OffsetOverflow, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOffsetOverflowClass = classes.Contains("m-tooltip");
            // Assert
            Assert.IsTrue(hasOffsetOverflowClass);
        }

        [TestMethod]
        public void RenderTooltipWithOpenDelay()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.OpenDelay, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOpenDelayClass = classes.Contains("m-tooltip");

            // Assert
            Assert.IsTrue(hasOpenDelayClass);
        }

        [TestMethod]
        public void RenderTooltipWithOpenOnClick()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.OpenOnClick, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOpenOnClickClass = classes.Contains("m-tooltip");
            // Assert
            Assert.IsTrue(hasOpenOnClickClass);
        }

        [TestMethod]
        public void RenderTooltipWithOpenOnFocus()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.OpenOnFocus, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOpenOnFocusClass = classes.Contains("m-tooltip");
            // Assert
            Assert.IsTrue(hasOpenOnFocusClass);
        }

        [TestMethod]
        public void RenderTooltipWithOpenOnHover()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.OpenOnHover, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOpenOnHoverClass = classes.Contains("m-tooltip");
            // Assert
            Assert.IsTrue(hasOpenOnHoverClass);
        }

        [TestMethod]
        public void RenderTooltipWithPositionX()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.PositionX, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPositionXClass = classes.Contains("m-tooltip");

            // Assert
            Assert.IsTrue(hasPositionXClass);
        }

        [TestMethod]
        public void RenderTooltipWithPositionY()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.PositionY, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPositionYClass = classes.Contains("m-tooltip");

            // Assert
            Assert.IsTrue(hasPositionYClass);
        }

        [TestMethod]
        public void RenderTooltipWithRight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.Right, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRightClass = classes.Contains("m-tooltip");
            // Assert
            Assert.IsTrue(hasRightClass);
        }

        [TestMethod]
        public void RenderTooltipWithTop()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.Top, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTopClass = classes.Contains("m-tooltip");
            // Assert
            Assert.IsTrue(hasTopClass);
        }

        [TestMethod]
        public void RenderTooltipWithZIndex()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTooltip>(props =>
            {
                props.Add(tooltip => tooltip.ZIndex, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasZIndexClass = classes.Contains("m-tooltip");

            // Assert
            Assert.IsTrue(hasZIndexClass);
        }
    }
}
