using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.List
{
    [TestClass]
    public class MListItemIconTests:TestBase
    {
        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MListItemIcon>(props =>
            {
                props.Add(listitemicon => listitemicon.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-list-item__icon");
            //v-list-item border v
            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
