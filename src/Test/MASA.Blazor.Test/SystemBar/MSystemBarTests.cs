using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Bunit;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.SystemBar
{
    [TestClass]
    public class MSystemBarTests:TestBase
    {
        [TestMethod]
        public void RendeMSystemBarWithLightsOut()
        {
            //Act
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.LightsOut, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightsOutClass = classes.Contains("m-system-bar--lights-out");
            // Assert
            Assert.IsTrue(hasLightsOutClass);
        }

        [TestMethod]
        public void RendeMSystemBarWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-system-bar--absolute");
            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RendeMSystemBarWithnoAbsolute()
        {
            //Act
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noAbsoluteClass =!classes.Contains("m-system-bar--fixed");
            // Assert
            Assert.IsTrue(noAbsoluteClass);
        }

        [TestMethod]
        public void RendeMSystemBarWithWindow()
        {
            //Act
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.Window, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasWindowClass = classes.Contains("m-system-bar--window");
            // Assert
            Assert.IsTrue(hasWindowClass);
        }

        [TestMethod]
        public void RendeMSystemBarWithFixed()
        {
            //Act
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.Fixed, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedClass = classes.Contains("m-system-bar--fixed");
            // Assert
            Assert.IsTrue(hasFixedClass);
        }

        [TestMethod]
        public void RendeMSystemBarWithApp()
        {
            //Act
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.App, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppClass = classes.Contains("m-system-bar--fixed");
            // Assert
            Assert.IsTrue(hasAppClass);
        }

        [TestMethod]
        public void RenderSystemBarWithDark()
        {
            //Act
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderSystemBarWithLight()
        {
            //Act
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderButtonAndonClick()
        {
            // Arrange
            var times = 0;
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.OnClick, args =>
                {
                    times++;
                });
            });

            // Act
            var buttonElement = cut.Find(".m-system-bar");
            buttonElement.Click();

            // Assert
            Assert.AreEqual(1, times);
        }

        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-system-bar");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
