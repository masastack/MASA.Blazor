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
    public class MListItemTests:TestBase
    {
        [TestMethod]
        public void RendeButtonGroupWithDense()
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
        public void RendeButtonGroupNoWithDense()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Dense, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-list-item");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithDisabled()
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
        public void RendeButtonGroupNoWithDisabled()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Disabled, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-list-item");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithSelectable()
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
        public void RendeButtonGroupNoWithSelectable()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Selectable, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSelectableClass = classes.Contains("m-list-item");

            // Assert
            Assert.IsTrue(hasSelectableClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithTwoLine()
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
        public void RendeButtonGroupNoWithTwoLine()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.TwoLine, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTwoLineClass = classes.Contains("m-list-item");

            // Assert
            Assert.IsTrue(hasTwoLineClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithThreeLine()
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
        public void RendeButtonGroupNoWithThreeLine()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.ThreeLine, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasThreeLineClass = classes.Contains("m-list-item");

            // Assert
            Assert.IsTrue(hasThreeLineClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithHighlighted()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Highlighted, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHighlightedClass = classes.Contains("m-list-item--highlighted");

            // Assert
            Assert.IsTrue(hasHighlightedClass);
        }

        [TestMethod]
        public void RendeButtonGroupNoWithHighlighted()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Highlighted, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHighlightedClass = classes.Contains("m-list-item");

            // Assert
            Assert.IsTrue(hasHighlightedClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithInactive()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Inactive, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noInactiveClass = !classes.Contains("m-list-item--link");

            // Assert
            Assert.IsTrue(noInactiveClass);
        }

        [TestMethod]
        public void RendeButtonGroupNoWithInactive()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listitem => listitem.Inactive, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noInactiveClass = !classes.Contains("v-list-item__title");

            // Assert
            Assert.IsTrue(noInactiveClass);
        }

        [TestMethod]
        public void RendeListWithDark()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
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
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listgroup => listgroup.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-list-item");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RendeListWithLight()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listgroup => listgroup.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RendeListNoWithLight()
        {
            //Act
            var cut = RenderComponent<MListItem>(props =>
            {
                props.Add(listgroup => listgroup.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-list-item");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }
    }
}
