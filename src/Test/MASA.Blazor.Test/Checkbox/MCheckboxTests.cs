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
    public class MCheckboxTests:TestBase
    {
        [TestMethod]
        public void RenderCheckboxWithIndeterminate()
        {
            //Act
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(checkbox => checkbox.Indeterminate, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIndeterminateClass = classes.Contains("m-input--indeterminate");

            // Assert
            Assert.IsTrue(hasIndeterminateClass);
        }

        [TestMethod]
        public void RenderNormal()
        {
            // Arrange & Act
            var cut = RenderComponent<MCheckbox>();
            var inputDiv = cut.Find("div");

            // Assert
            Assert.AreEqual(6, inputDiv.ClassList.Length);
            Assert.IsTrue(inputDiv.ClassList.Contains("m-input"));
            Assert.IsTrue(inputDiv.ClassList.Contains("theme--light"));
        }

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
