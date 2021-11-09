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
        public void RenderButtonWithOutlined()
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
        public void RenderButtonWithShaped()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-sheet--shaped");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderButtonWithFlat()
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
        public void RenderButtonWithHover()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Hover, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHoverClass = classes.Contains("m-card--hover");

            // Assert
            Assert.IsTrue(hasHoverClass);
        }

        [TestMethod]
        public void RenderButtonWithLink()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Link, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var haLinkClass = classes.Contains("m-card--link");

            // Assert
            Assert.IsTrue(haLinkClass);
        }

        [TestMethod]
        public void RenderButtonWithLoading()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Loading, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoadingClass = classes.Contains("m-card--loading");

            // Assert
            Assert.IsTrue(hasLoadingClass);
        }

        [TestMethod]
        public void RenderButtonWithDisabled()
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
        public void RenderButtonWithRaised()
        {
            //Act
            var cut = RenderComponent<MCard>(props =>
            {
                props.Add(card => card.Raised, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRaisedClass = classes.Contains("m-card--disabled");

            // Assert
            Assert.IsTrue(hasRaisedClass);
        }

        [TestMethod]
        public void RenderCardWithWithDark()
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
        public void RenderCardWithWithLight()
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

        [TestMethod]
        public void RenderButtonAndonClick()
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
    }
}
