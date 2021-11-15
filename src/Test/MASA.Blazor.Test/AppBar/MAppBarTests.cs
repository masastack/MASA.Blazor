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
        public void RenderAppBarWithScroll()
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
        public void RenderAppBarNoWithScroll()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.ElevateOnScroll, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasScrollClass = classes.Contains("m-app-bar");

            // Assert
            Assert.IsTrue(hasScrollClass);
        }

        [TestMethod]
        public void RenderAppBarWithClippedRight()
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
        public void RenderAppBarNoWithClippedRight()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.ClippedRight, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClippedRightClass = classes.Contains("m-app-bar");

            // Assert
            Assert.IsTrue(hasClippedRightClass);
        }

        [TestMethod]
        public void RenderAppBarWithClippedLeft()
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
        public void RenderAppBarNoWithClippedLeft()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.ClippedLeft, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClippedLeftClass = classes.Contains("m-app-bar");

            // Assert
            Assert.IsTrue(hasClippedLeftClass);
        }

        [TestMethod]
        public void RenderAppBarWithAbsolute()
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
        public void RenderAppBarNoWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.Absolute, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noAbsoluteClass = !classes.Contains("m-toolbar--absolute");

            // Assert
            Assert.IsTrue(noAbsoluteClass);
        }

        [TestMethod]
        public void RenderAppBarWithApp()
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
        public void RenderAppBarNoWithApp()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.App, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppClass = classes.Contains("m-app-bar");

            // Assert
            Assert.IsTrue(hasAppClass);
        }

        [TestMethod]
        public void RenderAppBarWithFixed()
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
        public void RenderAppBarNoWithFixed()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.Fixed, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedClass = classes.Contains("m-app-bar");

            // Assert
            Assert.IsTrue(hasFixedClass);
        }

        [TestMethod]
        public void RenderAppBarWithCollapseOnScroll()
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
        public void RenderAppBarNoWithCollapseOnScroll()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.CollapseOnScroll, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasScrollClass = classes.Contains("m-toolbar");

            // Assert
            Assert.IsTrue(hasScrollClass);
        }

        [TestMethod]
        public void RenderAppBarWithShrinkOnScroll()
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
        public void RenderAppBarNoWithShrinkOnScroll()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.ShrinkOnScroll, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasScrollClass = classes.Contains("m-app-bar");

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

        [TestMethod]
        public void RenderWithMarginTop()
        {
            // Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(p => p.MarginTop, 100);
            });
            var inputSlotDiv = cut.Find(".m-app-bar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 64px;margin-top:100px;transform:translateY(0px);left:0px;right:0px", style);
        }

        [TestMethod]
        public void RenderAppBarWithFadeImgOnScroll()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.FadeImgOnScroll, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFadeImgOnScrollClass = classes.Contains("m-app-bar");

            // Assert
            Assert.IsTrue(hasFadeImgOnScrollClass);
        }

        [TestMethod]
        public void RenderAppBarNoWithFadeImgOnScroll()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.FadeImgOnScroll, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFadeImgOnScrollClass = classes.Contains("m-app-bar");

            // Assert
            Assert.IsTrue(hasFadeImgOnScrollClass);
        }

        [TestMethod]
        public void RenderAppBarWithHideOnScroll()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.HideOnScroll, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideOnScrollClass = classes.Contains("m-app-bar");

            // Assert
            Assert.IsTrue(hasHideOnScrollClass);
        }

        [TestMethod]
        public void RenderAppBarNoWithHideOnScroll()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.HideOnScroll, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideOnScrollClass = classes.Contains("m-app-bar");

            // Assert
            Assert.IsTrue(hasHideOnScrollClass);
        }

        [TestMethod]
        public void RenderAppBarWithInvertedScroll()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.InvertedScroll, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInvertedScrollClass = classes.Contains("m-app-bar");

            // Assert
            Assert.IsTrue(hasInvertedScrollClass);
        }

        [TestMethod]
        public void RenderAppBarNoWithInvertedScroll()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.InvertedScroll, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInvertedScrollClass = classes.Contains("m-app-bar");

            // Assert
            Assert.IsTrue(hasInvertedScrollClass);
        }

        [TestMethod]
        public void RenderAppBarWithScrollThreshold()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.ScrollThreshold, 20);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasScrollThresholdClass = classes.Contains("m-app-bar");

            // Assert
            Assert.IsTrue(hasScrollThresholdClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(p => p.Height, 64);
            });
            var inputSlotDiv = cut.Find(".m-app-bar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 64px;height: 64px;height:64px;transform:translateY(0px);left:0px;right:0px", style);
        }

        [TestMethod]
        public void RenderAppBarWithCollapse()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.Collapse, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCollapseClass = classes.Contains("m-toolbar--collapse");

            // Assert
            Assert.IsTrue(hasCollapseClass);
        }

        [TestMethod]
        public void RenderAppBarNoWithCollapse()
        {
            //Act
            var cut = RenderComponent<MAppBar>(props =>
            {
                props.Add(appbar => appbar.Collapse, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCollapseClass = classes.Contains("m-app-bar");

            // Assert
            Assert.IsTrue(hasCollapseClass);
        }
    }
}
