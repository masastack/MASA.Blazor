using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Bunit;

namespace MASA.Blazor.Test.Tabs
{
    [TestClass]
    public  class MWindowTests:TestBase
    {
        [TestMethod]
        public void RendeWindowWithShowArrowsOnHover()
        {
            //Act
            var cut = RenderComponent<MWindow>(props =>
            {
                props.Add(window => window.ShowArrowsOnHover, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowArrowsOnHoverClass = classes.Contains("m-window--show-arrows-on-hover");

            // Assert
            Assert.IsTrue(hasShowArrowsOnHoverClass);
        }

        [TestMethod]
        public void RendeWindowNoWithShowArrowsOnHover()
        {
            //Act
            var cut = RenderComponent<MWindow>(props =>
            {
                props.Add(window => window.ShowArrowsOnHover, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowArrowsOnHoverClass = classes.Contains("m-window");

            // Assert
            Assert.IsTrue(hasShowArrowsOnHoverClass);
        }

        //[TestMethod]
        //public void RenderWithChildContentt()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MWindow>(props =>
        //    {
        //        props.Add(list => list.PrevContent, context=>"<span>Hello world</span>");
        //    });
        //    var contentDiv = cut.Find(".m-window__prev");

        //    // Assert
        //    contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        //}
    }
}
