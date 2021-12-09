using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Chip
{
    [TestClass]
    public class MChipTests:TestBase
    {
        [TestMethod]
        public void RenderChipWithActive()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Active, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasActiveClass);
        }

        [TestMethod]
        public void RenderChipWithActiveClass()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                string active = "activeclass";
                props.Add(chip => chip.ActiveClass, active);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClassClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasActiveClassClass);
        }

        [TestMethod]
        public void RenderChipWithClose()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Close, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCloseClass = classes.Contains("m-chip--removable");

            // Assert
            Assert.IsTrue(hasCloseClass);
        }

        [TestMethod]
        public void RenderChipWithCloseIcon()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                string icon = "mdi-star";
                props.Add(chip => chip.CloseIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCloseIconClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasCloseIconClass);
        }

        [TestMethod]
        public void RenderChipWithCloseLabel()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                string closelabel = "mdi-star";
                props.Add(chip => chip.CloseLabel, closelabel);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCloseLabelClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasCloseLabelClass);
        }

        [TestMethod]
        public void RenderChipWithColor()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                string color = "chip";
                props.Add(chip => chip.Color, color);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColorClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasColorClass);
        }

        [TestMethod]
        public void RenderChipWithDark()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderChipWithLight()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderChipWithDisabled()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-chip--disabled");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderChipWithFilter()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Filter, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFilterClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasFilterClass);
        }

        [TestMethod]
        public void RenderChipWithFilterIcon()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                string icon = "mdi-star";
                props.Add(chip => chip.FilterIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFilterIconClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasFilterIconClass);
        }

        [TestMethod]
        public void RenderChipWithHref()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                string href = "href";
                props.Add(chip => chip.Href, href);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHrefClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasHrefClass);
        }

        [TestMethod]
        public void RenderChipWithLabel()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Label, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFilterClass = classes.Contains("m-chip--label");

            // Assert
            Assert.IsTrue(hasFilterClass);
        }

        [TestMethod]
        public void RenderChipWithLarge()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Large, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLargeClass = classes.Contains("m-size--large");

            // Assert
            Assert.IsTrue(hasLargeClass);
        }

        [TestMethod]
        public void RenderChipWithLink()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Link, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLargeClass = classes.Contains("m-size");

            // Assert
            Assert.IsTrue(hasLargeClass);
        }

        [TestMethod]
        public void RenderChipWithOutlined()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-chip--outlined");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderChipWithPill()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Pill, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPillClass = classes.Contains("m-chip--pill");

            // Assert
            Assert.IsTrue(hasPillClass);
        }

        [TestMethod]
        public void RenderChipWithRipple()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Ripple, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRippleClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasRippleClass);
        }

        [TestMethod]
        public void RenderChipWithSmall()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Small, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSmallClass = classes.Contains("m-size--small");

            // Assert
            Assert.IsTrue(hasSmallClass);
        }

        [TestMethod]
        public void RenderChipWithTag()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                string tag = "chip";
                props.Add(chip => chip.Tag, tag);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTagClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasTagClass);
        }

        [TestMethod]
        public void RenderChipWithTarget()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                string target = "chip";
                props.Add(chip => chip.Target, target);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTargetClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasTargetClass);
        }

        [TestMethod]
        public void RenderChipWithTextColor()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                string textcolor = "chip";
                props.Add(chip => chip.TextColor, textcolor);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTextColorClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasTextColorClass);
        }

        [TestMethod]
        public void RenderChipWithValue()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                string value = "chip";
                props.Add(chip => chip.Value, value);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderChipWithXSmall()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.XSmall, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasXSmallClass = classes.Contains("m-size--x-small");

            // Assert
            Assert.IsTrue(hasXSmallClass);
        }

        [TestMethod]
        public void RenderChipWithXLarge()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.XLarge, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasXLargeClass = classes.Contains("m-size--x-large");

            // Assert
            Assert.IsTrue(hasXLargeClass);
        }

        [TestMethod]
        public void RenderChipWithDraggable()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Draggable, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDraggableClass = classes.Contains("m-chip--draggable");

            // Assert
            Assert.IsTrue(hasDraggableClass);
        }

        [TestMethod]
        public void RenderButtonAndonClick()
        {
            // Arrange
            var times = 0;
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.OnClick, args =>
                {
                    times++;
                });
            });

            // Act
            var chipElement = cut.Find("span");
            chipElement.Click();

            // Assert
            Assert.AreEqual(1, times);
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(checkbox => checkbox.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-chip__content");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
