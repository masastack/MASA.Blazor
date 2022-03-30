using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Card
{
    [TestClass]
    public class MCardTitleTests : TestBase
    {
        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MCardTitle>(props =>
            {
                props.Add(cardsubtitle => cardsubtitle.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-card__title");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
