using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Bunit;

namespace MASA.Blazor.Test.Windows
{
    [TestClass]
    public  class MWindowTests:TestBase
    {
        [TestMethod]
        public void RenderWindowWithContinuous()
        {
            //Act
            var cut = RenderComponent<MWindow>(props =>
            {
                props.Add(window => window.Continuous, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
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
            var classes = cut.Instance.CssProvider.GetClass();
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
            var classes = cut.Instance.CssProvider.GetClass();
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
            var classes = cut.Instance.CssProvider.GetClass();
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
            var classes = cut.Instance.CssProvider.GetClass();
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
            var classes = cut.Instance.CssProvider.GetClass();
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
            var classes = cut.Instance.CssProvider.GetClass();
            var hasVerticalClass = classes.Contains("m-window");

            // Assert
            Assert.IsTrue(hasVerticalClass);
        }

        [TestMethod]
        public void RenderWindowWithActiveClass()
        {
            //Act
            var cut = RenderComponent<MWindow>(props =>
            {
                string active = "m-window-item--active";
                props.Add(window => window.ActiveClass, active);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClassClass = classes.Contains("m-window");

            // Assert
            Assert.IsTrue(hasActiveClassClass);
        }

        [TestMethod]
        public void RenderWindowWithNextIcon()
        {
            //Act
            var cut = RenderComponent<MWindow>(props =>
            {
                string icon = "mdi-star";
                props.Add(window => window.NextIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNextIconClass = classes.Contains("m-window");

            // Assert
            Assert.IsTrue(hasNextIconClass);
        }

        [TestMethod]
        public void RenderWindowWithPrevIcon()
        {
            //Act
            var cut = RenderComponent<MWindow>(props =>
            {
                string icon = "mdi-star";
                props.Add(window => window.PrevIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPrevIconClass = classes.Contains("m-window");

            // Assert
            Assert.IsTrue(hasPrevIconClass);
        }

        [TestMethod]
        public void RenderWindowWithValue()
        {
            //Act
            var cut = RenderComponent<MWindow>(props =>
            {
                string icon = "mdi-star";
                props.Add(window => window.Value, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-window");

            // Assert
            Assert.IsTrue(hasValueClass);
        }
    }
}
