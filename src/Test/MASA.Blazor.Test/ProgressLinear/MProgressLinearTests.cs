using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Bunit;

namespace MASA.Blazor.Test.ProgressLinear
{
    [TestClass]
    public class MProgressLinearTests:TestBase
    {
        [TestMethod]
        public void RendeMProgressLinearWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progress => progress.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-progress-linear--absolute");
            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RendeMProgressLinearWithFixed()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progress => progress.Fixed, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedClass = classes.Contains("m-progress-linear--fixed");
            // Assert
            Assert.IsTrue(hasFixedClass);
        }

        [TestMethod]
        public void RendeMProgressLinearWithQuery()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progress => progress.Query, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasQueryClass = classes.Contains("m-progress-linear--query");
            // Assert
            Assert.IsTrue(hasQueryClass);
        }

        [TestMethod]
        public void RendeMProgressLinearWithRounded()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progress => progress.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-progress-linear--rounded");
            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RendeMProgressLinearWithStriped()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progress => progress.Striped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasStripedClass = classes.Contains("m-progress-linear--striped");
            // Assert
            Assert.IsTrue(hasStripedClass);
        }

        [TestMethod]
        public void RendeMProgressLinearWithIsVisible()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progress => progress.IsVisible, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIsVisibleClass = classes.Contains("m-progress-linear--visible");
            // Assert
            Assert.IsTrue(hasIsVisibleClass);
        }

        [TestMethod]
        public void RenderWithIconContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.ChildContent, context => "<span>Hello world</span>");
            });
            var progresslinearDiv = cut.Find(".m-progress-linear__content");

            // Assert
            progresslinearDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void RenderProgressLinearWithDark()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(counter => counter.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderProgressLinearWithLight()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(counter => counter.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }
    }
}
