using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Breadcrumbs
{
    [TestClass]
    public class MBreadcrumbsTeste : TestBase
    {
        [TestMethod]
        public void RenderBreadcrumbsWithLarge()
        {
            //Act
            var cut = RenderComponent<MBreadcrumbs>(props =>
            {
                props.Add(breadcrumbs => breadcrumbs.Large, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLargeClass = classes.Contains("m-breadcrumbs--large");

            // Assert
            Assert.IsTrue(hasLargeClass);
        }

        [TestMethod]
        public void RenderBreadcrumbsWithWithDark()
        {
            //Act
            var cut = RenderComponent<MBreadcrumbs>(props =>
            {
                props.Add(breadcrumbs => breadcrumbs.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderBreadcrumbsWithWithLight()
        {
            //Act
            var cut = RenderComponent<MBreadcrumbs>(props =>
            {
                props.Add(breadcrumbs => breadcrumbs.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MBreadcrumbs>(props =>
            {
                props.Add(breadcrumbs => breadcrumbs.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-breadcrumbs");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
