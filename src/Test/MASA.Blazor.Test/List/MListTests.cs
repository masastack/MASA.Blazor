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
