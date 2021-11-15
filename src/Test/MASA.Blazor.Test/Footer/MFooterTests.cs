using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Footer
{
    [TestClass]
    public class MFooterTests:TestBase
    {
        [TestMethod]
        public void RenderFooterWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-footer--absolute");

            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RenderFooterNoWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Absolute, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RenderFooterWithPadless()
        {
            //Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Padless, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPadlessClass = classes.Contains("m-footer--padless");

            // Assert
            Assert.IsTrue(hasPadlessClass);
        }

        [TestMethod]
        public void RenderFooterNoWithPadless()
        {
            //Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Padless, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPadlessClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasPadlessClass);
        }

        [TestMethod]
        public void RenderFooterWithInset()
        {
            //Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Inset, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInsetClass = classes.Contains("m-footer--inset");

            // Assert
            Assert.IsTrue(hasInsetClass);
        }

        [TestMethod]
        public void RenderFooterNoWithInset()
        {
            //Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Inset, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInsetClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasInsetClass);
        }

        [TestMethod]
        public void RenderFooterWithDark()
        {
            //Act
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
        public void RenderFooterNoWithDark()
        {
            //Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderFooterWithLight()
        {
            //Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderFooterNoWithLight()
        {
            //Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderFooterWithFixed()
        {
            //Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Fixed, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedClass = classes.Contains("m-footer--fixed");

            // Assert
            Assert.IsTrue(hasFixedClass);
        }

        [TestMethod]
        public void RenderFooteNorWithFixed()
        {
            //Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Fixed, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasFixedClass);
        }

        [TestMethod]
        public void RenderFooterWithApp()
        {
            //Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.App, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppClass = classes.Contains("m-footer--fixed");

            // Assert
            Assert.IsTrue(hasAppClass);
        }

        [TestMethod]
        public void RenderFooterNoWithApp()
        {
            //Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.App, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppClass = classes.Contains("m-footer");

            // Assert
            Assert.IsTrue(hasAppClass);
        }

        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-footer");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void RenderFooterWithElevation()
        {
            //Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(footer => footer.Elevation, 24);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("elevation-2");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-footer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px;left:;right:;bottom:", style);
        }

        [TestMethod]
        public void RenderWithWidth()
        {
            // Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(p => p.Width, 100);
            });
            var inputSlotDiv = cut.Find(".m-footer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: auto;width: 100px;left:;right:;bottom:", style);
        }

        [TestMethod]
        public void RenderWithMaxWidth()
        {
            // Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(p => p.MaxWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-footer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: auto;max-width: 100px;left:;right:;bottom:", style);
        }

        [TestMethod]
        public void RenderWithMinWidth()
        {
            // Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(p => p.MinWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-footer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: auto;min-width: 100px;left:;right:;bottom:", style);
        }

        [TestMethod]
        public void RenderWithMaxHeight()
        {
            // Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(p => p.MaxHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-footer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: auto;max-height: 100px;left:;right:;bottom:", style);
        }

        [TestMethod]
        public void RenderWithMinHeight()
        {
            // Act
            var cut = RenderComponent<MFooter>(props =>
            {
                props.Add(p => p.MinHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-footer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: auto;min-height: 100px;left:;right:;bottom:", style);
        }
    }
}
