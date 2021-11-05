using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.AppBar
{
    [TestClass]
    public class MAppBarTests:TestBase
    {
        [TestMethod]
        public void RenderButtonWithScroll()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.ElevateOnScroll, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasScrollClass = classes.Contains("m-app-bar--elevate-on-scroll");

            // Assert
            Assert.IsTrue(hasScrollClass);
        }

        [TestMethod]
        public void RenderButtonWithClippedRight()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.ClippedRight, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClippedRightClass = classes.Contains("m-app-bar--clipped");

            // Assert
            Assert.IsTrue(hasClippedRightClass);
        }

        [TestMethod]
        public void RenderButtonWithClippedLeft()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.ClippedLeft, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClippedLeftClass = classes.Contains("m-app-bar--clipped");

            // Assert
            Assert.IsTrue(hasClippedLeftClass);
        }

        [TestMethod]
        public void RenderButtonWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noAbsoluteClass = !classes.Contains("m-app-bar--fixed");

            // Assert
            Assert.IsTrue(noAbsoluteClass);
        }

        [TestMethod]
        public void RenderButtonWithApp()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.App, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppClass = classes.Contains("m-app-bar--fixed");

            // Assert
            Assert.IsTrue(hasAppClass);
        }

        [TestMethod]
        public void RenderButtonWithFixed()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.Fixed, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedClass = classes.Contains("m-app-bar--fixed");

            // Assert
            Assert.IsTrue(hasFixedClass);
        }

        [TestMethod]
        public void RenderButtonWithCollapseOnScroll()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.CollapseOnScroll, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasScrollClass = classes.Contains("m-toolbar--collapse");

            // Assert
            Assert.IsTrue(hasScrollClass);
        }

        [TestMethod]
        public void RenderButtonWithShrinkOnScroll()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.ShrinkOnScroll, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasScrollClass = classes.Contains("v-app-bar--shrink-on-scroll");

            // Assert
            Assert.IsTrue(hasScrollClass);
        }

        [TestMethod]
        public void RenderWithLeft()
        {
            // Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(p => p.Left, 0);
            });
            var inputSlotDiv = cut.Find(".m-app-bar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 64px;transform:translateY(0px);left:0px;right:0px", style);
        }

        [TestMethod]
        public void RenderWithRight()
        {
            // Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(p => p.Right, 0);
            });
            var inputSlotDiv = cut.Find(".m-app-bar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 64px;transform:translateY(0px);left:0px;right:0px", style);
        }
    }
}
