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
        public void RenderShouldHasNoEmptyComponent()
        {
            // Act
            var cut = RenderComponent<MInput<string>>();
            var components = cut.FindComponents<EmptyComponent>();

            // Assert
            Assert.AreEqual(0, components.Count);
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
        public void RenderShouldHasClasses()
        {
            // Act
            var cut = RenderComponent<MInput<string>>();
            var classes = cut.Instance.CssProvider.GetClass();

            // Assert
            Assert.AreEqual("m-input theme--light", classes);
        }

        [TestMethod]
        public void RenderIsDisabledShouldHasClasses()
        {
            // Act
            var cut = RenderComponent<TestInput>(props =>
            {
                props.Add(p => p.MockIsDisabled, true);
            });
            var input = cut.Instance;
            var classes = input.CssProvider.GetClass();

            // Assert
            Assert.IsTrue(input.IsDisabled);
            Assert.AreEqual("m-input m-input--is-disabled theme--light", classes);
        }

        [TestMethod]
        public void RenderHasStateShouldHasClasses()
        {
            // Act
            var cut = RenderComponent<TestInput>(props =>
            {
                props.Add(r => r.MockHasState, true);
            });
            var input = cut.Instance;
            var classes = input.CssProvider.GetClass();

            // Assert
            Assert.IsTrue(input.HasState);
            Assert.AreEqual("m-input m-input--has-state theme--light", classes);
        }

        [TestMethod]
        public void RenderHideDetailsShouldHasClasses()
        {
            // Act
            var cut = RenderComponent<TestInput>(props =>
            {
                props.Add(r => r.MockShowDetails, false);
            });
            var input = cut.Instance;
            var classes = input.CssProvider.GetClass();

            // Assert
            Assert.IsFalse(input.ShowDetails);
            Assert.AreEqual("m-input m-input--hide-details theme--light", classes);
        }

        [TestMethod]
        public void RenderValidationStateShouldPassedToLabel()
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
            Assert.IsTrue(label.Properties.ContainsKey("Color"));
            Assert.AreEqual("error", label.Properties["Color"]);
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
                props.Add(r => r.Dark, true);
            });

            // Assert
            Assert.IsTrue(cut.Instance.IsDark);
        }

        [TestMethod]
        public void RenderThemeableIsDarkShouldBeTrue()
        {
            // Arrange
            var mock = new Mock<IThemeable>();
            mock.Setup(r => r.IsDark).Returns(true);

            // Act
            var cut = RenderComponent<MInput<string>>(props =>
            {
                props.AddCascadingValue(mock.Object);
            });

            // Assert
            Assert.IsTrue(cut.Instance.IsDark);
        }

        [TestMethod]
        public void RenderLightIsDarkShouldBeFalse()
        {
            // Act
            var cut = RenderComponent<MInput<string>>(props =>
            {
                props.Add(r => r.Light, true);
            });

            // Assert
            Assert.IsFalse(cut.Instance.IsDark);
        }
    }
}
