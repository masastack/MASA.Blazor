using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Bunit;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Border
{
    [TestClass]
    public  class MBorderTests:TestBase
    {
        [TestMethod]
        public void RenderBorderWithRounded()
        {
            //Act
            var cut = RenderComponent<MBorder>(props =>
            {
                props.Add(border => border.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-border");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }
    }
}
