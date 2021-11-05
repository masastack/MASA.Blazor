using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Test.Checkbox
{
    [TestClass]
    public class MSimpleCheckboxTests:TestBase
    {
        [TestMethod]
        public void RenderAlertWithWithLight()
        {
            //Act
            var cut = RenderComponent<MSimpleCheckbox>(props =>
            {
                props.Add(breadcrumbs => breadcrumbs.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("m-simple-checkbox--disabled");

            // Assert
            Assert.IsTrue(hasLightClass);
        }
    }
}
