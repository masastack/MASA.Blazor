using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.NavigationDrawer
{
    [TestClass]
    public class MNavigationDrawerTests:TestBase
    {
        [TestMethod]
        public void RendeButtonGroupWithHorizontal()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-navigation-drawer--absolute");

            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RendeButtonGroupNoWithHorizontal()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Absolute, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithIsBottoml()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Bottom, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIsBottomClass = classes.Contains("m-navigation-drawer--bottom");

            // Assert
            Assert.IsTrue(hasIsBottomClass);
        }

        [TestMethod]
        public void RendeButtonGroupNoWithIsBottoml()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Bottom, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIsBottomClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasIsBottomClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithClipped()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Clipped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClippedClass = classes.Contains("m-navigation-drawer--clipped");

            // Assert
            Assert.IsTrue(hasClippedClass);
        }

        [TestMethod]
        public void RendeButtonGroupNoWithClipped()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Clipped, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClippedClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasClippedClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithIsActive()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Value, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIsActiveClass = classes.Contains("m-navigation-drawer--open");

            // Assert
            Assert.IsTrue(hasIsActiveClass);
        }

        [TestMethod]
        public void RendeButtonGroupNoWithIsActive()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Value, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIsActiveClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasIsActiveClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithFloating()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Floating, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFloatingClass = classes.Contains("m-navigation-drawer--floating");

            // Assert
            Assert.IsTrue(hasFloatingClass);
        }

        [TestMethod]
        public void RendeButtonGroupNoWithFloating()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Floating, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFloatingClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasFloatingClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithExpandOnHover()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.ExpandOnHover, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasExpandOnHoverClass = classes.Contains("m-navigation-drawer--open-on-hover");

            // Assert
            Assert.IsTrue(hasExpandOnHoverClass);
        }

        [TestMethod]
        public void RendeButtonGroupNoWithExpandOnHover()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.ExpandOnHover, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasExpandOnHoverClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasExpandOnHoverClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithRight()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Right, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRightClass = classes.Contains("m-navigation-drawer--right");

            // Assert
            Assert.IsTrue(hasRightClass);
        }

        [TestMethod]
        public void RendeButtonGroupNoWithRight()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Right, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRightClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasRightClass);
        }

        [TestMethod]
        public void RendeButtonGroupWithTemporary()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Temporary, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTemporaryClass = classes.Contains("m-navigation-drawer--temporary");

            // Assert
            Assert.IsTrue(hasTemporaryClass);
        }

        [TestMethod]
        public void RendeButtonGroupNoWithTemporary()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Temporary, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTemporaryClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasTemporaryClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithDark()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerNoWithDark()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithLight()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerNoWithLight()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithApp()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.App, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppClass = classes.Contains("m-navigation-drawer--fixed");

            // Assert
            Assert.IsTrue(hasAppClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerNoWithApp()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.App, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasAppClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithFixed()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Fixed, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedClass = classes.Contains("m-navigation-drawer--fixed");

            // Assert
            Assert.IsTrue(hasFixedClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerNoWithFixed()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Fixed, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasFixedClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noAbsoluteClass =! classes.Contains("m-navigation-drawer--fixed");

            // Assert
            Assert.IsTrue(noAbsoluteClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerNoWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Absolute, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noAbsoluteClass = !classes.Contains("m-navigation-drawer__content");

            // Assert
            Assert.IsTrue(noAbsoluteClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithIsActive()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Value, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noIsActiveClass = !classes.Contains("m-navigation-drawer--fixed");

            // Assert
            Assert.IsTrue(noIsActiveClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerNoWithIsActive()
        {
            //Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Value, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noIsActiveClass = !classes.Contains("m-navigation-drawer__content");

            // Assert
            Assert.IsTrue(noIsActiveClass);
        }

        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(list => list.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-navigation-drawer__content");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        //[TestMethod]
        //public void RenderNavigationDrawerWithDisableResizeWatcher()
        //{
        //    //Act
        //    var cut = RenderComponent<MNavigationDrawer>(props =>
        //    {
        //        props.Add(navigationdrawer => navigationdrawer.DisableResizeWatcher, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasFixedClass = classes.Contains("m-navigation-disable-resize-watcher");

        //    // Assert
        //    Assert.IsTrue(hasFixedClass);
        //}

        //[TestMethod]
        //pub
        //
        //lic void RenderWithSize()
        //{
        //    Act
        //   var cut = RenderComponent<MNavigationDrawer>(props =>
        //   {
        //       props.Add(p => p.Width, 256);
        //   });
        //    var inputSlotDiv = cut.Find(".m-navigation-drawer--open");
        //    var style = inputSlotDiv.GetAttribute("style");

        //    Assert
        //    Assert.AreEqual("width:256px", style);
        //}
    }
}
