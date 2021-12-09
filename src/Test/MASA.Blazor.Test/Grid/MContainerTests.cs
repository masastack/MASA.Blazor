using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Grid
{
    [TestClass]
    public class MContainerTests:TestBase
    {
        [TestMethod]
        public void RenderContainerWithFluid()
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
        public void RenderContainerWithId()
        {
            //Act
            var cut = RenderComponent<MContainer>(props =>
            {
                string id = "container";
                props.Add(container => container.Id, id);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIdClass = classes.Contains("container");

            // Assert
            Assert.IsTrue(hasIdClass);
        }

        [TestMethod]
        public void RenderContainerWithTag()
        {
            //Act
            var cut = RenderComponent<MContainer>(props =>
            {
                string id = "container";
                props.Add(container => container.Tag, id);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTagClass = classes.Contains("container");

            // Assert
            Assert.IsTrue(hasTagClass);
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
    