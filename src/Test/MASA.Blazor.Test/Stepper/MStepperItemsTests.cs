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
    public  class MStepperItemsTests:TestBase
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
