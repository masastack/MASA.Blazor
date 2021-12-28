using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bunit;

namespace MASA.Blazor.Test.Stepper
{
    [TestClass]
    public class MStepperHeaderTests:TestBase
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
