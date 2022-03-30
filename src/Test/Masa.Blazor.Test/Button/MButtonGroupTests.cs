using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Button
{
    [TestClass]
    public class MButtonGroupTests : TestBase
    {
        [TestMethod]
        public void RenderButtonGroupWithBorderless()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Borderless, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBorderlessClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasBorderlessClass);
        }

        [TestMethod]
        public void RenderButtonGroupWithDark()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderButtonGroupWithDense()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderButtonGroupWithGroup()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Group, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasGroupClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasGroupClass);
        }

        [TestMethod]
        public void RenderButtonGroupWithLight()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderButtonGroupWithMandatory()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Mandatory, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMandatoryClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasMandatoryClass);
        }

        [TestMethod]
        public void RenderButtonGroupWithMultiple()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Multiple, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMultipleClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasMultipleClass);
        }

        [TestMethod]
        public void RenderButtonGroupWithRounded()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderButtonGroupWithShaped()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderButtonGroupWithTile()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasTileClass);
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-btn-toggle");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
