using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.SystemBar
{
    [TestClass]
    public class MSystemBarTests : TestBase
    {
        [TestMethod]
        public void RenderSystemBarWithAbsolute()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-system-bar");

            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RenderSystemBarWithApp()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.App, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppClass = classes.Contains("m-system-bar");

            // Assert
            Assert.IsTrue(hasAppClass);
        }

        [TestMethod]
        public void RenderSystemBarWithDark()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
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
        public void RenderSystemBarWithFixed()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.Fixed, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedClass = classes.Contains("m-system-bar");

            // Assert
            Assert.IsTrue(hasFixedClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-system-bar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height:100px", style);
        }

        [TestMethod]
        public void RenderSystemBarWithLight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
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
        public void RenderSystemBarWithLightsOut()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.LightsOut, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightsOutClass = classes.Contains("m-system-bar");

            // Assert
            Assert.IsTrue(hasLightsOutClass);
        }

        [TestMethod]
        public void RenderSystemBarWithWindow()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.Window, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasWindowClass = classes.Contains("m-system-bar");

            // Assert
            Assert.IsTrue(hasWindowClass);
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-system-bar");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void RenderSystemBarAndOnClick()
        {
            // Arrange
            JSInterop.Mode = JSRuntimeMode.Loose;
            var times = 0;
            var cut = RenderComponent<MSystemBar>(props =>
            {
                props.Add(systembar => systembar.OnClick, args =>
                {
                    times++;
                });
            });

            // Act
            var systembarElement = cut.Find(".m-system-bar");
            systembarElement.Click();

            // Assert
            Assert.AreEqual(1, times);
        }
    }
}
