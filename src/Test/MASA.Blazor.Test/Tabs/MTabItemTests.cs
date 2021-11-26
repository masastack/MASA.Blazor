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
    public class MTabItemTests:TestBase
    {
        //[TestMethod]
        //public void RenderTabItemWithDisabled()
        //{
        //    //Act
        //    var cut = RenderComponent<MTabItem>(props =>
        //    {
        //        props.Add(tabitem => tabitem.Disabled, false);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasDisabledClass = classes.Contains("m-slide-group__next--disabled");

        //    // Assert
        //    Assert.IsTrue(hasDisabledClass);
        //}

        //[TestMethod]
        //public void RenderWithChildContent()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MTabItem>(props =>
        //    {
        //        props.Add(tabitem => tabitem.ChildContent, "<span>Hello world</span>");
        //    });
        //    var contentDiv = cut.Find(".m-tabs-bar__content");

        //    // Assert
        //    contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        //}
    }
}
