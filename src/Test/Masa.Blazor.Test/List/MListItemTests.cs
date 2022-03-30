using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.List
{
    [TestClass]
    public class MListItemTests : TestBase
    {
        [TestMethod]
        public void RenderListItemWithDense()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-list-item--dense");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderListItemWithDisabled()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-list-item--disabled");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderListItemWithInactive()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Inactive, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInactiveClass = classes.Contains("m-list-item");

            // Assert
            Assert.IsTrue(hasInactiveClass);
        }

        [TestMethod]
        public void RenderListItemWithDark()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderListItemWithLight()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderListItemWithLink()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Link, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInactiveClass = classes.Contains("m-list-item--link");

            // Assert
            Assert.IsTrue(hasInactiveClass);
        }

        [TestMethod]
        public void RenderListItemWithRipple()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Ripple, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRippleClass = classes.Contains("m-list-item");

            // Assert
            Assert.IsTrue(hasRippleClass);
        }

        [TestMethod]
        public void RenderListItemWithSelectable()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Selectable, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSelectableClass = classes.Contains("m-list-item--selectable");

            // Assert
            Assert.IsTrue(hasSelectableClass);
        }

        [TestMethod]
        public void RenderListItemWithThreeLine()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.ThreeLine, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasThreeLineClass = classes.Contains("m-list-item--three-line");

            // Assert
            Assert.IsTrue(hasThreeLineClass);
        }

        [TestMethod]
        public void RenderListItemWithTwoLine()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.TwoLine, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTwoLineClass = classes.Contains("m-list-item--two-line");

            // Assert
            Assert.IsTrue(hasTwoLineClass);
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-list-item");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
