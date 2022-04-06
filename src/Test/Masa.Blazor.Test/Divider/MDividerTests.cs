using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

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
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

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
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInsetClass = classes.Contains("m-divider--inset");

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
            var classes = cut.Instance.CssProvider.GetClass();
            var hasVerticalClass = classes.Contains("m-divider--vertical");

            // Assert
            Assert.IsTrue(hasVerticalClass);
        }
    }
}
