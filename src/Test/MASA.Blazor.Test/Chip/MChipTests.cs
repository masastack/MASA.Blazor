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
        public void RenderChipNoWithDisabled()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Disabled, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasDisabledClass);
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
            var hasLabelClass = classes.Contains("m-chip--label");

            // Assert
            Assert.IsTrue(hasLabelClass);
        }

        [TestMethod]
        public void RenderChipNoWithLabel()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Label, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLabelClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasLabelClass);
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
        public void RenderChipNoWithClose()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Close, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCloseClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasCloseClass);
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
        public void RenderChipNoWithXSmall()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.XSmall, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasXSmallClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasXSmallClass);
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
        public void RenderChipNoWithSmall()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Small, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSmallClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasSmallClass);
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
        public void RenderChipNoWithLarge()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Large, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLargeClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasLargeClass);
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
        public void RenderChipNoWithXLarge()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.XLarge, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasXLargeClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasXLargeClass);
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
        public void RenderChipNoWithOutlined()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Outlined, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
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
        public void RenderChipNoWithDraggable()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Draggable, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDraggableClass = classes.Contains("m-chip");

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
        public void RenderChipNoWithDark()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-chip");

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
        public void RenderChipNoWithLight()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasLightClass);
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
        public void RenderChipNoWithPill()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Pill, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPillClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasPillClass);
        }

        [TestMethod]
        public void RenderChipWithIsActive()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.IsActive, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIsActiveClass = classes.Contains("m-chip--active");

            // Assert
            Assert.IsTrue(hasIsActiveClass);
        }

        [TestMethod]
        public void RenderChipNoWithIsActive()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.IsActive, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIsActiveClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasIsActiveClass);
        }

        [TestMethod]
        public void RendeChipWithFilter()
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
        public void RendeChipNoWithFilter()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Filter, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFilterClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasFilterClass);
        }

        [TestMethod]
        public void RendeChipWithLink()
        {
            //Act
            var cut = RenderComponent<MChip>(props =>
            {
                props.Add(chip => chip.Link, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFilterClass = classes.Contains("m-chip");

            // Assert
            Assert.IsTrue(hasFilterClass);
        }
    }
}
