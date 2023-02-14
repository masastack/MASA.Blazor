using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Snackbar
{
    [TestClass]
    public class MSnackbarTests : TestBase
    {
        [TestMethod]
        public void RenderSnackbarWithAbsolute()
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
        public void RenderSnackbarWithBottom()
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
        public void RenderSnackbarWithCentered()
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
        public void RenderSnackbarWithDark()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderSnackbarWithElevation()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Elevation, 24);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("m-snack");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-sheet");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px", style);
        }

        [TestMethod]
        public void RenderSnackbarWithLeft()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Left, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLeftClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasLeftClass);
        }

        [TestMethod]
        public void RenderSnackbarWithLight()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderWithMaxHeight()
        {
            // Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(p => p.MaxHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-sheet");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMaxWidth()
        {
            // Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(p => p.MaxWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-sheet");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-width: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinHeight()
        {
            // Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(p => p.MinHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-sheet");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinWidth()
        {
            // Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(p => p.MinWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-sheet");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-width: 100px", style);
        }

        [TestMethod]
        public void RenderSnackbarWithMultiLine()
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
        public void RenderSnackbarWithnoOutlined()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderSnackbarWithRight()
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
        public void RenderSnackbarWithRounded()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderSnackbarWithShaped()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-snack");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderSnackbarWithText()
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
        public void RenderSnackbarWithTile()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-snack");
            // Assert
            Assert.IsTrue(hasTileClass);
        }

        [TestMethod]
        public void RenderSnackbarWithTimeout()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Timeout, 5000);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTimeoutClass = classes.Contains("m-snack");

            // Assert
            Assert.IsTrue(hasTimeoutClass);
        }

        [TestMethod]
        public void RenderSnackbarWithTop()
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
        public void RenderSnackbarWithVertical()
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
        public void RenderWithWidth()
        {
            // Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(p => p.Width, 100);
            });
            var inputSlotDiv = cut.Find(".m-sheet");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("width: 100px", style);
        }

        [TestMethod]
        public void RenderSnackbarWithValue()
        {
            //Act
            var cut = RenderComponent<MSnackbar>(props =>
            {
                props.Add(snackbar => snackbar.Value, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-snack--active");
            // Assert
            Assert.IsTrue(hasValueClass);
        }
    }
}
