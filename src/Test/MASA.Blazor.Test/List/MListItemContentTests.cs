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
    public class MListItemContentTests:TestBase
    {
        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MListItemContent>(props =>
            {
                props.Add(listitemavatar => listitemavatar.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-list-item__content");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
