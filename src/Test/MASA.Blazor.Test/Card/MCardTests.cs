using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Card
{
    [TestClass]
    public class MCardTests:TestBase
    {
        [TestMethod]
        public void RenderCardWithActiveClass()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                string activeclass = "card";
                props.Add(card => card.ActiveClass, activeclass);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClassClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasActiveClassClass);
        }

        [TestMethod]
        public void RenderCardWithDark()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderCardWithColor()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                string color = "card";
                props.Add(card => card.Color, color);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColorClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasColorClass);
        }

        [TestMethod]
        public void RenderCardWithDisabled()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-card--disabled");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderCardWithElevation()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(alert => alert.Elevation, 24);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasElevationClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasElevationClass);
        }

        [TestMethod]
        public void RenderCardWithFlat()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-card--flat");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-card");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px", style);
        }

        [TestMethod]
        public void RenderWithWidth()
        {
            // Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(p => p.Width, 100);
            });
            var inputSlotDiv = cut.Find(".m-card");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("width: 100px", style);
        }

        [TestMethod]
        public void RenderCardWithHover()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Hover, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHoverClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasHoverClass);
        }

        [TestMethod]
        public void RenderCardWithHref()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                string href = "card";
                props.Add(card => card.Href, href);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHrefClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasHrefClass);
        }

        [TestMethod]
        public void RenderCardWithImg()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                string img = "card";
                props.Add(card => card.Img, img);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasImgClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasImgClass);
        }

        [TestMethod]
        public void RenderCardWithLight()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderCardWithLink()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Link, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLinkClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasLinkClass);
        }

        [TestMethod]
        public void RenderCardWithLoaderHeight()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.LoaderHeight, 4);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoaderHeightClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasLoaderHeightClass);
        }

        [TestMethod]
        public void RenderCardWithLoading()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Loading, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoadingClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasLoadingClass);
        }

        [TestMethod]
        public void RenderWithMaxHeight()
        {
            // Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(p => p.MaxHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-card");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMaxWidth()
        {
            // Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(p => p.MaxWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-card");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-width: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinHeight()
        {
            // Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(p => p.MinHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-card");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-height: 100px", style);
        }

        [TestMethod]
        public void RenderWithMinWidth()
        {
            // Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(p => p.MinWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-card");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-width: 100px", style);
        }

        [TestMethod]
        public void RenderCardWithOutlined()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-sheet--outlined");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderCardWithRaised()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Raised, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRaisedClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasRaisedClass);
        }

        [TestMethod]
        public void RenderCardWithRipple()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Ripple, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRippleClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasRippleClass);
        }

        [TestMethod]
        public void RenderCardWithRounded()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderCardWithShaped()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderCardWithTag()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                string tag = "card";
                props.Add(card => card.Tag, tag);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTagClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasTagClass);
        }

        [TestMethod]
        public void RenderCardWithTarget()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                string target = "card";
                props.Add(card => card.Target, target);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTargetClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasTargetClass);
        }

        [TestMethod]
        public void RenderCardWithTile()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-card");

            // Assert
            Assert.IsTrue(hasTileClass);
        }

        [TestMethod]
        public void RenderCardAndOnClick()
        {
            // Arrange
            var times = 0;
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.OnClick, args =>
                {
                    times++;
                });
            });

            // Act
            var cardElement = cut.Find(".m-card");
            cardElement.Click();

            // Assert
            Assert.AreEqual(1, times);
        }

        [TestMethod]
        public void RenderWithChildContentt()
        {
            // Arrange & Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-card");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
