using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Overlay
{
    [TestClass]
    public class MOverlayTests : TestBase
    {
        [TestMethod]
        public void RenderOverlayWithDark()
        {
            //Act
            var cut = RenderComponent<MOverlay>(props =>
            {
                props.Add(overlay => overlay.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderOverlayWithLight()
        {
            //Act
            var cut = RenderComponent<MOverlay>(props =>
            {
                props.Add(overlay => overlay.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-overlay");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderOverlayWithValue()
        {
            //Act
            var cut = RenderComponent<MOverlay>(props =>
            {
                props.Add(overlay => overlay.Value, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-overlay--active");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderOverlayWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MOverlay>(props =>
            {
                props.Add(overlay => overlay.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-overlay--absolute");

            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RenderWithOpacity()
        {
            // Act
            var cut = RenderComponent<MOverlay>(props =>
            {
                props.Add(p => p.Opacity, 0.46);
            });
            var overlayDiv = cut.Find(".m-overlay__scrim");
            var style = overlayDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("background-color:#212121;border-color:#212121;opacity:0", style);
        }

        [TestMethod]
        public void RenderWithZIndex()
        {
            // Act
            var cut = RenderComponent<MOverlay>(props =>
            {
                props.Add(p => p.ZIndex, 5);
            });
            var overlayDiv = cut.Find(".m-overlay__scrim");
            var style = overlayDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("background-color:#212121;border-color:#212121;opacity:0", style);
        }
    }
}
