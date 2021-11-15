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
                props.Add(progresslinear => progresslinear.Absolute, true);
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
                props.Add(progresslinear => progresslinear.Fixed, true);
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
                props.Add(progresslinear => progresslinear.Query, true);
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
                props.Add(progresslinear => progresslinear.Rounded, true);
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
                props.Add(progresslinear => progresslinear.Striped, true);
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
                props.Add(progresslinear => progresslinear.IsVisible, true);
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
                props.Add(progresslinear => progresslinear.Dark, true);
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
                props.Add(progresslinear => progresslinear.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderProgressLinearWithActive()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Active, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClass = classes.Contains("m-progress-linear");

            // Assert
            Assert.IsTrue(hasActiveClass);
        }

        [TestMethod]
        public void RenderProgressLinearWithStream()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Stream, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasStreamClass = classes.Contains("m-progress-linear");

            // Assert
            Assert.IsTrue(hasStreamClass);
        }

        [TestMethod]
        public void RenderProgressLinearWithReverse()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Reverse, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReverseClass = classes.Contains("m-progress-linear");

            // Assert
            Assert.IsTrue(hasReverseClass);
        }

        [TestMethod]
        public void RendeMProgressLinearNoWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Absolute, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-progress-linear");
            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }



        [TestMethod]
        public void RendeMProgressLinearNoWithFixed()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Fixed, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedClass = classes.Contains("m-progress-linear");
            // Assert
            Assert.IsTrue(hasFixedClass);
        }

        [TestMethod]
        public void RendeMProgressLinearNoWithQuery()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Query, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasQueryClass = classes.Contains("m-progress-linear");
            // Assert
            Assert.IsTrue(hasQueryClass);
        }

        [TestMethod]
        public void RendeMProgressLinearNoWithRounded()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Rounded, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-progress-linear");
            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RendeMProgressLinearNoWithStriped()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Striped, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasStripedClass = classes.Contains("m-progress-linear");
            // Assert
            Assert.IsTrue(hasStripedClass);
        }

        [TestMethod]
        public void RendeMProgressLinearNoWithIsVisible()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.IsVisible, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIsVisibleClass = classes.Contains("m-progress-linear");
            // Assert
            Assert.IsTrue(hasIsVisibleClass);
        }

        [TestMethod]
        public void RenderProgressLinearNoWithDark()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-progress-linear");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderProgressLinearNoWithLight()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-progress-linear");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderProgressLinearNoWithActive()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Active, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClass = classes.Contains("m-progress-linear");

            // Assert
            Assert.IsTrue(hasActiveClass);
        }

        [TestMethod]
        public void RenderProgressLinearNoWithStream()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Stream, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasStreamClass = classes.Contains("m-progress-linear");

            // Assert
            Assert.IsTrue(hasStreamClass);
        }

        [TestMethod]
        public void RenderProgressLinearNoWithReverse()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Reverse, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReverseClass = classes.Contains("m-progress-linear");

            // Assert
            Assert.IsTrue(hasReverseClass);
        }
    }
}
