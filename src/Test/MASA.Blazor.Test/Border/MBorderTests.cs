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
        public void RenderBorderWithWidth()
        {
            //Act
            var cut = RenderComponent<MBorder>(props =>
            {
                props.Add(border => border.Width, 24);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasWidthClass = classes.Contains("m-border");

            // Assert
            Assert.IsTrue(hasWidthClass);
        }
    }
}
