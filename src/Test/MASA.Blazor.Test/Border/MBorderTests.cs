using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Bunit;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Border
{
    [TestClass]
    public  class MBorderTests:TestBase
    {
        [TestMethod]
        public void RenderBorderWithClass()
        {
            //Act
            var cut = RenderComponent<MBorder>(props =>
            {
                string border="class";
                props.Add(border => border.Class, border);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClassClass = classes.Contains("m-border");
            // Assert
            Assert.IsTrue(hasClassClass);
        }

        [TestMethod]
        public void RenderBorderWithColor()
        {
            //Act
            var cut = RenderComponent<MBorder>(props =>
            {
                string border = "color";
                props.Add(border => border.Color, border);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColorClass = classes.Contains("m-border");
            // Assert
            Assert.IsTrue(hasColorClass);
        }

        [TestMethod]
        public void RenderBorderWithId()
        {
            //Act
            var cut = RenderComponent<MBorder>(props =>
            {
                string border = "id";
                props.Add(border => border.Id, border);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIdClass = classes.Contains("m-border");
            // Assert
            Assert.IsTrue(hasIdClass);
        }

        [TestMethod]
        public void RenderBorderWithOffset()
        {
            //Act
            var cut = RenderComponent<MBorder>(props =>
            {
                props.Add(border => border.Offset, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOffsetClass = classes.Contains("m-border");
            // Assert
            Assert.IsTrue(hasOffsetClass);
        }

        [TestMethod]
        public void RenderBorderWithRounded()
        {
            //Act
            var cut = RenderComponent<MBorder>(props =>
            {
                props.Add(border => border.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-border");
            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderBorderWithStyle()
        {
            //Act
            var cut = RenderComponent<MBorder>(props =>
            {
                string border = "style";
                props.Add(border => border.Style, border);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasStyleClass = classes.Contains("m-border");
            // Assert
            Assert.IsTrue(hasStyleClass);
        }

        [TestMethod]
        public void RenderBorderWithValue()
        {
            //Act
            var cut = RenderComponent<MBorder>(props =>
            {
                props.Add(border => border.Value, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-border");
            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderWithWidth()
        {
            // Act
            var cut = RenderComponent<MBorder>(props =>
            {
                props.Add(p => p.Width, 100);
            });
            var inputSlotDiv = cut.Find(".m-border");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("border-width:50px", style);
        }

        [TestMethod]
        public void RenderBorderWithWrapperStyle()
        {
            //Act
            var cut = RenderComponent<MBorder>(props =>
            {
                string border = "wrapperstyle";
                props.Add(border => border.WrapperStyle, border);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasWrapperStyleClass = classes.Contains("m-border");
            // Assert
            Assert.IsTrue(hasWrapperStyleClass);
        }
    }
}
