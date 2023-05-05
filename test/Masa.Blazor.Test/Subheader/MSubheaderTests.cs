using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Subheader
{
    [TestClass]
    public class MSubheaderTests : TestBase
    {
        [TestMethod]
        public void RenderSubheaderWithInset()
        {
            //Act
            var cut = RenderComponent<MSubheader>(props =>
            {
                props.Add(subheader => subheader.Inset, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInsetClass = classes.Contains("m-subheader--inset");
            // Assert
            Assert.IsTrue(hasInsetClass);
        }

        [TestMethod]
        public void RenderSubheaderWithDark()
        {
            //Act
            var cut = RenderComponent<MSubheader>(props =>
            {
                props.Add(subheader => subheader.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderSubheaderWithLight()
        {
            //Act
            var cut = RenderComponent<MSubheader>(props =>
            {
                props.Add(subheader => subheader.Light, true);
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
            var cut = RenderComponent<MStepper>(props =>
            {
                props.Add(subheader => subheader.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-stepper");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
