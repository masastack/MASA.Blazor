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

        [TestMethod]
        public void RendeMSnackbarNoWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Absolute, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RendeMSnackbarNoWithIsActive()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.IsActive, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIsActiveClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasIsActiveClass);
        }

        [TestMethod]
        public void RendeMSnackbarNoWithBottom()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Bottom, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBottomClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasBottomClass);
        }

        [TestMethod]
        public void RendeMSnackbarNoWithCentered()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Centered, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCenteredClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasCenteredClass);
        }

        [TestMethod]
        public void RendeMSnackbarNoWithLeft()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Left, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLeftClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasLeftClass);
        }

        [TestMethod]
        public void RendeMSnackbarNoWithRight()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Right, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRightClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasRightClass);
        }

        [TestMethod]
        public void RendeMSnackbarNoWithText()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Text, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTextClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasTextClass);
        }

        [TestMethod]
        public void RendeMSnackbarNoWithTop()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Top, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTopClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasTopClass);
        }

        [TestMethod]
        public void RendeMSnackbarNoWithVertical()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Vertical, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasVerticalClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasVerticalClass);
        }

        [TestMethod]
        public void RendeMSnackbarNoWithMultiLine()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.MultiLine, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMultiLineClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasMultiLineClass);
        }

        [TestMethod]
        public void RendeMSnackbarNoWithnoVertical()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Vertical, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noVerticalClass = !classes.Contains("m-snack__wrapper");
            // Assert
            Assert.IsTrue(noVerticalClass);
        }

        [TestMethod]
        public void RendeMSnackbarNoWithnoText()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Text, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noTextClass = !classes.Contains("m-btn__content");
            // Assert
            Assert.IsTrue(noTextClass);
        }

        [TestMethod]
        public void RendeMSnackbarNoWithnoOutlined()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Outlined, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noOutlinedClass = !classes.Contains("m-btn__content");
            // Assert
            Assert.IsTrue(noOutlinedClass);
        }

        [TestMethod]
        public void RendeMSnackbarNoWithnoTop()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Top, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noTopClass = !classes.Contains("m-btn__content");
            // Assert
            Assert.IsTrue(noTopClass);
        }

        //[TestMethod]
        //public void RenderAlertWithElevation()
        //{
        //    //Act
        //    var cut = RenderComponent<MSnackbar>(props =>
        //    {
        //        props.Add(snackbar => snackbar.Elevation, 24);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasElevationClass = classes.Contains("elevation-24");

        //    // Assert
        //    Assert.IsTrue(hasElevationClass);
        //}

        //[TestMethod]
        //public void RendeListWithShaped()
        //{
        //    //Act
        //    var cut = RenderComponent<MSnackbar>(props =>
        //    {
        //        props.Add(snackbar => snackbar.Shaped, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasShapedClass = classes.Contains("m-snack__content");

        //    // Assert
        //    Assert.IsTrue(hasShapedClass);
        //}
    }
}
