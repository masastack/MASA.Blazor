using Bunit;

namespace Masa.Blazor.Test.Button
{
    [TestClass]
    public class MButtonGroupTests : TestBase
    {
        [TestMethod]
        public void RenderButtonGroupWithDark()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Dark, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderButtonGroupWithLight()
        {
            //Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.Light, true);
            });
            var classes = cut.Instance.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MButtonGroup>(props =>
            {
                props.Add(buttongroup => buttongroup.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-btn-toggle");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
