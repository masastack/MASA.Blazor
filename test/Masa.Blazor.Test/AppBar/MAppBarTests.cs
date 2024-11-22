using AngleSharp.Css.Dom;
using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.AppBar
{
    [TestClass]
    public class MAppBarTests : TestBase<MAppBar>
    {
        public MAppBarTests() : base("header")
        {
        }

        [TestMethod]
        public void RenderAppBarWithAbsolute()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.Absolute, true); });
            var hasAbsoluteClass = cut.ClassList.Contains("m-toolbar--absolute");

            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RenderAppBarWithApp()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.App, true); });
            var hasAppClass = cut.ClassList.Contains("m-app-bar--app");

            // Assert
            Assert.IsTrue(hasAppClass);
        }

        [TestMethod]
        public void RenderAppBarWithBottom()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.Bottom, true); });

            var hasBottomClass = cut.ClassList.Contains("m-toolbar--bottom");

            // Assert
            Assert.IsTrue(hasBottomClass);
        }

        [TestMethod]
        public void RenderAppBarWithClippedRight()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.ClippedRight, true); });
            var hasClippedClass = cut.ClassList.Contains("m-app-bar--clipped");
            var hasClippedRightClass = cut.ClassList.Contains("m-app-bar--clipped-right");

            // Assert
            Assert.IsTrue(hasClippedClass && hasClippedRightClass);
        }

        [TestMethod]
        public void RenderAppBarWithClippedLeft()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.ClippedLeft, true); });
            var hasClippedClass = cut.ClassList.Contains("m-app-bar--clipped");
            var hasClippedLeftClass = cut.ClassList.Contains("m-app-bar--clipped-left");

            // Assert
            Assert.IsTrue(hasClippedClass && hasClippedLeftClass);
        }

        [TestMethod]
        public void RenderAppBarWithCollapse()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.Collapse, true); });
            var hasClippedLeftClass = cut.ClassList.Contains("m-toolbar--collapse");

            // Assert
            Assert.IsTrue(hasClippedLeftClass);
        }

        [TestMethod]
        public void RenderAppBarWithDense()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.Dense, true); });
            var hasDenseClass = cut.ClassList.Contains("m-toolbar--dense");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderButtonWithScroll()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.ElevateOnScroll, true); });
            var hasScrollClass = cut.ClassList.Contains("m-app-bar--elevate-on-scroll");

            // Assert
            Assert.IsTrue(hasScrollClass);
        }

        [TestMethod]
        public void RenderAppBarWithElevation()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.Elevation, 2); });
            var hasElevationClass = cut.ClassList.Contains("elevation-2");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderButtonWithExtended()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.Extended, true); });
            var hasScrollClass = cut.ClassList.Contains("m-toolbar--extended");

            // Assert
            Assert.IsTrue(hasScrollClass);
        }

        [TestMethod]
        public void RenderAppBarWithExtensionHeight()
        {
            //Act
            var cut = Render(props =>
            {
                props.Add(appbar => appbar.Extended, true);
                props.Add(appbar => appbar.ExtensionHeight, 52);
            });

            // Assert
            var extension = cut.Find(".m-toolbar__extension");
            Assert.AreEqual("52px", extension.GetStyle()["height"]);
        }

        [TestMethod]
        public void RenderAppBarWithFadeImgOnScroll()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.FadeImgOnScroll, true); });
            var hasFadeImgOnScrollClass = cut.ClassList.Contains("m-app-bar--fade-img-on-scroll");

            // Assert
            Assert.IsTrue(hasFadeImgOnScrollClass);
        }

        [TestMethod]
        public void RenderAppBarWithFixed()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.Fixed, true); });
            var hasFixedClass = cut.ClassList.Contains("m-app-bar--fixed");

            // Assert
            Assert.IsTrue(hasFixedClass);
        }

        [TestMethod]
        public void RenderAppBarWithFlat()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.Flat, true); });
            var hasFlatClass = cut.ClassList.Contains("m-toolbar--flat");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderAppBarWithFloating()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.Floating, true); });
            var hasFloatingClass = cut.ClassList.Contains("m-toolbar--floating");

            // Assert
            Assert.IsTrue(hasFloatingClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = Render(props => { props.Add(p => p.Height, 100); });
            var inputSlotDiv = cut.Find(".m-toolbar__content");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px;", style);
        }

        [TestMethod]
        public void RenderWithWidth()
        {
            // Act
            var cut = Render(props => { props.Add(p => p.Width, 100); });
            var inputSlotDiv = cut.Find(".m-toolbar__content");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 64px;", style);
        }

        [TestMethod]
        public void RenderWithMinHeight()
        {
            // Act
            var cut = Render(props => { props.Add(p => p.MinHeight, 100); });
            var inputSlotDiv = cut.Find(".m-toolbar__content");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 64px;", style);
        }

        [TestMethod]
        public void RenderWithMinWidth()
        {
            // Act
            var cut = Render(props => { props.Add(p => p.MinWidth, 100); });
            var inputSlotDiv = cut.Find(".m-toolbar__content");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 64px;", style);
        }

        [TestMethod]
        public void RenderWithMaxHeight()
        {
            // Act
            var cut = Render(props => { props.Add(p => p.MaxHeight, 100); });
            var inputSlotDiv = cut.Find(".m-toolbar__content");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 64px;", style);
        }

        [TestMethod]
        public void RenderWithMaxWidth()
        {
            // Act
            var cut = Render(props => { props.Add(p => p.MaxWidth, 100); });
            var inputSlotDiv = cut.Find(".m-toolbar__content");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 64px;", style);
        }

        [TestMethod]
        public void RenderAppBarWithLight()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.Light, true); });
            var hasLightClass = cut.ClassList.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderAppBarWithOutlined()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.Outlined, true); });
            var hasOutlinedClass = cut.ClassList.Contains("m-sheet--outlined");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderAppBarWithProminent()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.Prominent, true); });
            var hasProminentClass = cut.ClassList.Contains("m-toolbar--prominent");

            // Assert
            Assert.IsTrue(hasProminentClass);
        }

        [TestMethod]
        public void RenderWithScrollThreshold()
        {
            // Act
            var cut = Render(props => { props.Add(p => p.ScrollThreshold, 0); });
            var inputSlotDiv = cut.Find(".m-app-bar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 64px;transform: translateY(0px);margin-top: 0px;left: 0px;right: 0px;", style);
        }

        [TestMethod]
        public void RenderButtonWithShrinkOnScroll()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.ShrinkOnScroll, true); });
            var hasScrollClass = cut.ClassList.Contains("m-app-bar--shrink-on-scroll");

            // Assert
            Assert.IsTrue(hasScrollClass);
        }

        [TestMethod]
        public void RenderAppBarWithTile()
        {
            //Act
            var cut = RenderAndGetRootElement(props => { props.Add(appbar => appbar.Tile, true); });
            var hasRoundedClass = cut.ClassList.Contains("rounded-0");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = Render(props => { props.Add(appbar => appbar.ChildContent, "<span>Hello world</span>"); });
            var contentDiv = cut.Find(".m-toolbar__content");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}