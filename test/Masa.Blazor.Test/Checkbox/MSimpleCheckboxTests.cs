using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.Checkbox
{
    [TestClass]
    public class MSimpleCheckboxTests : TestBase
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
    }
}
