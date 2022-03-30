using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Toolbar
{
    [TestClass]
    public class MToolbarTests : TestBase
    {
        [TestMethod]
        public void RenderToolbarWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-toolbar--absolute");
            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RenderToolbarWithBottom()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Bottom, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBottomClass = classes.Contains("m-toolbar--bottom");
            // Assert
            Assert.IsTrue(hasBottomClass);
        }

        [TestMethod]
        public void RenderToolbarWithCollapse()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Collapse, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCollapseClass = classes.Contains("m-toolbar--collapse");
            // Assert
            Assert.IsTrue(hasCollapseClass);
        }

        [TestMethod]
        public void RenderToolbarWithDark()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");
            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderToolbarWithDense()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-toolbar--dense");
            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderToolbarWithElevation()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Elevation, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("elevation-2");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderToolbarWithExtended()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Extended, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasExtendedClass = classes.Contains("m-toolbar--extended");
            // Assert
            Assert.IsTrue(hasExtendedClass);
        }

        [TestMethod]
        public void RenderToolbarWithFlat()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-toolbar--flat");
            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderToolbarWithFloating()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Floating, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFloatingClass = classes.Contains("m-toolbar--floating");
            // Assert
            Assert.IsTrue(hasFloatingClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-toolbar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px;height: 100px", style);
        }

        [TestMethod]
        public void RenderWithWidth()
        {
            // Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(p => p.Width, 100);
            });
            var inputSlotDiv = cut.Find(".m-toolbar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("width: 100px;height: 64px", style);
        }

        [TestMethod]
        public void RenderWithExtensionHeight()
        {
            // Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(p => p.ExtensionHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-toolbar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 64px", style);
        }

        [TestMethod]
        public void RenderToolbarWithLight()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Light, true);
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
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(p => p.MaxHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-toolbar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-height: 100px;height: 64px", style);
        }

        [TestMethod]
        public void RenderWithMaxWidth()
        {
            // Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(p => p.MaxWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-toolbar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-width: 100px;height: 64px", style);
        }

        [TestMethod]
        public void RenderWithMinHeight()
        {
            // Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(p => p.MinHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-toolbar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-height: 100px;height: 64px", style);
        }

        [TestMethod]
        public void RenderWithMinWidth()
        {
            // Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(p => p.MinWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-toolbar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-width: 100px;height: 64px", style);
        }

        [TestMethod]
        public void RenderToolbarWithOutlined()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-toolbar");
            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderToolbarWithProminent()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Prominent, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasProminentClass = classes.Contains("m-toolbar--prominent");
            // Assert
            Assert.IsTrue(hasProminentClass);
        }

        [TestMethod]
        public void RenderToolbarWithRounded()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-toolbar");
            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderToolbarWithShaped()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-toolbar");
            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderToolbarWithShort()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Short, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShortClass = classes.Contains("m-toolbar");
            // Assert
            Assert.IsTrue(hasShortClass);
        }

        [TestMethod]
        public void RenderToolbarWithTile()
        {
            //Act
            var cut = RenderComponent<MToolbar>(props =>
            {
                props.Add(toolbar => toolbar.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-toolbar");
            // Assert
            Assert.IsTrue(hasTileClass);
        }
    }
}
