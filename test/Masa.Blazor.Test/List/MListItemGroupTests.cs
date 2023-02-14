using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.List
{
    [TestClass]
    public class MListItemGroupTests : TestBase
    {
        [TestMethod]
        public void RenderListItemGroupWithDark()
        {
            //Act
            var cut = RenderComponent<MListItemGroup>(props =>
            {
                props.Add(listitemgroup => listitemgroup.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-list-item-group");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderListItemGroupWithLight()
        {
            //Act
            var cut = RenderComponent<MListItemGroup>(props =>
            {
                props.Add(listitemgroup => listitemgroup.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-list-item-group");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderListItemGroupWithMandatory()
        {
            //Act
            var cut = RenderComponent<MListItemGroup>(props =>
            {
                props.Add(listitemgroup => listitemgroup.Mandatory, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMandatoryClass = classes.Contains("m-list-item");

            // Assert
            Assert.IsTrue(hasMandatoryClass);
        }

        [TestMethod]
        public void RenderListItemGroupWithMax()
        {
            //Act
            var cut = RenderComponent<MListItemGroup>(props =>
            {
                props.Add(listitemgroup => listitemgroup.Max, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMaxClass = classes.Contains("m-list-item-group");

            // Assert
            Assert.IsTrue(hasMaxClass);
        }

        [TestMethod]
        public void RenderListItemGroupWithMultiple()
        {
            //Act
            var cut = RenderComponent<MListItemGroup>(props =>
            {
                props.Add(listitemgroup => listitemgroup.Multiple, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMultipleClass = classes.Contains("m-list-item");

            // Assert
            Assert.IsTrue(hasMultipleClass);
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MListItemGroup>(props =>
            {
                props.Add(listitemgroup => listitemgroup.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-list-item-group");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
