using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.List
{
    [TestClass]
    public class MListItemTitleTests : TestBase
    {
        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MListItemTitle>(props =>
            {
                props.Add(listitemtitle => listitemtitle.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-list-item__title");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
