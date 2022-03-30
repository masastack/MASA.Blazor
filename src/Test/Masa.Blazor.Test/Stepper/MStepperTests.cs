using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Masa.Blazor.Test.Stepper
{
    [TestClass]
    public class MStepperTests : TestBase
    {

        [TestMethod]
        public void RenderStepperWithAltLabels()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.AltLabels, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAltLabelsClass = classes.Contains("m-stepper--alt-labels");
            // Assert
            Assert.IsTrue(hasAltLabelsClass);
        }

        [TestMethod]
        public void RenderStepperWithDark()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");
            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderStepperWithElevation()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.Elevation, 24);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("m-stepper");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderStepperWithFlat()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-stepper--flat");
            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-stepper");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px", style);
        }

        [TestMethod]
        public void RenderStepperWithLight()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.Light, true);
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
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(p => p.MaxHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-stepper");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMaxWidth()
        {
            // Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(p => p.MaxWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-stepper");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-width: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinHeight()
        {
            // Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(p => p.MinHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-stepper");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinWidth()
        {
            // Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(p => p.MinWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-stepper");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-width: 100px", style);
        }

        [TestMethod]
        public void RenderStepperWithNonLinear()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.NonLinear, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNonLinearClass = classes.Contains("m-stepper--non-linear");
            // Assert
            Assert.IsTrue(hasNonLinearClass);
        }

        [TestMethod]
        public void RenderStepperWithOutlined()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-stepper");
            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderStepperWithRounded()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-stepper");
            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderStepperWithShaped()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-stepper");
            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderStepperWithTile()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-stepper");
            // Assert
            Assert.IsTrue(hasTileClass);
        }

        [TestMethod]
        public void RenderStepperWithVertical()
        {
            //Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.Vertical, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasVerticalClass = classes.Contains("m-stepper--vertical");
            // Assert
            Assert.IsTrue(hasVerticalClass);
        }

        [TestMethod]
        public void RenderWithWidth()
        {
            // Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(p => p.Width, 100);
            });
            var inputSlotDiv = cut.Find(".m-stepper");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("width: 100px", style);
        }

        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(stepper => stepper.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-stepper");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
