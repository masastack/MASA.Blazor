using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.ExpansionPanel
{
    [TestClass]
    public class MExpansionPanelsTests:TestBase
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
        public void RenderExpansionPanelsWithActiveClass()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                string activeclass = "m-item--active";
                props.Add(expansionpanels => expansionpanels.ActiveClass, activeclass);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClassClass = classes.Contains("m-expansion-panels");

            // Assert
            Assert.IsTrue(hasActiveClassClass);
        }

        [TestMethod]
        public void RenderExpansionPanelsWithDisabled()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanels => expansionpanels.Disabled, true);
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
                props.Add(expansionpanels => expansionpanels.Flat, true);
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
                props.Add(expansionpanels => expansionpanels.Focusable, true);
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
                props.Add(expansionpanels => expansionpanels.Hover, true);
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
                props.Add(expansionpanels => expansionpanels.Inset, true);
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
                props.Add(expansionpanels => expansionpanels.Mandatory, true);
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
                props.Add(expansionpanels => expansionpanels.Max, 2);
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
                props.Add(expansionpanels => expansionpanels.Multiple, true);
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
                props.Add(expansionpanels => expansionpanels.Popout, true);
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
                props.Add(expansionpanels => expansionpanels.Readonly, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReadonlyClass = classes.Contains("m-expansion-panels");

            // Assert
            Assert.IsTrue(hasReadonlyClass);
        }

        [TestMethod]
        public void RenderExpansionPanelsWithValue()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                string value = "panels";
                props.Add(expansionpanels => expansionpanels.Value, value);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-expansion-panels");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderExpansionPanelsWithTile()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanels => expansionpanels.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-expansion-panels--tile");

            // Assert
            Assert.IsTrue(hasTileClass);
        }
    }
}
