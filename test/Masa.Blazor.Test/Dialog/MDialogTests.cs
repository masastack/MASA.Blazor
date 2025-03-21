﻿namespace Masa.Blazor.Test.Dialog
{
    [TestClass]
    public class MDialogTests : TestBase
    {
        [TestMethod]
        public void RenderDialogWithCloseDelay()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(alert => alert.CloseDelay, 0);
            });
            var classes = cut.Instance.GetClass();
            var hasCloseDelayClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasCloseDelayClass);
        }

        [TestMethod]
        public void RenderDialogWithDark()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(dialog => dialog.Dark, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDarkClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderDialogWithDisabled()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(dialog => dialog.Disabled, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDisabledClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderDialogWithFullscreen()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(dialog => dialog.Fullscreen, true);
            });
            var classes = cut.Instance.GetClass();
            var hasFullscreenClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasFullscreenClass);
        }

        [TestMethod]
        public void RenderDialogWithLight()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(dialog => dialog.Light, true);
            });
            var classes = cut.Instance.GetClass();
            var hasLightClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderDialogWithOpenDelay()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(dialog => dialog.OpenDelay, 0);
            });
            var classes = cut.Instance.GetClass();
            var hasOpenDelayClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasOpenDelayClass);
        }

        [TestMethod]
        public void RenderDialogWithOpenOnFocus()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(dialog => dialog.OpenOnFocus, true);
            });
            var classes = cut.Instance.GetClass();
            var hasOpenOnFocusClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasOpenOnFocusClass);
        }

        [TestMethod]
        public void RenderDialogWithOpenOnHover()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(dialog => dialog.OpenOnHover, true);
            });
            var classes = cut.Instance.GetClass();
            var hasOpenOnHoverClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasOpenOnHoverClass);
        }

        [TestMethod]
        public void RenderDialogWithPersistent()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(dialog => dialog.Persistent, true);
            });
            var classes = cut.Instance.GetClass();
            var hasPersistentClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasPersistentClass);
        }

        [TestMethod]
        public void RenderDialogWithScrollable()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(dialog => dialog.Scrollable, true);
            });
            var classes = cut.Instance.GetClass();
            var hasScrollableClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasScrollableClass);
        }


    }
}
