using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Stepper
{
    [TestClass]
    public class MStepperHeaderTests : TestBase
    {
        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MStepperHeader>(props =>
            {
                props.Add(stepperheader => stepperheader.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-stepper__header");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
