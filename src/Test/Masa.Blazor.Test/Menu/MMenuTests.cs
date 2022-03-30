using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Menu
{
    [TestClass]
    public class MMenuTests : TestBase
    {
        [TestMethod]
        public void RenderMenuWithAbsolute()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RenderMenuWithAllowOverflow()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.AllowOverflow, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAllowOverflowClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasAllowOverflowClass);
        }

        [TestMethod]
        public void RenderMenuWithAuto()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.Auto, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAutoClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasAutoClass);
        }

        [TestMethod]
        public void RenderMenuWithBottom()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.Bottom, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBottomClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasBottomClass);
        }

        [TestMethod]
        public void RenderMenuWithCloseDelay()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.CloseDelay, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCloseDelayClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasCloseDelayClass);
        }

        [TestMethod]
        public void RenderMenuWithCloseOnClick()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.CloseOnClick, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCloseOnClickClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasCloseOnClickClass);
        }

        [TestMethod]
        public void RenderMenuWithCloseOnContentClick()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.CloseOnContentClick, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCloseOnContentClickClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasCloseOnContentClickClass);
        }

        [TestMethod]
        public void RenderMenuWithDark()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderMenuWithDisableKeys()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.DisableKeys, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisableKeysClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasDisableKeysClass);
        }

        [TestMethod]
        public void RenderMenuWithDisabled()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderMenuWithLeft()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.Left, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLeftClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasLeftClass);
        }

        [TestMethod]
        public void RenderMenuWithMaxHeight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.MaxHeight, 100);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNudgeBottomClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasNudgeBottomClass);
        }

        [TestMethod]
        public void RenderMenuWithMaxWidth()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.MaxWidth, 100);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNudgeBottomClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasNudgeBottomClass);
        }

        [TestMethod]
        public void RenderMenuWithMinWidth()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.MinWidth, 100);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNudgeBottomClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasNudgeBottomClass);
        }

        [TestMethod]
        public void RenderMenuWithNudgeBottom()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.NudgeBottom, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNudgeBottomClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasNudgeBottomClass);
        }

        [TestMethod]
        public void RenderMenuWithNudgeLeft()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.NudgeLeft, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNudgeLeftClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasNudgeLeftClass);
        }

        [TestMethod]
        public void RenderMenuWithNudgeRight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.NudgeRight, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNudgeRightClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasNudgeRightClass);
        }

        [TestMethod]
        public void RenderMenuWithNudgeTop()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.NudgeTop, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNudgeTopClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasNudgeTopClass);
        }

        [TestMethod]
        public void RenderMenuWithNudgeWidth()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.NudgeWidth, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNudgeWidthClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasNudgeWidthClass);
        }

        [TestMethod]
        public void RenderMenuWithOffsetOverflow()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.OffsetOverflow, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOffsetOverflowClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasOffsetOverflowClass);
        }

        [TestMethod]
        public void RenderMenuWithOffsetX()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.OffsetX, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOffsetXClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasOffsetXClass);
        }

        [TestMethod]
        public void RenderMenuWithOpenDelay()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.OpenDelay, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOpenDelayClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasOpenDelayClass);
        }

        [TestMethod]
        public void RenderMenuWithOffsetY()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.OffsetY, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOffsetYClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasOffsetYClass);
        }

        [TestMethod]
        public void RenderMenuWithOpenOnClick()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.OpenOnClick, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOpenOnClickClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasOpenOnClickClass);
        }

        [TestMethod]
        public void RenderMenuWithOpenOnFocus()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.OpenOnFocus, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOpenOnFocusClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasOpenOnFocusClass);
        }

        [TestMethod]
        public void RenderMenuWithOpenOnHover()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.OpenOnHover, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOpenOnHoverClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasOpenOnHoverClass);
        }

        [TestMethod]
        public void RenderMenuWithPositionX()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.PositionX, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPositionXClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasPositionXClass);
        }

        [TestMethod]
        public void RenderMenuWithPositionY()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.PositionY, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPositionYClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasPositionYClass);
        }

        [TestMethod]
        public void RenderMenuWithRight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.Right, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRightClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasRightClass);
        }

        [TestMethod]
        public void RenderMenuWithRounded()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderMenuWithTile()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasTileClass);
        }

        [TestMethod]
        public void RenderMenuWithTop()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.Top, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTopClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasTopClass);
        }

        [TestMethod]
        public void RenderMenuWithZIndex()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MMenu>(props =>
            {
                props.Add(menu => menu.ZIndex, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPositionYClass = classes.Contains("m-menu");

            // Assert
            Assert.IsTrue(hasPositionYClass);
        }
    }
}
