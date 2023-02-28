using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Sheet
{
    [TestClass]
    public class MSheetTests : TestBase
    {
        [TestMethod]
        public void RenderSheetWithDark()
        {
            //Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(sheet => sheet.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderSheetWithElevation()
        {
            //Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(sheet => sheet.Elevation, 24);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("elevation-2");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-sheet");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px", style);
        }

        [TestMethod]
        public void RenderSheetWithLight()
        {
            //Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(sheet => sheet.Light, true);
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
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(p => p.MaxHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-sheet");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMaxWidth()
        {
            // Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(p => p.MaxWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-sheet");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-width: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinHeight()
        {
            // Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(p => p.MinHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-sheet");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinWidth()
        {
            // Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(p => p.MinWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-sheet");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-width: 100px", style);
        }

        [TestMethod]
        public void RenderSheetWithOutlined()
        {
            //Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(sheet => sheet.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-sheet--outlined");
            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderSheetWithRounded()
        {
            //Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(sheet => sheet.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-sheet");
            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderSheetWithShaped()
        {
            //Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(sheet => sheet.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-sheet--shaped");
            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderSheetWithTile()
        {
            //Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(sheet => sheet.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-sheet");
            // Assert
            Assert.IsTrue(hasTileClass);
        }

        [TestMethod]
        public void RenderWithWidth()
        {
            // Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(p => p.Width, 100);
            });
            var inputSlotDiv = cut.Find(".m-sheet");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("width: 100px", style);
        }

        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MSheet>(props =>
            {
                props.Add(sheet => sheet.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-sheet");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }


    }
}
