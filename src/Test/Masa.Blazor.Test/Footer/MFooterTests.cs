using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Footer
{
    [TestClass]
    public class MFooterTests : TestBase
    {
        [TestMethod]
        public void RenderFooterWithAbsolute()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RenderFooterWithApp()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.App, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasAppClass);
        }

        [TestMethod]
        public void RenderFooterWithDark()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderFooterWithElevation()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Elevation, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("elevation-2");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderFooterWithFixed()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Fixed, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasFixedClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-footer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px", style);
        }

        [TestMethod]
        public void RenderFooterWithInset()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Inset, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInsetClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasInsetClass);
        }

        [TestMethod]
        public void RenderFooterWithLight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderWithMaxHeight()
        {
            // Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(p => p.MaxHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-footer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: auto;max-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMaxWidth()
        {
            // Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(p => p.MaxWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-footer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: auto;max-width: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinHeight()
        {
            // Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(p => p.MinHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-footer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: auto;min-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinWidth()
        {
            // Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(p => p.MinWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-footer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: auto;min-width: 100px", style);
        }

        [TestMethod]
        public void RenderWithWidth()
        {
            // Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(p => p.Width, 100);
            });
            var inputSlotDiv = cut.Find(".m-footer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: auto;width: 100px", style);
        }

        [TestMethod]
        public void RenderFooterWithPadless()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Padless, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPadlessClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasPadlessClass);
        }

        [TestMethod]
        public void RenderFooterWithRounded()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderFooterWithTile()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasTileClass);
        }
    }
}
