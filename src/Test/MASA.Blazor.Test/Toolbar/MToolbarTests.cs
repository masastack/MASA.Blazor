using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Bunit;

namespace MASA.Blazor.Test.Toolbar
{
    [TestClass]
    public class MToolbarTests:TestBase
    {
        [TestMethod]
        public void RendeMToolbarWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-toolbar--absolute");
            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RendeMToolbarWithBottom()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Bottom, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBottomClass = classes.Contains("m-toolbar--bottom");
            // Assert
            Assert.IsTrue(hasBottomClass);
        }

        [TestMethod]
        public void RendeMToolbarWithCollapse()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Collapse, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCollapseClass = classes.Contains("m-toolbar--collapse");
            // Assert
            Assert.IsTrue(hasCollapseClass);
        }

        [TestMethod]
        public void RendeMToolbarWithDense()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-toolbar--dense");
            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RendeMToolbarWithFlat()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-toolbar--flat");
            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RendeMToolbarWithFloating()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Floating, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFloatingClass = classes.Contains("m-toolbar--floating");
            // Assert
            Assert.IsTrue(hasFloatingClass);
        }

        [TestMethod]
        public void RendeMToolbarWithProminent()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Prominent, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasProminentClass = classes.Contains("m-toolbar--prominent");
            // Assert
            Assert.IsTrue(hasProminentClass);
        }

        [TestMethod]
        public void RendeMToolbarWithExtended()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Extended, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasExtendedClass = classes.Contains("m-toolbar--extended");
            // Assert
            Assert.IsTrue(hasExtendedClass);
        }

        //[TestMethod]
        //public void RenderWithChildContentt()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MToolbarItems>(props =>
        //    {
        //        props.Add(list => list.ChildContent, "<span>Hello world</span>");
        //    });
        //    var contentDiv = cut.Find(".m-toolbar__items");

        //    // Assert
        //    contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        //}


    }
}
