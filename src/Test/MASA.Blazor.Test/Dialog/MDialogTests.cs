using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Dialog
{
    [TestClass]
    public class MDialogTests:TestBase
    {
        [TestMethod]
        public void RenderDialogWithAttach()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                string attach = "dialog";
                props.Add(dialog => dialog.Attach, attach);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAttachClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasAttachClass);
        }

        [TestMethod]
        public void RenderDialogWithCloseDelay()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(alert => alert.CloseDelay, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
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
            var classes = cut.Instance.CssProvider.GetClass();
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
            var classes = cut.Instance.CssProvider.GetClass();
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
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFullscreenClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasFullscreenClass);
        }

        [TestMethod]
        public void RenderDialogWithHideOverlay()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(dialog => dialog.HideOverlay, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideOverlayClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasHideOverlayClass);
        }

        [TestMethod]
        public void RenderDialogWithLight()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(dialog => dialog.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderDialogWithMaxWidth()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(alert => alert.MaxWidth, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMaxWidthClass = classes.Contains("m-dialog__container");

            // Assert
            Assert.IsTrue(hasMaxWidthClass);
        }

        [TestMethod]
        public void RenderDialogWithWidth()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(alert => alert.Width, 64);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasWidthClass = classes.Contains("m-dialog__container");

            // Assert
            Assert.IsTrue(hasWidthClass);
        }

        [TestMethod]
        public void RenderDialogWithOpenDelay()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(dialog => dialog.OpenDelay, 0);
            });
            var classes = cut.Instance.CssProvider.GetClass();
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
            var classes = cut.Instance.CssProvider.GetClass();
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
            var classes = cut.Instance.CssProvider.GetClass();
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
            var classes = cut.Instance.CssProvider.GetClass();
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
            var classes = cut.Instance.CssProvider.GetClass();
            var hasScrollableClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasScrollableClass);
        }

        [TestMethod]
        public void RenderDialogWithTransition()
        {
            //Act
            var cut = RenderComponent<MDialog>(props =>
            {
                string transition = "dialog-transition";
                props.Add(dialog => dialog.Transition, transition);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTransitionClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasTransitionClass);
        }

        [TestMethod]
        public void RenderDialogWithValue()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDialog>(props =>
            {
                props.Add(dialog => dialog.Value, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-dialog");

            // Assert
            Assert.IsTrue(hasValueClass);
        }
    }
}
