using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Alert
{
    [TestClass]
    public class MAlertTests : TestBase
    {
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
        public void RenderButtonWithShaped()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(button => button.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-sheet--shaped");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderButtonWithProminent()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(button => button.Prominent, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasProminentClass = classes.Contains("m-alert--prominent");

            // Assert
            Assert.IsTrue(hasProminentClass);
        }

        [TestMethod]
        public void RenderButtonWithDense()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(button => button.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-alert--dense");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderButtonWithText()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(button => button.Text, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTextClass = classes.Contains("m-alert--text");

            // Assert
            Assert.IsTrue(hasTextClass);
        }

        [TestMethod]
        public void RenderButtonWithOutlined()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(button => button.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-alert--outlined");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderButtonWithValue()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(button => button.Value, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noValueClass = !classes.Contains("display:none");

            // Assert
            Assert.IsTrue(noValueClass);
        }

        [TestMethod]
        public void RenderButtonWithColoredBorder()
        {
            //Act
            var cut = RenderComponent<MAlert>(props =>
            {
                props.Add(border => border.ColoredBorder, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noColoredBorderClass = !classes.Contains("ColoredBorder");

            // Assert
            Assert.IsTrue(noColoredBorderClass);
        }

    }
}
