using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Masa.Blazor.Test.Table
{
    [TestClass]
    public class MTableColTests:TestBase
    {
        [TestMethod]
        public void RendeMTableColWithLightsOut()
        {
            //Act
            var cut = RenderComponent<MTableCol>(props =>
            {
                props.Add(tablecol => tablecol.Divider, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDividerClass = classes.Contains("m-data-table__divider");
            // Assert
            Assert.IsTrue(hasDividerClass);
        }
    }
}
