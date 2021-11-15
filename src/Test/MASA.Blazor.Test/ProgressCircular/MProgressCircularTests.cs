using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.ProgressCircular
{
    [TestClass]
    public class MProgressCircularTests:TestBase
    {
        [TestMethod]
        public void RendeProgressCircularWithIndeterminate()
        {
            //Act
            var cut = RenderComponent<MProgressCircular>(props =>
            {
                props.Add(progresscircular => progresscircular.Indeterminate, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIndeterminateClass = classes.Contains("m-progress-circular--indeterminate");

            // Assert
            Assert.IsTrue(hasIndeterminateClass);
        }

        [TestMethod]
        public void RendeProgressCircularNoWithIndeterminate()
        {
            //Act
            var cut = RenderComponent<MProgressCircular>(props =>
            {
                props.Add(progresscircular => progresscircular.Indeterminate, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIndeterminateClass = classes.Contains("m-progress-circular");

            // Assert
            Assert.IsTrue(hasIndeterminateClass);
        }

        //[TestMethod]
        //public void RenderWithChildContentt()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MProgressCircular>(props =>
        //    {
        //        props.Add(list => list.ChildContent, "<span>Hello world</span>");
        //    });
        //    var contentDiv = cut.Find(".m-progresscircular");

        //    // Assert
        //    contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        //}

        //[TestMethod]
        //public void RenderWithSize()
        //{
        //    // Act
        //    var cut = RenderComponent<MProgressCircular>(props =>
        //    {
        //        props.Add(p => p.Rotate, 0);
        //    });
        //    var inputSlotDiv = cut.Find("");
        //    var style = inputSlotDiv.GetAttribute("style");

        //    // Assert
        //    Assert.AreEqual("transform: rotate(0deg)", style);
        //}


    }
}
