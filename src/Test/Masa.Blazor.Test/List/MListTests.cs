using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.List
{
    [TestClass]
    public class MListTests : TestBase
    {
        [TestMethod]
        public void RenderListWithDark()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderListWithDense()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-list--dense");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderListWithDisabled()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-list--disabled");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderListWithElevation()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.Elevation, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("elevation-2");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderListWithExpand()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.Expand, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasExpandClass = classes.Contains("m-list");

            // Assert
            Assert.IsTrue(hasExpandClass);
        }

        [TestMethod]
        public void RenderListWithFlat()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-list--flat");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-list");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px", style);
        }

        [TestMethod]
        public void RenderWithWidth()
        {
            // Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(p => p.Width, 100);
            });
            var inputSlotDiv = cut.Find(".m-list");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("width: 100px", style);
        }

        [TestMethod]
        public void RenderListWithLight()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.Light, true);
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
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(p => p.MaxHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-list");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMaxWidth()
        {
            // Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(p => p.MaxWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-list");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-width: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinHeight()
        {
            // Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(p => p.MinHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-list");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinWidth()
        {
            // Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(p => p.MinWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-list");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-width: 100px", style);
        }

        [TestMethod]
        public void RenderListWithNav()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.Nav, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNavClass = classes.Contains("m-list--nav");

            // Assert
            Assert.IsTrue(hasNavClass);
        }

        [TestMethod]
        public void RenderListWithOutlined()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-sheet--outlined");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderListWithRounded()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-list--rounded");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderListWithShaped()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-sheet--shaped");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderListWithSubheader()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.Subheader, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSubheaderClass = classes.Contains("m-list--subheader");

            // Assert
            Assert.IsTrue(hasSubheaderClass);
        }

        [TestMethod]
        public void RenderListWithThreeLine()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.ThreeLine, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasThreeLineClass = classes.Contains("m-list--three-line");

            // Assert
            Assert.IsTrue(hasThreeLineClass);
        }

        [TestMethod]
        public void RenderListWithTwoLine()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.TwoLine, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTwoLineClass = classes.Contains("m-list--two-line");

            // Assert
            Assert.IsTrue(hasTwoLineClass);
        }

        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(list => list.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-list");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
