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

        //[TestMethod]
        //public void RenderFooterWithTile()
        //{
        //    //Act
        //    var cut = RenderComponent<MFooter>(props =>
        //    {
        //        props.Add(footer => footer.Tile, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasTileClass = classes.Contains("");

        //    // Assert
        //    Assert.IsTrue(hasTileClass);
        //}
    }
}
