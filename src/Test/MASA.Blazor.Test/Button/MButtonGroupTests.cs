using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Button
{
    [TestClass]
    public class MButtonGroupTests:TestBase
    {
        [TestMethod]
        public void RenderButtonGroupWithActiveClass()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                string activeclass = "m-item--active";
                props.Add(buttongroup => buttongroup.ActiveClass, activeclass);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClassClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasActiveClassClass);
        }

        [TestMethod]
        public void RenderButtonGroupWithBackgroundColor()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                string color = "backdroundcolor";
                props.Add(buttongroup => buttongroup.BackgroundColor, color);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBackgroundColorClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasBackgroundColorClass);
        }

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
        public void RenderButtonGroupWithColor()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                string color = "color";
                props.Add(buttongroup => buttongroup.Color, color);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColorClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasColorClass);
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
        public void RenderButtonGroupWithMax()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                string max = "max";
                props.Add(buttongroup => buttongroup.Max, max);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMaxClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasMaxClass);
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
        public void RenderButtonGroupWithValue()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                string value = "value";
                props.Add(buttongroup => buttongroup.Value, value);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasValueClass);
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
