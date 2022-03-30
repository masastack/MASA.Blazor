using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Counter
{
    [TestClass]
    public class MCounterTests : TestBase
    {
        [TestMethod]
        public void RenderCounterWithWithDark()
        {
            //Act
            var cut = RenderComponent<MCounter>(props =>
            {
                props.Add(counter => counter.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderCounterWithWithLight()
        {
            //Act
            var cut = RenderComponent<MCounter>(props =>
            {
                props.Add(counter => counter.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }
    }
}
