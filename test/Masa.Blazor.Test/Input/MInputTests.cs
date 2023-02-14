using BlazorComponent;
using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Input
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
            // Act
            var cut = RenderComponent<TestInput>(props =>
            {
                props.Add(r => r.MockValidationState, "error");
            });
            var abstractProvider = cut.Instance.AbstractProvider;
            var label = abstractProvider.GetMetadata<BLabel>();

            // Assert
            Assert.AreEqual("error", cut.Instance.ValidationState);
            Assert.IsTrue(label.Attributes.ContainsKey("Color"));
            Assert.AreEqual("error", label.Attributes["Color"]);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("red")]
        [DataRow("blue")]
        [DataRow("#fff")]
        public void RenderColorComputedColorShouldBeColor(string color)
        {
            // Act
            var cut = RenderComponent<TestInput>(props =>
            {
                props.Add(r => r.Color, color);
            });

            // Assert
            Assert.AreEqual(color, cut.Instance.ComputedColor);
        }

        [TestMethod]
        public void RenderComputedColorShouldBePrimary()
        {
            // Act
            var cut = RenderComponent<TestInput>();

            // Assert
            Assert.AreEqual("primary", cut.Instance.ComputedColor);
        }

        [TestMethod]
        public void RenderIsDarkComputedColorShouldBeWhite()
        {
            // Act
            var cut = RenderComponent<TestInput>(props =>
            {
                props.Add(r => r.MockIsDark, true);
            });

            // Assert
            Assert.IsTrue(cut.Instance.IsDark);
            Assert.AreEqual("white", cut.Instance.ComputedColor);
        }

        [TestMethod]
        public void RenderIsDarkShouldBeFalse()
        {
            // Act
            var cut = RenderComponent<MInput<string>>();

            // Assert
            Assert.IsFalse(cut.Instance.IsDark);
        }

        [TestMethod]
        public void RenderDarkIsDarkShouldBeTrue()
        {
            // Act
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
