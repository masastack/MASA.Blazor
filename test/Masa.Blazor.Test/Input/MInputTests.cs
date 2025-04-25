using Bunit;

namespace Masa.Blazor.Test.Input
{
    [TestClass]
    public class MInputTests : TestBase<MInput<string>>
    {
        [TestMethod]
        public void RenderNormal()
        {
            // Arrange & Act
            var root = RenderAndGetRootElement();

            // Assert
            Assert.IsTrue(root.ClassList.Contains("m-input"));
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = Render(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-input__slot");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px;", style);
        }

        [TestMethod]
        public void RenderInputWithWithDark()
        {
            //Act
            var root = RenderAndGetRootElement(props =>
            {
                props.Add(input => input.Dark, true);
            });
            var hasDarkClass = root.ClassList.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderInputNoWithWithDark()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(input => input.Dark, false);
            });
            var hasDarkClass = cut.ClassList.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderInputWithWithLight()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(input => input.Light, true);
            });
            var hasLightClass = cut.ClassList.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
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
        public void RenderInputWithWithDense()
        {
            //Act
            var cut = RenderAndGetRootElement(props =>
            {
                props.Add(input => input.Dense, true);
            });

            // Assert
            Assert.IsTrue(cut.ClassList.Contains("m-input--dense"));
        }
    }
}
