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
        public void RenderNoWithTile()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanel => expansionpanel.Tile, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTileClass = classes.Contains("m-expansion-panels");

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
        public void RenderNoWithFlat()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanel => expansionpanel.Flat, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-expansion-panels");

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
        public void RenderNoWithAccordion()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanel => expansionpanel.Accordion, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAccordionClass = classes.Contains("m-expansion-panels");

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

        [TestMethod]
        public void RenderNoWithFocusable()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanel => expansionpanel.Focusable, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFocusableClass = classes.Contains("m-expansion-panels");

            // Assert
            Assert.IsTrue(hasFocusableClass);
        }

        [TestMethod]
        public void RenderWithHover()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanel => expansionpanel.Hover, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHoverClass = classes.Contains("m-expansion-panels--hover");

            // Assert
            Assert.IsTrue(hasHoverClass);
        }

        [TestMethod]
        public void RenderNoWithHover()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanel => expansionpanel.Hover, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHoverClass = classes.Contains("m-expansion-panels");

            // Assert
            Assert.IsTrue(hasHoverClass);
        }

        [TestMethod]
        public void RenderWithInset()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanel => expansionpanel.Inset, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInsetClass = classes.Contains("m-expansion-panels--inset");

            // Assert
            Assert.IsTrue(hasInsetClass);
        }

        [TestMethod]
        public void RenderNoWithInset()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanel => expansionpanel.Inset, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasInsetClass = classes.Contains("m-expansion-panels");

            // Assert
            Assert.IsTrue(hasInsetClass);
        }

        [TestMethod]
        public void RenderWithPopout()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanel => expansionpanel.Popout, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPopoutClass = classes.Contains("m-expansion-panels--popout");

            // Assert
            Assert.IsTrue(hasPopoutClass);
        }

        [TestMethod]
        public void RenderNoWithPopout()
        {
            //Act
            var cut = RenderComponent<MExpansionPanels>(props =>
            {
                props.Add(expansionpanel => expansionpanel.Popout, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPopoutClass = classes.Contains("m-expansion-panels");

            // Assert
            Assert.IsTrue(hasPopoutClass);
        }

        //[TestMethod]
        //public void RenderMExpansionPanelHeaderAndonClick()
        //{
        //    // Arrange
        //    var times = 0;
        //    var cut = RenderComponent<MExpansionPanelHeader>(props =>
        //    {
        //        props.Add(systembar => systembar.OnClick, args =>
        //        {
        //            times++;
        //        });
        //    });

        //    // Act
        //    var buttonElement = cut.Find(".m-expansion-panel-header");
        //    buttonElement.Click();

        //    // Assert
        //    Assert.AreEqual(1, times);
        //}
    }
}
