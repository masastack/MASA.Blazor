using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using BlazorComponent;
using Moq;

namespace MASA.Blazor.Test.Treeview
{
    [TestClass]
    public class MTreeviewTests:TestBase
    {
        [TestMethod]
        public void RendeListWithDark()
        {
            ////Act
            //var cut = RenderComponent<MTreeview<string, string>>(props =>
            // {
            //     props.Add(treeview => treeview.Hoverable, true);
            //     props.Add(treeview => treeview.ItemKey, item => item);
            // });
            //var classes = cut.Instance.CssProvider.GetClass();
            //var hasDarkClass = classes.Contains("m-treeview--hoverable");

            //// Assert
            //Assert.IsTrue(hasDarkClass);
        }
    }
}
