using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Banner
{
    [TestClass]
    public class MBannerTests:TestBase
    {
        [TestMethod]
        public void RenderBannerWithApp()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.App, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppClass = classes.Contains("m-banner");

            // Assert
            Assert.IsTrue(hasAppClass);
        }

        [TestMethod]
        public void RenderBannerWithColor()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                string color = "color";
                props.Add(banner => banner.Color, color);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColorClass = classes.Contains("m-banner");

            // Assert
            Assert.IsTrue(hasColorClass);
        }

        [TestMethod]
        public void RenderBannerWithDark()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasdarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasdarkClass);
        }

        [TestMethod]
        public void RenderBannerWithElevation()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.Elevation, 24);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("elevation");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderBannerWithIcon()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                string icon = "mdi-star";
                props.Add(banner => banner.Icon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIconClass = classes.Contains("m-banner");

            // Assert
            Assert.IsTrue(hasIconClass);
        }

        [TestMethod]
        public void RenderBannerWithIconColor()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                string icon = "mdi-star";
                props.Add(banner => banner.IconColor, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIconColorClass = classes.Contains("m-banner");

            // Assert
            Assert.IsTrue(hasIconColorClass);
        }

        [TestMethod]
        public void RenderBannerWithLight()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderBannerWithSingleLine()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.SingleLine, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleLineClass = classes.Contains("m-banner--single-line");

            // Assert
            Assert.IsTrue(hasSingleLineClass);
        }

        [TestMethod]
        public void RenderBannerWithSticky()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.Sticky, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasStickyClass = classes.Contains("m-banner--sticky");

            // Assert
            Assert.IsTrue(hasStickyClass);
        }

        [TestMethod]
        public void RenderButtonWithValue()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.Value, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noValueClass = !classes.Contains("display:none");

            // Assert
            Assert.IsTrue(noValueClass);
        }

        [TestMethod]
        public void RenderWithIconContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.IconContent, "<span>Hello world</span>");
            });
            var bannerDiv = cut.Find(".m-banner__icon");

            // Assert
            bannerDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void RenderWithActionsContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.ActionsContent, context => "<span>Hello world</span>");
            });
            var bannerDiv = cut.Find(".m-banner__actions");

            // Assert
            bannerDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.ChildContent, "<span>Hello world</span>");
            });
            var bannerDiv = cut.Find(".m-banner__text");

            // Assert
            bannerDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        
    }
}
