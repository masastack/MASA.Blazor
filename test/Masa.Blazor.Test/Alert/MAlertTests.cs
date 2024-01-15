using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Alert
{
    [TestClass]
    public class MAlertTests : TestBase
    {
        [TestMethod]
        public void RenderAlertWithColoredBorder()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.ColoredBorder, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColoredBorderClass = classes.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasColoredBorderClass);
        }

        [TestMethod]
        public void RenderAlertWithDense()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-alert--dense");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderAlertWithDark()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderAlertWithDismissible()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Dismissible, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDismissibleClass = classes.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasDismissibleClass);
        }

        [TestMethod]
        public void RenderAlertWithElevation()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Elevation, 2);
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
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-alert");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px", style);
        }

        [TestMethod]
        public void RenderAlertWithIcon()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Icon, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIconClass = classes.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasIconClass);
        }

        [TestMethod]
        public void RenderAlertWithWithLight()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderWithMaxHeight()
        {
            // Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(p => p.MaxHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-alert");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMaxWidth()
        {
            // Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(p => p.MaxWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-alert");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-width: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinWidth()
        {
            // Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(p => p.MinWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-alert");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-width: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinHeight()
        {
            // Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(p => p.MinHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-alert");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-height: 100px", style);
        }

        [TestMethod]
        public void RenderAlertWithOutlined()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-alert--outlined");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderAlertWithProminent()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Prominent, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasProminentClass = classes.Contains("m-alert--prominent");

            // Assert
            Assert.IsTrue(hasProminentClass);
        }

        [TestMethod]
        public void RenderAlertWithRounded()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("rounded");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderAlertWithShaped()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-sheet--shaped");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderAlertWithText()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Text, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTextClass = classes.Contains("m-alert--text");

            // Assert
            Assert.IsTrue(hasTextClass);
        }

        [TestMethod]
        public void RenderAlertWithTile()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("rounded");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderAlertWithValue()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Value, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-alert__content");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
