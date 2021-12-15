using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Bunit;

namespace MASA.Blazor.Test.Tabs
{
    [TestClass]
    public class MTabsTests:TestBase
    {
        [TestMethod]
        public void RenderTabsWithTitle()
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
        public void RenderTabsWithCenterActive()
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
        public void RenderTabsWithCentered()
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
        public void RenderTabsWithDark()
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
        public void RenderTabsWithFixedTabs()
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
        public void RenderTabsWithGrow()
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
        public void RenderTabsWithHideSlider()
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
        public void RenderTabsWithIconsAndText()
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
        public void RenderTabsWithLight()
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
        public void RenderTabsWithOptional()
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
        public void RenderTabsWithRight()
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
        public void RenderTabsWithShowArrows()
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
        public void RenderTabsWithVertical()
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

        [TestMethod]
        public void RenderTabsWithActiveClass()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                string active = "active-class";
                props.Add(tabs => tabs.ActiveClass, active);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClassClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasActiveClassClass);
        }

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
        public void RenderTabsWithBackgroundColor()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                string color = "color";
                props.Add(tabs => tabs.BackgroundColor, color);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBackgroundColorClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasBackgroundColorClass);
        }

        [TestMethod]
        public void RenderTabsWithColor()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                string color = "color";
                props.Add(tabs => tabs.Color, color);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColorClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasColorClass);
        }

        [TestMethod]
        public void RenderTabsWithMobileBreakpoint()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                props.Add(tabs => tabs.MobileBreakpoint, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMobileBreakpointClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasMobileBreakpointClass);
        }

        [TestMethod]
        public void RenderTabsWithNextIcon()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                string icon = "mdi-star";
                props.Add(tabs => tabs.NextIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNextIconClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasNextIconClass);
        }

        [TestMethod]
        public void RenderTabsWithPrevIcon()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                string icon = "mdi-star";
                props.Add(tabs => tabs.PrevIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPrevIconClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasPrevIconClass);
        }

        [TestMethod]
        public void RenderTabsWithSliderColor()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                string icon = "mdi-star";
                props.Add(tabs => tabs.SliderColor, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSliderColorClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasSliderColorClass);
        }

        [TestMethod]
        public void RenderTabsWithValue()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MTabs>(props =>
            {
                string icon = "mdi-star";
                props.Add(tabs => tabs.Value, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-tabs");

            // Assert
            Assert.IsTrue(hasValueClass);
        }
    }
}
