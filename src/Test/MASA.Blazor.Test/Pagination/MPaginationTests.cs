using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.Pagination
{
    [TestClass]
    public class MPaginationTests:TestBase
    {
        //[TestMethod]
        //public void RenderPaginationWithCircle()
        //{
        //    //Act
        //    JSInterop.Mode = JSRuntimeMode.Loose;
        //    var cut = RenderComponent<MPagination>(props =>
        //    {
        //        props.Add(pagination => pagination.Circle, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasCircleClass = classes.Contains("m-pagination");

        //    // Assert
        //    Assert.IsTrue(hasCircleClass);
        //}
    }
}
