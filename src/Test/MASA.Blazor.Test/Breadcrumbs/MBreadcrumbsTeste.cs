using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Breadcrumbs
{
    [TestClass]
    public class MBreadcrumbsTeste:TestBase
    {
        [TestMethod]
        public void RenderButtonWithLarge()
        {
            //Act
            var cut = RenderComponent<MBreadcrumbs>(props =>
            {
                props.Add(breadcrumbs => breadcrumbs.Large, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLargeClass = classes.Contains("m-breadcrumbs--large");

            // Assert
            Assert.IsTrue(hasLargeClass);
        }
    }
}
