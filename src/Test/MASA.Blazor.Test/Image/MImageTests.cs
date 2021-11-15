using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Image
{
    [TestClass]
    public class MImageTests:TestBase
    {
        [TestMethod]
        public void RenderFooterWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MImage>(props =>
            {
                props.Add(image => image.Contain, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noFluidClass =! classes.Contains("m-image__image--cover");

            // Assert
            Assert.IsTrue(noFluidClass);
        }

        [TestMethod]
        public void RenderFooterNoWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MImage>(props =>
            {
                props.Add(image => image.Contain, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noFluidClass = !classes.Contains("m-image__image--contain");

            // Assert
            Assert.IsTrue(noFluidClass);
        }

        [TestMethod]
        public void RenderWithIconContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MImage>(props =>
            {
                props.Add(image => image.PlaceholderContent, "<span>Hello world</span>");
            });
            var imageDiv = cut.Find(".m-image__placeholder");

            // Assert
            imageDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void RenderMImageWithDark()
        {
            //Act
            var cut = RenderComponent<MImage>(props =>
            {
                props.Add(counter => counter.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderMImageNoWithDark()
        {
            //Act
            var cut = RenderComponent<MImage>(props =>
            {
                props.Add(counter => counter.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-image");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderMImageWithLight()
        {
            //Act
            var cut = RenderComponent<MImage>(props =>
            {
                props.Add(counter => counter.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderMImageNoWithLight()
        {
            //Act
            var cut = RenderComponent<MImage>(props =>
            {
                props.Add(counter => counter.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-image");

            // Assert
            Assert.IsTrue(hasLightClass);
        }
    }
}
