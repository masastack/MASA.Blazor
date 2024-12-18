using Bunit;

namespace Masa.Blazor.Test.Windows
{
    [TestClass]
    public class MWindowItemTests : TestBase
    {
        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MWindowItem>(props =>
            {
                props.Add(w => w.Eager, true);
                props.Add(windowitem => windowitem.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-window-item");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
