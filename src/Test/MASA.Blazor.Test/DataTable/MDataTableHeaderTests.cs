using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MASA.Blazor.Test.DataTable
{
    [TestClass]
    public  class MDataTableHeaderTests:TestBase
    {
        [TestMethod]
        public void RenderDataTableHeaderWithDisableSort()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.DisableSort, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisableSortClass = classes.Contains("m-data-table-header");

            // Assert
            Assert.IsTrue(hasDisableSortClass);
        }

        [TestMethod]
        public void RenderDataTableHeaderWithEveryItem()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.EveryItem, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasEveryItemClass = classes.Contains("m-data-table-header");

            // Assert
            Assert.IsTrue(hasEveryItemClass);
        }

        [TestMethod]
        public void RenderDataTableHeaderWithShowGroupBy()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.ShowGroupBy, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowGroupByClass = classes.Contains("m-data-table-header");

            // Assert
            Assert.IsTrue(hasShowGroupByClass);
        }

        [TestMethod]
        public void RenderDataTableHeaderWithSingleSelect()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.SingleSelect, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleSelectClass = classes.Contains("m-data-table-header");

            // Assert
            Assert.IsTrue(hasSingleSelectClass);
        }

        [TestMethod]
        public void RenderDataTableHeaderWithSomeItems()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.SomeItems, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSomeItemsClass = classes.Contains("m-data-table-header");

            // Assert
            Assert.IsTrue(hasSomeItemsClass);
        }

        [TestMethod]
        public void RenderDataTableHeaderWithCheckboxColor()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                string color = "color";
                props.Add(datatableheader => datatableheader.CheckboxColor, color);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCheckboxColorClass = classes.Contains("m-data-table-header");

            // Assert
            Assert.IsTrue(hasCheckboxColorClass);
        }

        [TestMethod]
        public void RenderDataTableHeaderWithSortIcon()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                string icon = "mdi-star";
                props.Add(datatableheader => datatableheader.SortIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSortIconClass = classes.Contains("m-data-table-header");

            // Assert
            Assert.IsTrue(hasSortIconClass);
        }
    }
}
