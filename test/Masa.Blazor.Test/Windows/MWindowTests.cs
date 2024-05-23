using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Windows
{
    [TestClass]
    public class MWindowTests : TestBase
    {
        [TestMethod]
        public void RenderWindowWithContinuous()
        {
            //Act
            var cut = RenderComponent<MWindow>(props =>
            {
                props.Add(window => window.Continuous, true);
            });
            var classes = cut.Instance.GetClass();
            var hasContinuousClass = classes.Contains("m-window");

            // Assert
            Assert.IsTrue(hasContinuousClass);
        }

        [TestMethod]
        public void RenderWindowWithDark()
        {
            //Act
            var cut = RenderComponent<MWindow>(props =>
            {
                props.Add(window => window.Dark, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDarkClass = classes.Contains("m-window");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderWindowWithLight()
        {
            //Act
            var cut = RenderComponent<MWindow>(props =>
            {
                props.Add(window => window.Light, true);
            });
            var classes = cut.Instance.GetClass();
            var hasLightClass = classes.Contains("m-window");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderWindowWithReverse()
        {
            //Act
            var cut = RenderComponent<MWindow>(props =>
            {
                props.Add(window => window.Reverse, true);
            });
            var classes = cut.Instance.GetClass();
            var hasReverseClass = classes.Contains("m-window");

            // Assert
            Assert.IsTrue(hasReverseClass);
        }

        [TestMethod]
        public void RenderWindowWithShowArrows()
        {
            //Act
            var cut = RenderComponent<MWindow>(props =>
            {
                props.Add(window => window.ShowArrows, true);
            });
            var classes = cut.Instance.GetClass();
            var hasShowArrowsClass = classes.Contains("m-window");

            // Assert
            Assert.IsTrue(hasShowArrowsClass);
        }

        [TestMethod]
        public void RenderWindowWithShowArrowsOnHover()
        {
            //Act
            var cut = RenderComponent<MWindow>(props =>
            {
                props.Add(window => window.ShowArrowsOnHover, true);
            });
            var classes = cut.Instance.GetClass();
            var hasShowArrowsOnHoverClass = classes.Contains("m-window--show-arrows-on-hover");

            // Assert
            Assert.IsTrue(hasShowArrowsOnHoverClass);
        }

        [TestMethod]
        public void RenderWindowWithVertical()
        {
            //Act
            var cut = RenderComponent<MWindow>(props =>
            {
                props.Add(window => window.Vertical, true);
            });
            var classes = cut.Instance.GetClass();
            var hasVerticalClass = classes.Contains("m-window");

            // Assert
            Assert.IsTrue(hasVerticalClass);
        }
    }
}
