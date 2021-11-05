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
    public class MExpansionPanelTests:TestBase
    {
        [TestMethod]
        public void RenderWithTile()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanel => expansionpanel.Tile, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-expansion-panels--tile");

            // Assert
            Assert.IsTrue(hasTileClass);
        }

        [TestMethod]
        public void RenderWithFlat()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanel => expansionpanel.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-expansion-panels--flat");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderWithAccordion()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanel => expansionpanel.Accordion, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAccordionClass = classes.Contains("m-expansion-panels--accordion");

            // Assert
            Assert.IsTrue(hasAccordionClass);
        }

        [TestMethod]
        public void RenderWithFocusable()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanel => expansionpanel.Focusable, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFocusableClass = classes.Contains("m-expansion-panels--focusable");

            // Assert
            Assert.IsTrue(hasFocusableClass);
        }
    }
}
