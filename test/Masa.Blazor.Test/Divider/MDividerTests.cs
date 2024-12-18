using Bunit;

namespace Masa.Blazor.Test.Divider
{
    [TestClass]
    public class MDividerTests : TestBase
    {
        [TestMethod]
        public void RenderDividerWithDark()
        {
            //Act
            var cut = RenderComponent<MDivider>(props =>
            {
                props.Add(divider => divider.Dark, true);
            });
            var divider = cut.Find(".m-divider__wrapper").FirstElementChild;
            var hasDarkClass = divider.ClassList.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderDividerWithLight()
        {
            //Act
            var cut = RenderComponent<MDivider>(props =>
            {
                props.Add(divider => divider.Light, true);
            });
            var divider = cut.Find(".m-divider__wrapper").FirstElementChild;
            var hasLightClass = divider.ClassList.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderDividerWithInset()
        {
            //Act
            var cut = RenderComponent<MDivider>(props =>
            {
                props.Add(divider => divider.Inset, true);
            });
            var divider = cut.Find(".m-divider__wrapper").FirstElementChild;
            var hasInsetClass = divider.ClassList.Contains("m-divider--inset");

            // Assert
            Assert.IsTrue(hasInsetClass);
        }

        [TestMethod]
        public void RenderDividerWithVertical()
        {
            //Act
            var cut = RenderComponent<MDivider>(props =>
            {
                props.Add(divider => divider.Vertical, true);
            });
            var divider = cut.Find(".m-divider__wrapper").FirstElementChild;
            var hasVerticalClass = divider.ClassList.Contains("m-divider--vertical");

            // Assert
            Assert.IsTrue(hasVerticalClass);
        }
    }
}
