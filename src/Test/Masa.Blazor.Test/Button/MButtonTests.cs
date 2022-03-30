using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Button
{
    [TestClass]
    public class MButtonTests : TestBase
    {
        [TestMethod]
        public void RenderButtonWithAbsolute()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Absolute, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAbsoluteClass = classes.Contains("m-btn--absolute");

            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RenderButtonWithBlock()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Block, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBlockClass = classes.Contains("m-btn--block");

            // Assert
            Assert.IsTrue(hasBlockClass);
        }

        [TestMethod]
        public void RenderButtonWithBottom()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Bottom, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBottomClass = classes.Contains("m-btn--bottom");

            // Assert
            Assert.IsTrue(hasBottomClass);
        }

        [TestMethod]
        public void RenderButtonWithDark()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderButtonWithDepressed()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Depressed, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noElevatedClass = !classes.Contains("m-bin--is-elevated");

            // Assert
            Assert.IsTrue(noElevatedClass);
        }

        [TestMethod]
        public void RenderButtonWithDisabled()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-btn--disabled");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderButtonWithElevation()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Elevation, 24);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("m-btn--is-elevated");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderButtonWithFab()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Fab, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFabClass = classes.Contains("m-btn--fab");

            // Assert
            Assert.IsTrue(hasFabClass);
        }

        [TestMethod]
        public void RenderButtonWithFixed()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Fixed, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFixedClass = classes.Contains("m-btn--fixed");

            // Assert
            Assert.IsTrue(hasFixedClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-btn");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px", style);
        }

        [TestMethod]
        public void RenderButtonWithIcon()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Icon, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIconClass = classes.Contains("m-btn--icon");

            // Assert
            Assert.IsTrue(hasIconClass);
        }

        [TestMethod]
        public void RenderButtonWithLarge()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Large, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLargeClass = classes.Contains("m-size--large");

            // Assert
            Assert.IsTrue(hasLargeClass);
        }

        [TestMethod]
        public void RenderButtonWithLeft()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Left, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLeftClass = classes.Contains("m-btn--left");

            // Assert
            Assert.IsTrue(hasLeftClass);
        }

        [TestMethod]
        public void RenderButtonWithLight()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderButtonWithLoading()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Loading, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoadingClass = classes.Contains("m-btn--loading");

            // Assert
            Assert.IsTrue(hasLoadingClass);
        }

        [TestMethod]
        public void RenderWithMaxHeight()
        {
            // Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(p => p.MaxHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-btn");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMaxWidth()
        {
            // Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(p => p.MaxWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-btn");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-width: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinHeight()
        {
            // Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(p => p.MinHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-btn");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinWidth()
        {
            // Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(p => p.MinWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-btn");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-width: 100px", style);
        }

        [TestMethod]
        public void RenderButtonWithOutlined()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasoutlinedClass = classes.Contains("m-btn--outlined");

            // Assert
            Assert.IsTrue(hasoutlinedClass);
        }

        [TestMethod]
        public void RenderButtonWithPlain()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Plain, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPlainClass = classes.Contains("m-btn--plain");

            // Assert
            Assert.IsTrue(hasPlainClass);
        }

        [TestMethod]
        public void RenderButtonWithRight()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Right, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRightClass = classes.Contains("m-btn--right");

            // Assert
            Assert.IsTrue(hasRightClass);
        }

        [TestMethod]
        public void RenderButtonWithRipple()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Ripple, true);

            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRippleClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasRippleClass);
        }

        [TestMethod]
        public void RenderButtonWithRounded()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-btn--rounded");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderButtonWithShaped()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Shaped, true);

            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-btn");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderButtonWithSmall()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Small, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSmallClass = classes.Contains("m-size--small");

            // Assert
            Assert.IsTrue(hasSmallClass);
        }

        [TestMethod]
        public void RenderButtonWithText()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Text, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTextClass = classes.Contains("m-btn--text");

            // Assert
            Assert.IsTrue(hasTextClass);
        }

        [TestMethod]
        public void RenderButtonWithTile()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-btn--tile");

            // Assert
            Assert.IsTrue(hasTileClass);
        }

        [TestMethod]
        public void RenderButtonWithTop()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.Top, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTopClass = classes.Contains("m-btn--top");

            // Assert
            Assert.IsTrue(hasTopClass);
        }

        [TestMethod]
        public void RenderButtonWithXLarge()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.XLarge, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasXLargeClass = classes.Contains("m-size--x-large");

            // Assert
            Assert.IsTrue(hasXLargeClass);
        }

        [TestMethod]
        public void RenderButtonWithXSmall()
        {
            //Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.XSmall, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasXSmallClass = classes.Contains("m-size--x-small");

            // Assert
            Assert.IsTrue(hasXSmallClass);
        }

        [TestMethod]
        public void RenderButtonAndOnClick()
        {
            // Arrange
            var times = 0;
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.OnClick, args =>
                {
                    times++;
                });
            });

            // Act
            var buttonElement = cut.Find("button");
            buttonElement.Click();

            // Assert
            Assert.AreEqual(1, times);
        }

        [TestMethod]
        public void RenderWithLoaderContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.LoaderContent, "<span>Hello world</span>");
                props.Add(button => button.Loading, true);
            });
            var contentDiv = cut.Find(".m-btn__loader");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MButton>(props =>
            {
                props.Add(button => button.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-btn__content");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

    }
}

