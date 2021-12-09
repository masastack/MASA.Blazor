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
        public void RenderSimpleCheckboxWithDark()
        {
            //Act
            var cut = RenderComponent<MSimpleCheckbox>(props =>
            {
                props.Add(simplecheckbox => simplecheckbox.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-simple-checkbox");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderSimpleCheckboxWithLight()
        {
            //Act
            var cut = RenderComponent<MSimpleCheckbox>(props =>
            {
                props.Add(simplecheckbox => simplecheckbox.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("m-simple-checkbox");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderSimpleCheckboxWithDisabled()
        {
            //Act
            var cut = RenderComponent<MSimpleCheckbox>(props =>
            {
                props.Add(simplecheckbox => simplecheckbox.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-simple-checkbox--disabled");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderSimpleCheckboxWithIndeterminate()
        {
            //Act
            var cut = RenderComponent<MSimpleCheckbox>(props =>
            {
                props.Add(simplecheckbox => simplecheckbox.Indeterminate, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIndeterminateClass = classes.Contains("m-simple-checkbox");

            // Assert
            Assert.IsTrue(hasIndeterminateClass);
        }

        [TestMethod]
        public void RenderSimpleCheckboxWithRipple()
        {
            //Act
            var cut = RenderComponent<MSimpleCheckbox>(props =>
            {
                props.Add(simplecheckbox => simplecheckbox.Ripple, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRippleClass = classes.Contains("m-simple-checkbox");

            // Assert
            Assert.IsTrue(hasRippleClass);
        }

        [TestMethod]
        public void RenderSimpleCheckboxWithValue()
        {
            //Act
            var cut = RenderComponent<MSimpleCheckbox>(props =>
            {
                props.Add(simplecheckbox => simplecheckbox.Value, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-simple-checkbox");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderSimpleCheckboxWithColor()
        {
            //Act
            var cut = RenderComponent<MSimpleCheckbox>(props =>
            {
                string icon = "mdi-star";
                props.Add(simplecheckbox => simplecheckbox.Color, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColorClass = classes.Contains("m-simple-checkbox");

            // Assert
            Assert.IsTrue(hasColorClass);
        }

        [TestMethod]
        public void RenderSimpleCheckboxWithIndeterminateIcon()
        {
            //Act
            var cut = RenderComponent<MSimpleCheckbox>(props =>
            {
                string icon = "mdi-star";
                props.Add(simplecheckbox => simplecheckbox.IndeterminateIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIndeterminateIconClass = classes.Contains("m-simple-checkbox");

            // Assert
            Assert.IsTrue(hasIndeterminateIconClass);
        }

        [TestMethod]
        public void RenderSimpleCheckboxWithOffIcon()
        {
            //Act
            var cut = RenderComponent<MSimpleCheckbox>(props =>
            {
                string icon = "mdi-star";
                props.Add(simplecheckbox => simplecheckbox.OffIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOffIconClass = classes.Contains("m-simple-checkbox");

            // Assert
            Assert.IsTrue(hasOffIconClass);
        }

        [TestMethod]
        public void RenderSimpleCheckboxWithOnIcon()
        {
            //Act
            var cut = RenderComponent<MSimpleCheckbox>(props =>
            {
                string icon = "mdi-star";
                props.Add(simplecheckbox => simplecheckbox.OnIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOnIconClass = classes.Contains("m-simple-checkbox");

            // Assert
            Assert.IsTrue(hasOnIconClass);
        }
    }
}
