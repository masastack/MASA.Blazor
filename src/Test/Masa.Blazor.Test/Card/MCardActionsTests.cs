using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Card
{
    [TestClass]
    public class MCardActionsTests : TestBase
    {
        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MCardActions>(props =>
            {
                props.Add(cardactions => cardactions.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-card__actions");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
