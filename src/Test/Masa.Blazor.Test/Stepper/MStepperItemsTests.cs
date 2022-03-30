using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Stepper
{
    [TestClass]
    public class MStepperItemsTests : TestBase
    {
        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MStepperItems>(props =>
            {
                props.Add(stepperitems => stepperitems.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-stepper__items");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
