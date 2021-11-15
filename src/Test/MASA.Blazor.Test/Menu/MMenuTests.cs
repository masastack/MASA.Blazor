using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.Menu
{
    [TestClass]
    public class MMenuTests:TestBase
    {
        //JS互操作
        //[TestMethod]
        //public void RendeMenuWithDark()
        //{
        //    //Act
        //    var cut = RenderComponent<MMenu>(props =>
        //    {
        //        props.Add(menu => menu.Dark, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasDarkClass = classes.Contains("theme--dark");

        //    // Assert
        //    Assert.IsTrue(hasDarkClass);
        //}
    }
}
