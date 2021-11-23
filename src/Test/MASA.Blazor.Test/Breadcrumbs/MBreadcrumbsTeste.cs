using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Breadcrumbs
{
    [TestClass]
    public class MBreadcrumbsTeste:TestBase
    {
        [TestMethod]
        public void RenderButtonWithLarge()
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
        public void RenderButtonNoWithLarge()
        {
            //Act
            var cut = RenderComponent<MBreadcrumbs>(props =>
            {
                props.Add(breadcrumbs => breadcrumbs.Large, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLargeClass = classes.Contains("m-breadcrumbs");

            // Assert
            Assert.IsTrue(hasLargeClass);
        }

        [TestMethod]
        public void RenderAlertWithWithDark()
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
        public void RenderAlertNoWithWithDark()
        {
            //Act
            var cut = RenderComponent<MBreadcrumbs>(props =>
            {
                props.Add(breadcrumbs => breadcrumbs.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-breadcrumbs");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderAlertWithWithLight()
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
        public void RenderAlertNoWithWithLight()
        {
            //Act
            var cut = RenderComponent<MBreadcrumbs>(props =>
            {
                props.Add(breadcrumbs => breadcrumbs.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-breadcrumbs");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderWithChildContentt()
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
