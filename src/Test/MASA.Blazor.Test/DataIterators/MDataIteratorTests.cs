using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bunit;

namespace MASA.Blazor.Test.DataIterators
{
    [TestClass]
    public class MDataIteratorTests:TestBase
    {
        [TestMethod]
        public void RenderDataIteratorWithDisableFiltering()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.DisableFiltering, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisableFilteringClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasDisableFilteringClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithDisablePagination()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.DisablePagination, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisablePaginationClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasDisablePaginationClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithDisableSort()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.DisableSort, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisableSortClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasDisableSortClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithHideDefaultFooter()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.HideDefaultFooter, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideDefaultFooterClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasHideDefaultFooterClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithItemsPerPage()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.ItemsPerPage, 10);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasItemsPerPageClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasItemsPerPageClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithLoading()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.Loading, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoadingClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasLoadingClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithMultiSort()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.MultiSort, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMultiSortClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasMultiSortClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithMustSort()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.MustSort, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMustSortClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasMustSortClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithPage()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.Page, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPageClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasPageClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithServerItemsLength()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.ServerItemsLength, -1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasServerItemsLengthClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasServerItemsLengthClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithSingleExpand()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.SingleExpand, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleExpandClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasSingleExpandClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithSingleSelect()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.SingleSelect, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleSelectClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasSingleSelectClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithSortDesc()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.SortDesc, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSortDescClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasSortDescClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithGroupBy()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                string groupby = "en-US";
                props.Add(dataiterator => dataiterator.GroupBy, groupby);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasGroupByClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasGroupByClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithLoadingText()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                string Loading = "Loading... Please wait";
                props.Add(dataiterator => dataiterator.LoadingText, Loading);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoadingTextClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasLoadingTextClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithLocale()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                string locale = "en-US";
                props.Add(dataiterator => dataiterator.Locale, locale);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLocaleClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasLocaleClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithNoDataText()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                string nodatatext = "No data available";
                props.Add(dataiterator => dataiterator.NoDataText, nodatatext);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNoDataTextClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasNoDataTextClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithNoResultsText()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                string noresultstext = "No matching records found";
                props.Add(dataiterator => dataiterator.NoResultsText, noresultstext);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasNoResultsTextClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasNoResultsTextClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithSearch()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                string search = "search";
                props.Add(dataiterator => dataiterator.Search, search);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSearchClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasSearchClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithSortBy()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                string sortby = "sortby";
                props.Add(dataiterator => dataiterator.SortBy, sortby);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSortByClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasSortByClass);
        }
    }
}
