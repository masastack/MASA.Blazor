using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.List
{
    [TestClass]
    public class MListItemSubtitleTests : TestBase
    {
        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MListItemSubtitle>(props =>
            {
                props.Add(listitemsubtitle => listitemsubtitle.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-list-item__subtitle");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
