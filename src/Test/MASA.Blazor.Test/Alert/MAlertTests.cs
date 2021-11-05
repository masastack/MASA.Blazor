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
    }
}
