using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.ProgressCircular
{
    [TestClass]
    public class MProgressCircularTests : TestBase
    {
        [TestMethod]
        public void RenderProgressCircularWithIndeterminate()
        {
            //Act
            var cut = RenderComponent<MProgressCircular>(props =>
            {
                props.Add(progresscircular => progresscircular.Indeterminate, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIndeterminateClass = classes.Contains("m-progress-circular--indeterminate");

            // Assert
            Assert.IsTrue(hasIndeterminateClass);
        }

        [TestMethod]
        public void RenderProgressCircularWithRotate()
        {
            //Act
            var cut = RenderComponent<MProgressCircular>(props =>
            {
                props.Add(progresscircular => progresscircular.Rotate, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRotateClass = classes.Contains("m-progress-circular");

            // Assert
            Assert.IsTrue(hasRotateClass);
        }

        [TestMethod]
        public void RenderProgressCircularWithSize()
        {
            //Act
            var cut = RenderComponent<MProgressCircular>(props =>
            {
                props.Add(progresscircular => progresscircular.Size, 32);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSizeClass = classes.Contains("m-progress-circular");

            // Assert
            Assert.IsTrue(hasSizeClass);
        }

        [TestMethod]
        public void RenderProgressCircularWithValue()
        {
            //Act
            var cut = RenderComponent<MProgressCircular>(props =>
            {
                props.Add(progresscircular => progresscircular.Value, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-progress-circular");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderProgressCircularWithWidth()
        {
            //Act
            var cut = RenderComponent<MProgressCircular>(props =>
            {
                props.Add(progresscircular => progresscircular.Width, 4);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasWidthClass = classes.Contains("m-progress-circular");

            // Assert
            Assert.IsTrue(hasWidthClass);
        }
    }
}
