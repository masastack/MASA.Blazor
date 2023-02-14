using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Bunit;

namespace Masa.Blazor.Test.Table
{
    [TestClass]
    public class MTableTests:TestBase
    {
        //[TestMethod]
        //public void RendeListWithDark()
        //{
        //    //Act
        //    var cut = RenderComponent<MTable<string>>(props =>
        //    {
        //        props.Add(listgroup => listgroup.Dark, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasDarkClass = classes.Contains("theme--table");

        //    // Assert
        //    Assert.IsTrue(hasDarkClass);
        //}

        //[TestMethod]
        //public void RenderSystemBarWithLight()
        //{
        //    //Act
        //    var cut = RenderComponent<MTable<string>>(props =>
        //    {
        //        props.Add(systembar => systembar.FixedHeader, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasLightClass = classes.Contains("m-data-table--
        //    fixed-header");

        //    // Assert
        //    Assert.IsTrue(hasLightClass);
        //}
    }
}
