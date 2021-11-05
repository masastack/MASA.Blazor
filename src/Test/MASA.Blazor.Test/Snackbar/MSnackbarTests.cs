using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MASA.Blazor.Test.Snackbar
{
    [TestClass]
    public class MSnackbarTests:TestBase
    {
        [TestMethod]
        public void RendeMSnackbarWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-snack--absolute");
            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RendeMSnackbarWithIsActive()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.IsActive, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIsActiveClass = classes.Contains("m-snack--active");
            // Assert
            Assert.IsTrue(hasIsActiveClass);
        }

        [TestMethod]
        public void RendeMSnackbarWithBottom()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Bottom, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBottomClass = classes.Contains("m-snack--bottom");
            // Assert
            Assert.IsTrue(hasBottomClass);
        }

        [TestMethod]
        public void RendeMSnackbarWithCentered()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Centered, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCenteredClass = classes.Contains("m-snack--centered");
            // Assert
            Assert.IsTrue(hasCenteredClass);
        }

        [TestMethod]
        public void RendeMSnackbarWithLeft()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Left, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLeftClass = classes.Contains("m-snack--left");
            // Assert
            Assert.IsTrue(hasLeftClass);
        }

        [TestMethod]
        public void RendeMSnackbarWithRight()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Right, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRightClass = classes.Contains("m-snack--right");
            // Assert
            Assert.IsTrue(hasRightClass);
        }

        [TestMethod]
        public void RendeMSnackbarWithText()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Text, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTextClass = classes.Contains("m-snack--text");
            // Assert
            Assert.IsTrue(hasTextClass);
        }

        [TestMethod]
        public void RendeMSnackbarWithTop()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Top, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTopClass = classes.Contains("m-snack--top");
            // Assert
            Assert.IsTrue(hasTopClass);
        }

        [TestMethod]
        public void RendeMSnackbarWithVertical()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Vertical, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasVerticalClass = classes.Contains("m-snack--vertical");
            // Assert
            Assert.IsTrue(hasVerticalClass);
        }

        [TestMethod]
        public void RendeMSnackbarWithMultiLine()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.MultiLine, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMultiLineClass = classes.Contains("m-snack--multi-line");
            // Assert
            Assert.IsTrue(hasMultiLineClass);
        }

        [TestMethod]
        public void RendeMSnackbarWithnoVertical()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Vertical, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noVerticalClass =! classes.Contains("m-snack--multi-line");
            // Assert
            Assert.IsTrue(noVerticalClass);
        }

        [TestMethod]
        public void RendeMSnackbarWithnoText()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Text, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noTextClass = !classes.Contains("m-snack--has-background");
            // Assert
            Assert.IsTrue(noTextClass);
        }

        [TestMethod]
        public void RendeMSnackbarWithnoOutlined()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noOutlinedClass = !classes.Contains("m-snack--has-background");
            // Assert
            Assert.IsTrue(noOutlinedClass);
        }

        [TestMethod]
        public void RendeMSnackbarWithnoTop()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Top, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noTopClass = !classes.Contains("m-snack--bottom");
            // Assert
            Assert.IsTrue(noTopClass);
        }

        //[TestMethod]
        //public void RenderWithChildContentt()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MSnackbar>(props =>
        //    {
        //        props.Add(list => list.ChildContent, "<span>Hello world</span>");
        //    });
        //    var contentDiv = cut.Find(".m-snackbar");

        //    // Assert
        //    contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        //}
    }
}
