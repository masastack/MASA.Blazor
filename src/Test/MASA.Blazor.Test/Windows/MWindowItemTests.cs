using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Bunit;

namespace MASA.Blazor.Test.Windows
{
    [TestClass]
    public class MWindowItemTests:TestBase
    {
        [TestMethod]
        public void RenderWindowItemWithDisabled()
        {
            //Act
            var cut = RenderComponent<MWindowItem>(props =>
            {
                props.Add(windowitem => windowitem.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-window-item");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MWindowItem>(props =>
            {
                props.Add(windowitem => windowitem.ChildContent,  "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-window-item");
            //row fill-height  
            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
