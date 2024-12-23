using AngleSharp.Css.Dom;
using Bunit;

namespace Masa.Blazor.Test.Overlay
{
    [TestClass]
    public class MOverlayTests : TestBase
    {
        [TestMethod]
        public void RenderOverlayWithDark()
        {
            //Act
            var cut = RenderComponent<MOverlay>(props => { props.Add(overlay => overlay.Dark, true); });
            var classes = cut.Instance.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderOverlayWithLight()
        {
            //Act
            var cut = RenderComponent<MOverlay>(props => { props.Add(overlay => overlay.Light, true); });
            var classes = cut.Instance.GetClass();
            var hasLightClass = classes.Contains("m-overlay");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderOverlayWithValue()
        {
            //Act
            var cut = RenderComponent<MOverlay>(props => { props.Add(overlay => overlay.Value, true); });
            var classes = cut.Instance.GetClass();
            var hasValueClass = classes.Contains("m-overlay--active");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderOverlayWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MOverlay>(props => { props.Add(overlay => overlay.Absolute, true); });
            var classes = cut.Instance.GetClass();
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
                props.Add(p => p.Value, true);
                props.Add(p => p.Opacity, 0.46);
            });
            var root = cut.Find(".m-overlay");
            var opacity = root.GetStyle()["--m-overlay-opacity"];
            Assert.AreEqual(opacity, "0.46");
        }

        [TestMethod]
        public void RenderWithZIndex()
        {
            // Act
            var cut = RenderComponent<MOverlay>(props => { props.Add(p => p.ZIndex, 5); });
            var root = cut.Find(".m-overlay");
            var zIndex = root.GetStyle()["z-index"];

            // Assert
            Assert.AreEqual(zIndex, "5");
        }
    }
}