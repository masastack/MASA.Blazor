using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.List
{
    [TestClass]
    public class MListTests:TestBase
    {

        [TestMethod]
        public void RendeListWithOutlined()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-sheet--outlined");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RendeListNoWithOutlined()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Outlined, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-sheet");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RendeListWithShaped()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-sheet--shaped");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RendeListNoWithShaped()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Shaped, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-sheet");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RendeListWithDense()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-list--dense");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RendeListNoWithDense()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Dense, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-list");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RendeListWithDisabled()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-list--disabled");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RendeListNoWithDisabled()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Disabled, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-list");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RendeListWithFlat()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-list--flat");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RendeListNoWithFlat()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Flat, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-list");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RendeListWithNav()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Nav, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNavClass = classes.Contains("m-list--nav");

            // Assert
            Assert.IsTrue(hasNavClass);
        }

        [TestMethod]
        public void RendeListNoWithNav()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Nav, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNavClass = classes.Contains("m-list");

            // Assert
            Assert.IsTrue(hasNavClass);
        }

        [TestMethod]
        public void RendeListWithRounded()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-list--rounded");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RendeListNoWithRounded()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Rounded, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-list");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RendeListWithSubheader()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Subheader, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSubheaderClass = classes.Contains("m-list--subheader");

            // Assert
            Assert.IsTrue(hasSubheaderClass);
        }

        [TestMethod]
        public void RendeListNoWithSubheader()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Subheader, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSubheaderClass = classes.Contains("m-list");

            // Assert
            Assert.IsTrue(hasSubheaderClass);
        }

        [TestMethod]
        public void RendeListWithTwoLine()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.TwoLine, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTwoLineClass = classes.Contains("m-list--two-line");

            // Assert
            Assert.IsTrue(hasTwoLineClass);
        }

        [TestMethod]
        public void RendeListNoWithTwoLine()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.TwoLine, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTwoLineClass = classes.Contains("m-list");

            // Assert
            Assert.IsTrue(hasTwoLineClass);
        }

        [TestMethod]
        public void RendeListWithThreeLine()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.ThreeLine, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasThreeLineClass = classes.Contains("m-list--three-line");

            // Assert
            Assert.IsTrue(hasThreeLineClass);
        }

        [TestMethod]
        public void RendeListNoWithThreeLine()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.ThreeLine, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasThreeLineClass = classes.Contains("m-list");

            // Assert
            Assert.IsTrue(hasThreeLineClass);
        }

        [TestMethod]
        public void RendeListWithDark()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RendeListNoWithDark()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-list");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RendeListWithLight()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RendeListNoWithLight()
        {
            //Act
            var cut = RenderComponent<MList>(props =>
            {
                props.Add(listgroup => listgroup.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-list");

            // Assert
            Assert.IsTrue(hasLightClass);
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
    }
}
