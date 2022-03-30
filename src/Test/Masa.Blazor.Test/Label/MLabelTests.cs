using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Label
{
    [TestClass]
    public class MLabelTests : TestBase
    {
        [TestMethod]
        public void RenderLabelWithDark()
        {
            //Act
            var cut = RenderComponent<MLabel>(props =>
            {
                props.Add(counter => counter.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderLabelWithLight()
        {
            //Act
            var cut = RenderComponent<MLabel>(props =>
            {
                props.Add(counter => counter.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderLabelWithDisabled()
        {
            //Act
            var cut = RenderComponent<MLabel>(props =>
            {
                props.Add(counter => counter.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-label--is-disabled");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderLabelWithValue()
        {
            //Act
            var cut = RenderComponent<MLabel>(props =>
            {
                props.Add(counter => counter.Value, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-label--active");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }
    }
}
