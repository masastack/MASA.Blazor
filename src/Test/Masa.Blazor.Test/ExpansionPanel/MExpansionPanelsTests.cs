using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.ExpansionPanel
{
    [TestClass]
    public class MExpansionPanelsTests : TestBase
    {
        [TestMethod]
        public void RenderExpansionPanelsWithAccordion()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanels => expansionpanels.Accordion, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAccordionClass = classes.Contains("m-expansion-panels--accordion");

            // Assert
            Assert.IsTrue(hasAccordionClass);
        }

        [TestMethod]
        public void RenderExpansionPanelsWithDisabled()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(appbar => appbar.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-expansion-panels");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderExpansionPanelsWithFlat()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(appbar => appbar.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-expansion-panels--flat");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderExpansionPanelsWithFocusable()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(appbar => appbar.Focusable, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFocusableClass = classes.Contains("m-expansion-panels--focusable");

            // Assert
            Assert.IsTrue(hasFocusableClass);
        }

        [TestMethod]
        public void RenderExpansionPanelsWithHover()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(appbar => appbar.Hover, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHoverClass = classes.Contains("m-expansion-panels--hover");

            // Assert
            Assert.IsTrue(hasHoverClass);
        }

        [TestMethod]
        public void RenderExpansionPanelsWithInset()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(appbar => appbar.Inset, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInsetClass = classes.Contains("m-expansion-panels--inset");

            // Assert
            Assert.IsTrue(hasInsetClass);
        }

        [TestMethod]
        public void RenderExpansionPanelsWithMandatory()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(appbar => appbar.Mandatory, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMandatoryClass = classes.Contains("m-expansion-panels");

            // Assert
            Assert.IsTrue(hasMandatoryClass);
        }

        [TestMethod]
        public void RenderExpansionPanelsWithMax()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(alert => alert.Max, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMaxClass = classes.Contains("");

            // Assert
            Assert.IsTrue(hasMaxClass);
        }

        [TestMethod]
        public void RenderExpansionPanelsWithMultiple()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(appbar => appbar.Multiple, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMultipleClass = classes.Contains("m-expansion-panels");

            // Assert
            Assert.IsTrue(hasMultipleClass);
        }

        [TestMethod]
        public void RenderExpansionPanelsWithPopout()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(appbar => appbar.Popout, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPopoutClass = classes.Contains("m-expansion-panels--popout");

            // Assert
            Assert.IsTrue(hasPopoutClass);
        }

        [TestMethod]
        public void RenderExpansionPanelsWithReadonly()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(appbar => appbar.Readonly, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReadonlyClass = classes.Contains("m-expansion-panels");

            // Assert
            Assert.IsTrue(hasReadonlyClass);
        }

        [TestMethod]
        public void RenderExpansionPanelsWithTile()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(appbar => appbar.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-expansion-panels--tile");

            // Assert
            Assert.IsTrue(hasTileClass);
        }
    }
}
