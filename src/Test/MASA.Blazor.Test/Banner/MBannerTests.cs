using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Banner
{
    [TestClass]
    public class MBannerTests:TestBase
    {
        [TestMethod]
        public void RenderButtonWithSingleLine()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.SingleLine, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleLineClass = classes.Contains("m-banner--single-line");

            // Assert
            Assert.IsTrue(hasSingleLineClass);
        }

        [TestMethod]
        public void RenderButtonWithValue()
        {
            //Act
            var cut = RenderComponent<MBanner>(props =>
            {
                props.Add(banner => banner.Value, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var noValueClass = ! classes.Contains("display:none");

            // Assert
            Assert.IsTrue(noValueClass);
        }

    }
}
