using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Overlay
{
    [TestClass]
    public class MOverlayTests:TestBase
    {
        [TestMethod]
        public void RenderOverlayWithDark()
        {
            //Act
            var cut = RenderComponent<MOverlay>(props =>
            {
                props.Add(overlay => overlay.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderOverlayNoWithDark()
        {
            //Act
            var cut = RenderComponent<MOverlay>(props =>
            {
                props.Add(overlay => overlay.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-overlay");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderOverlayWithValue()
        {
            //Act
            var cut = RenderComponent<MOverlay>(props =>
            {
                props.Add(overlay => overlay.Value, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-overlay--active");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderOverlayNoWithValue()
        {
            //Act
            var cut = RenderComponent<MOverlay>(props =>
            {
                props.Add(overlay => overlay.Value, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-overlay");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderOverlayWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MOverlay>(props =>
            {
                props.Add(overlay => overlay.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-overlay--absolute");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderOverlayNoWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MOverlay>(props =>
            {
                props.Add(overlay => overlay.Absolute, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-overlay");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderWithOpacity()
        {
            // Act
            var cut = RenderComponent<MOverlay>(props =>
            {
                props.Add(p => p.Opacity, 0.46);
            });
            var overlayDiv = cut.Find(".m-overlay__scrim");
            var style = overlayDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("background-color:#212121;border-color:#212121;opacity:0", style);
        }

        //[TestMethod]
        //public void RenderButtonAndonClick()
        //{
        //    // Arrange
        //    var times = 0;
        //    var cut = RenderComponent<MOverlay>(props =>
        //    {
        //        props.Add(button => button.OnClick, args =>
        //        {
        //            times++;
        //        });
        //    });

        //    // Act
        //    var buttonElement = cut.Find("Mbutton");
        //    buttonElement.Click();

        //    // Assert
        //    Assert.AreEqual(1, times);
        //}
    }
}
