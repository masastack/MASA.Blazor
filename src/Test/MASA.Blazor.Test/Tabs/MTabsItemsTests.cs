using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MASA.Blazor.Test.Tabs
{
    [TestClass]
    public class MTabsItemsTests:TestBase
    {
        [TestMethod]
        public void RenderTabsItemsWithContinuous()
        {
            //Act
            var cut = RenderComponent<MTabsItems>(props =>
            {
                props.Add(tabsitems => tabsitems.Continuous, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasContinuousClass = classes.Contains("m-tabs-items");

            // Assert
            Assert.IsTrue(hasContinuousClass);
        }

        [TestMethod]
        public void RenderTabsItemsWithDark()
        {
            //Act
            var cut = RenderComponent<MTabsItems>(props =>
            {
                props.Add(tabsitems => tabsitems.Dark, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-tabs-items");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderTabsItemsWithLight()
        {
            //Act
            var cut = RenderComponent<MTabsItems>(props =>
            {
                props.Add(tabsitems => tabsitems.Light, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-tabs-items");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderTabsItemsWithMandatory()
        {
            //Act
            var cut = RenderComponent<MTabsItems>(props =>
            {
                props.Add(tabsitems => tabsitems.Mandatory, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMandatoryClass = classes.Contains("m-tabs-items");

            // Assert
            Assert.IsTrue(hasMandatoryClass);
        }

        [TestMethod]
        public void RenderTabsItemsWithMultiple()
        {
            //Act
            var cut = RenderComponent<MTabsItems>(props =>
            {
                props.Add(tabsitems => tabsitems.Multiple, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMultipleClass = classes.Contains("m-tabs-items");

            // Assert
            Assert.IsTrue(hasMultipleClass);
        }

        [TestMethod]
        public void RenderTabsItemsWithMax()
        {
            //Act
            var cut = RenderComponent<MTabsItems>(props =>
            {
                props.Add(tabsitems => tabsitems.Max, 24);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMaxClass = classes.Contains("m-tabs-items");

            // Assert
            Assert.IsTrue(hasMaxClass);
        }

        [TestMethod]
        public void RenderTabsItemsWithActiveClass()
        {
            //Act
            var cut = RenderComponent<MTabsItems>(props =>
            {
                string active = "active-class";
                props.Add(tabsitems => tabsitems.ActiveClass, active);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasActiveClassClass = classes.Contains("m-tabs-items");

            // Assert
            Assert.IsTrue(hasActiveClassClass);
        }

        [TestMethod]
        public void RenderTabsItemsWithNextIcon()
        {
            //Act
            var cut = RenderComponent<MTabsItems>(props =>
            {
                string icon = "mdi-star";
                props.Add(tabsitems => tabsitems.NextIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNextIconClass = classes.Contains("m-tabs-items");

            // Assert
            Assert.IsTrue(hasNextIconClass);
        }

        [TestMethod]
        public void RenderTabsItemsWithPrevIcon()
        {
            //Act
            var cut = RenderComponent<MTabsItems>(props =>
            {
                string icon = "mdi-star";
                props.Add(tabsitems => tabsitems.PrevIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPrevIconClass = classes.Contains("m-tabs-items");

            // Assert
            Assert.IsTrue(hasPrevIconClass);
        }

        [TestMethod]
        public void RenderTabsItemsWithReverse()
        {
            //Act
            var cut = RenderComponent<MTabsItems>(props =>
            {
                props.Add(tabsitems => tabsitems.Reverse, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReverseClass = classes.Contains("m-tabs-items");

            // Assert
            Assert.IsTrue(hasReverseClass);
        }

        [TestMethod]
        public void RenderTabsItemsWithShowArrows()
        {
            //Act
            var cut = RenderComponent<MTabsItems>(props =>
            {
                props.Add(tabsitems => tabsitems.ShowArrows, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowArrowsClass = classes.Contains("m-tabs-items");

            // Assert
            Assert.IsTrue(hasShowArrowsClass);
        }

        [TestMethod]
        public void RenderTabsItemsWithShowArrowsOnHover()
        {
            //Act
            var cut = RenderComponent<MTabsItems>(props =>
            {
                props.Add(tabsitems => tabsitems.ShowArrowsOnHover, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowArrowsOnHoverClass = classes.Contains("m-tabs-items");

            // Assert
            Assert.IsTrue(hasShowArrowsOnHoverClass);
        }

        [TestMethod]
        public void RenderTabsItemsWithValue()
        {
            //Act
            var cut = RenderComponent<MTabsItems>(props =>
            {
                string icon = "mdi-star";
                props.Add(tabsitems => tabsitems.Value, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-tabs-items");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderTabsItemsWithVertical()
        {
            //Act
            var cut = RenderComponent<MTabsItems>(props =>
            {
                props.Add(tabsitems => tabsitems.Vertical, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasVerticalClass = classes.Contains("m-tabs-items");

            // Assert
            Assert.IsTrue(hasVerticalClass);
        }
    }
}
