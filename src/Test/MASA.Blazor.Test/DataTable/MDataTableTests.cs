using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.DataTable
{
    [TestClass]
    public class MDataTableTests:TestBase
    {
        [TestMethod]
        public void RendeDataTableWithEveryItem()
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
        public void RendeDataTableNoWithEveryItem()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.EveryItem, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasEveryItemClass = classes.Contains("m-data-table");

            // Assert
            Assert.IsTrue(hasEveryItemClass);
        }

        [TestMethod]
        public void RendeDataTableWithSomeItems()
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
        public void RendeDataTableNoWithSomeItems()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.SomeItems, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSomeItemsClass = classes.Contains("m-data-table");

            // Assert
            Assert.IsTrue(hasSomeItemsClass);
        }

        [TestMethod]
        public void RendeDataTableWithShowGroupBy()
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
        public void RendeDataTableNoWithShowGroupBy()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.ShowGroupBy, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowGroupByClass = classes.Contains("m-data-table");

            // Assert
            Assert.IsTrue(hasShowGroupByClass);
        }

        [TestMethod]
        public void RendeDataTableWithSingleSelect()
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
        public void RendeDataTableNoWithSingleSelect()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.SingleSelect, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleSelectClass = classes.Contains("m-data-table");

            // Assert
            Assert.IsTrue(hasSingleSelectClass);
        }

        [TestMethod]
        public void RendeDataTableWithDisableSort()
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
        public void RendeDataTableNoWithDisableSort()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.DisableSort, false);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisableSortClass = classes.Contains("m-data-table");

            // Assert
            Assert.IsTrue(hasDisableSortClass);
        }

        //[TestMethod]
        //public void RenderWithDataTableSelectContent()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MDataTableHeader>(props =>
        //    {
        //        props.Add(datatableheader => datatableheader.DataTableSelectContent, "<span>Hello world</span>");
        //    });
        //    var contentDiv = cut.Find(".m-data-table__checkbox");

        //    // Assert
        //    contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        //}

        //[TestMethod]
        //public void RenderWithHeaderColContent()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MDataTableHeader>(props =>
        //    {
        //        props.Add(datatableheader => datatableheader.HeaderColContent,context=> "<span>Hello world</span>");
        //    });
        //    var contentDiv = cut.Find(".m-data-table__wrapper");

        //    // Assert
        //    contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        //}
    }
}
