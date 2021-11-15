using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Responsive
{
    [TestClass]
    public class MResponsiveTests : TestBase
    {
        [TestMethod]
        public void RenderResponsive()
        {
            //Act
            var cut = RenderComponent<MResponsive>(props =>
            {
                props.Add(rating => rating.AspectRatio, 16/9D);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReadonlyClass = classes.Contains("m-rating--readonly");
            // Assert
            Assert.IsTrue(hasReadonlyClass);
        }
    }
}
