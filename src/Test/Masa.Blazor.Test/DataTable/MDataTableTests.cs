using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.DataTable
{
    [TestClass]
    public class MDataTableTests : TestBase
    {
        //[TestMethod]
        //public void RenderDataTableWithDense()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.Dense, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasDenseClass = classes.Contains("m-data-table--dense");

        //    // Assert
        //    Assert.IsTrue(hasDenseClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithDisableFiltering()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.DisableFiltering, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasDisableFilteringClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasDisableFilteringClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithDisablePagination()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.DisablePagination, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasDisablePaginationClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasDisablePaginationClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithDisableSort()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.DisableSort, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasDisableSortClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasDisableSortClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithFixedHeader()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.FixedHeader, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasFixedHeaderClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasFixedHeaderClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithHeadersLength()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.HeadersLength, 10);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasHeadersLengthClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasHeadersLengthClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithHeight()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.Height, 10);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasHeightClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasHeightClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithHideDefaultFooter()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.HideDefaultFooter, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasFixedHeaderClass = classes.Contains("m-data-table__wrapper");

        //    // Assert
        //    Assert.IsTrue(hasFixedHeaderClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithHideDefaultHeader()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.HideDefaultHeader, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasHideDefaultHeaderClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasHideDefaultHeaderClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithItemsPerPage()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.ItemsPerPage, 10);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasItemsPerPageClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasItemsPerPageClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithLoaderHeight()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.LoaderHeight, 10);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasLoaderHeightClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasLoaderHeightClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithLoading()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.Loading, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasLoadingClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasLoadingClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithMultiSort()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.MultiSort, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasMultiSortClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasMultiSortClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithMustSort()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.MustSort, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasMustSortClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasMustSortClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithPage()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.Page, 1);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasPageClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasPageClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithServerItemsLength()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.ServerItemsLength, -1);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasServerItemsLengthClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasServerItemsLengthClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithShowExpand()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.ShowExpand, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasShowExpandClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasShowExpandClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithShowGroupBy()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.ShowGroupBy, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasShowGroupByClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasShowGroupByClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithShowSelect()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.ShowSelect, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasShowSelectClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasShowSelectClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithSingleExpand()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.SingleExpand, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasSingleExpandClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasSingleExpandClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithSingleSelect()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.SingleSelect, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasSingleSelectClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasSingleSelectClass);
        //}

        //[TestMethod]
        //public void RenderDataTableWithSortDesc()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MDataTable<string>>(props =>
        //    {
        //        props.Add(datatable => datatable.SortDesc, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasSortDescClass = classes.Contains("m-data-table");

        //    // Assert
        //    Assert.IsTrue(hasSortDescClass);
        //}
    }
}
