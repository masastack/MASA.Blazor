using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Tabs
{
    [TestClass]
    public class MTabsTests : TestBase
    {
        [TestMethod]
        public void RenderTabsWithAlignWithTitle()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.AlignWithTitle, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAlignWithTitleClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasAlignWithTitleClass);
        }

        [TestMethod]
        public void RenderTabsWithAlignWithCenterActive()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.CenterActive, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCenterActiveClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasCenterActiveClass);
        }

        [TestMethod]
        public void RenderTabsWithAlignWithCentered()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.Centered, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCenteredClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasCenteredClass);
        }

        [TestMethod]
        public void RenderTabsWithAlignWithDark()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderTabsWithAlignWithFixedTabs()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.FixedTabs, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedTabsClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasFixedTabsClass);
        }

        [TestMethod]
        public void RenderTabsWithAlignWithGrow()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.Grow, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasGrowClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasGrowClass);
        }

        [TestMethod]
        public void RenderTabsWithAlignWithHideSlider()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.HideSlider, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideSliderClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasHideSliderClass);
        }

        [TestMethod]
        public void RenderTabsWithHeight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.Height, 24);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHeightClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasHeightClass);
        }

        [TestMethod]
        public void RenderTabsWithAlignWithIconsAndText()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.IconsAndText, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIconsAndTextClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasIconsAndTextClass);
        }

        [TestMethod]
        public void RenderTabsWithAlignWithLight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderTabsWithAlignWithOptional()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.Optional, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOptionalClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasOptionalClass);
        }

        [TestMethod]
        public void RenderTabsWithAlignWithRight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.Right, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRightClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasRightClass);
        }

        [TestMethod]
        public void RenderTabsWithAlignWithShowArrows()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.ShowArrows, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowArrowsClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasShowArrowsClass);
        }

        [TestMethod]
        public void RenderTabsWithAlignWithVertical()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.Vertical, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasVerticalClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasVerticalClass);
        }

        [TestMethod]
        public void RenderTabsWithSliderSize()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.SliderSize, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSliderSizeClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasSliderSizeClass);
        }
    }
}
