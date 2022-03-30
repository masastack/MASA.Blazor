using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Banner
{
    [TestClass]
    public class MBannerTests : TestBase
    {
        [TestMethod]
        public void RenderButtonWithSingleLine()
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
        public void RenderButtonNoWithSingleLine()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.SingleLine, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleLineClass = classes.Contains("m-banner");

            // Assert
            Assert.IsTrue(hasSingleLineClass);
        }

        [TestMethod]
        public void RenderButtonWithApp()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.App, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppClass = classes.Contains("m-banner--sticky");

            // Assert
            Assert.IsTrue(hasAppClass);
        }

        [TestMethod]
        public void RenderButtonNoWithApp()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.App, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppClass = classes.Contains("m-banner");

            // Assert
            Assert.IsTrue(hasAppClass);
        }

        [TestMethod]
        public void RenderButtonWithSticky()
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
        public void RenderButtonNoWithSticky()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.Sticky, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasStickyClass = classes.Contains("m-banner");

            // Assert
            Assert.IsTrue(hasStickyClass);
        }

        [TestMethod]
        public void RenderButtonWithDark()
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
        public void RenderButtonNoWithDark()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasdarkClass = classes.Contains("m-banner");

            // Assert
            Assert.IsTrue(hasdarkClass);
        }

        [TestMethod]
        public void RenderButtonWithLight()
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
        public void RenderButtonNoWithLight()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-banner");

            // Assert
            Assert.IsTrue(hasLightClass);
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
        public void RenderButtonNoWithValue()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.Value, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noValueClass = !classes.Contains("m-banner__wrapper");

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
    }
}
