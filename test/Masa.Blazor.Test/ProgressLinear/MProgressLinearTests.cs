using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.ProgressLinear
{
    [TestClass]
    public class MProgressLinearTests : TestBase
    {
        [TestMethod]
        public void RenderProgressLinearWithAbsolute()
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
        public void RenderProgressLinearWithBackgroundOpacity()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.BackgroundOpacity, 0.3);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBackgroundOpacityClass = classes.Contains("m-progress-linear");

            // Assert
            Assert.IsTrue(hasBackgroundOpacityClass);
        }

        [TestMethod]
        public void RenderProgressLinearWithBottom()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Bottom, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBottomClass = classes.Contains("m-progress-linear");
            // Assert
            Assert.IsTrue(hasBottomClass);
        }

        [TestMethod]
        public void RenderProgressLinearWithBufferValue()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.BufferValue, 100);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBufferValueClass = classes.Contains("m-progress-linear");

            // Assert
            Assert.IsTrue(hasBufferValueClass);
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
        public void RenderProgressLinearWithFixed()
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
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(p => p.Height, 4);
            });
            var inputSlotDiv = cut.Find(".m-progress-linear");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height:4px", style);
        }

        [TestMethod]
        public void RenderProgressLinearWithIndeterminate()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Indeterminate, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIndeterminateClass = classes.Contains("m-progress-linear");
            // Assert
            Assert.IsTrue(hasIndeterminateClass);
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
        public void RenderProgressLinearWithQuery()
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
        public void RenderProgressLinearWithRounded()
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
        public void RenderProgressLinearWithStriped()
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
        public void RenderProgressLinearWithTop()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Top, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTopClass = classes.Contains("m-progress-linear");
            // Assert
            Assert.IsTrue(hasTopClass);
        }

        [TestMethod]
        public void RenderProgressLinearWithValue()
        {
            //Act
            var cut = RenderComponent<MProgressLinear>(props =>
            {
                props.Add(progresslinear => progresslinear.Value, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-progress-linear");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderWithChildContent()
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










    }
}
