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
        //        props.Add(tabitem => tabitem.Disabled, true);
        //    });
        //    var classes = cut.Instance.CssProvider.GetClass();
        //    var hasDisabledClass = classes.Contains("m-tab");

        //    // Assert
        //    Assert.IsTrue(hasDisabledClass);//v-tabs
        //}

        //[TestMethod]
        //public void RenderWithChildContent()
        //{
        //    // Arrange & Act
        //    var cut = RenderComponent<MTabItem>(props =>
        //    {
        //        props.Add(tabitem => tabitem.ChildContent, "<span>Hello world</span>");
        //    });
        //    var contentDiv = cut.Find(".m-card__text");

        //    // Assert
        //    contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        //}
    }
}
