using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;
namespace MASA.Blazor.Test.ItemGroup
{
    [TestClass]
    public class MButtonGroupTests:TestBase
    {
        [TestMethod]
        public void RendeButtonGroupWithBorderless()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Borderless, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBorderlessClass = classes.Contains("m-btn-toggle--borderless");

            // Assert
            Assert.IsTrue(hasBorderlessClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithDense()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-btn-toggle--dense");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithGroup()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Group, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasGroupClass = classes.Contains("m-btn-toggle--group");

            // Assert
            Assert.IsTrue(hasGroupClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithRounded()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-btn-toggle--rounded");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithShaped()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-btn-toggle--shaped");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithTile()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-btn-toggle--tile");

            // Assert
            Assert.IsTrue(hasTileClass);
        }

        [TestMethod]
        public void RenderItemGroupWithDark()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasdarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasdarkClass);
        }

        [TestMethod]
        public void RenderItemGroupWithLight()
        {
            //Act
            var cut = RenderComponent<MItemGroup>(props =>
            {
                props.Add(itemgroup => itemgroup.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

    }
}
