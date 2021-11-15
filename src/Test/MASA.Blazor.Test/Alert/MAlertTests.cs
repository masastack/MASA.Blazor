using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.Alert
{
    [TestClass]
    public class MAlertTests : TestBase
    {
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
        public void RenderAlertNoWithShaped()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Shaped, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasShapedClass);
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
        public void RenderAlertNoWithProminent()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Prominent, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasProminentClass = classes.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasProminentClass);
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
        public void RenderAlertNoWithDense()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Dense, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasDenseClass);
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
        public void RenderAlertNoWithText()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Text, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTextClass = classes.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasTextClass);
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
        public void RenderAlertNoWithOutlined()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Outlined, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
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
            var noValueClass = !classes.Contains("display:none");

            // Assert
            Assert.IsTrue(noValueClass);
        }

        [TestMethod]
        public void RenderAlertNoWithValue()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Value, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noValueClass = !classes.Contains("m-alert__wrapper");

            // Assert
            Assert.IsTrue(noValueClass);
        }

        [TestMethod]
        public void RenderAlertWithColoredBorder()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.ColoredBorder, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noColoredBorderClass = !classes.Contains("m-alert__border--has-color");

            // Assert
            Assert.IsTrue(noColoredBorderClass);
        }

        [TestMethod]
        public void RenderAlertNoWithColoredBorder()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.ColoredBorder, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noColoredBorderClass = !classes.Contains("m-alert__content");

            // Assert
            Assert.IsTrue(noColoredBorderClass);
        }

        [TestMethod]
        public void RenderAlertWithWithDark()
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
        public void RenderAlertNoWithWithDark()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasDarkClass);
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
        public void RenderAlertNoWithWithLight()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderAlertWithWithOutlined()
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
        public void RenderAlertNoWithWithOutlined()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Outlined, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderAlertWithWithProminent()
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
        public void RenderAlertNoWithWithProminent()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Prominent, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasProminentClass = classes.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasProminentClass);
        }

        [TestMethod]
        public void RenderAlertWithWithText()
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
        public void RenderAlertNoWithWithText()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Text, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTextClass = classes.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasTextClass);
        }

        [TestMethod]
        public void RenderAlertWithWithDense()
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
        public void RenderAlertNoWithWithDense()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Dense, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-alert");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderAlertWithWithShaped()
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
        public void RenderAlertNoWithWithShaped()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(alert => alert.Shaped, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-sheet");

            // Assert
            Assert.IsTrue(hasShapedClass);
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
        public void RenderWithWidth()
        {
            // Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(p => p.Width, 100);
            });
            var inputSlotDiv = cut.Find(".m-alert");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("width: 100px", style);
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
        public void RenderWithMinHeight()
        {
            // Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(p => p.MinHeight, 80);
            });
            var inputSlotDiv = cut.Find(".m-alert");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-height: 80px", style);
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
                props.Add(p => p.MinWidth, 80);
            });
            var inputSlotDiv = cut.Find(".m-alert");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-width: 80px", style);
        }

    }
}
