using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Grid
{
    [TestClass]
    public class MContainerTests : TestBase
    {
        [TestMethod]
        public void RenderFooterWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MContainer>(props =>
            {
                props.Add(container => container.Fluid, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFluidClass = classes.Contains("container--fluid");

            // Assert
            Assert.IsTrue(hasFluidClass);
        }

        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MContainer>(props =>
            {
                props.Add(container => container.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".container ");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
