using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.Input
{
    [TestClass]
    public class MInputTests : TestBase
    {
        [TestMethod]
        public void RenderNormal()
        {
            // Arrange & Act
            var cut = RenderComponent<MInput<string>>();
            var inputDiv = cut.Find("div");

            // Assert
            Assert.AreEqual(2, inputDiv.ClassList.Length);
            Assert.IsTrue(inputDiv.ClassList.Contains("m-input"));
            Assert.IsTrue(inputDiv.ClassList.Contains("theme--light"));
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MInput<string>>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-input__slot");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px", style);
        }

        [TestMethod]
        public void RenderInputWithWithDark()
        {
            //Act
            var cut = RenderComponent<MInput<string>>(props =>
            {
                props.Add(input => input.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderInputNoWithWithDark()
        {
            //Act
            var cut = RenderComponent<MInput<string>>(props =>
            {
                props.Add(input => input.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-input");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderInputWithWithLight()
        {
            //Act
            var cut = RenderComponent<MInput<string>>(props =>
            {
                props.Add(input => input.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderInputNoWithWithLight()
        {
            //Act
            var cut = RenderComponent<MInput<string>>(props =>
            {
                props.Add(input => input.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-input");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderInputWithWithDense()
        {
            //Act
            var cut = RenderComponent<MInput<string>>(props =>
            {
                props.Add(input => input.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-input--dense");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderInputNoWithWithDense()
        {
            //Act
            var cut = RenderComponent<MInput<string>>(props =>
            {
                props.Add(input => input.Dense, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-input");

            // Assert
            Assert.IsTrue(hasLightClass);
        }
    }
}
