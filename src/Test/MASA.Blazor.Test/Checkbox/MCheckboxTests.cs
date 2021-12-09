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
        public void RenderCheckboxWithDark()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(checkbox => checkbox.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderCheckboxWithDense()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(checkbox => checkbox.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-input--dense");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderCheckboxWithDisabled()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(breadcrumbs => breadcrumbs.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-input--is-disabled");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderCheckboxWithError()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(checkbox => checkbox.Error, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasErrorClass);
        }

        [TestMethod]
        public void RenderCheckboxWithErrorCount()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(alert => alert.ErrorCount, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorCountClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasErrorCountClass);
        }

        [TestMethod]
        public void RenderCheckboxWithHideDetails()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(checkbox => checkbox.HideDetails, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideDetailsClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasHideDetailsClass);
        }

        [TestMethod]
        public void RenderCheckboxWithIndeterminate()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
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
        public void RenderCheckboxWithPersistentHint()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(checkbox => checkbox.PersistentHint, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIndeterminateClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasIndeterminateClass);
        }

        [TestMethod]
        public void RenderCheckboxWithReadonly()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(checkbox => checkbox.Readonly, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReadonlyClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasReadonlyClass);
        }

        [TestMethod]
        public void RenderCheckboxWithRipple()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(checkbox => checkbox.Ripple, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRippleClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasRippleClass);
        }

        [TestMethod]
        public void RenderCheckboxWithSuccess()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(checkbox => checkbox.Success, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSuccessClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasSuccessClass);
        }

        [TestMethod]
        public void RenderCheckboxWithValidateOnBlur()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(checkbox => checkbox.ValidateOnBlur, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValidateOnBlurClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasValidateOnBlurClass);
        }

        [TestMethod]
        public void RenderCheckboxWithValue()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(checkbox => checkbox.Value, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderCheckboxWithAppendIcon()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                string icon = "mdi-star";
                props.Add(checkbox => checkbox.AppendIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppendIconClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasAppendIconClass);
        }

        [TestMethod]
        public void RenderCheckboxWithBackgroundColor()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                string icon = "mdi-star";
                props.Add(checkbox => checkbox.BackgroundColor, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBackgroundColorClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasBackgroundColorClass);
        }

        [TestMethod]
        public void RenderCheckboxWithColor()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                string icon = "mdi-star";
                props.Add(checkbox => checkbox.Color, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColorClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasColorClass);
        }

        [TestMethod]
        public void RenderCheckboxWithHint()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                string icon = "mdi-star";
                props.Add(checkbox => checkbox.Hint, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHintClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasHintClass);
        }

        [TestMethod]
        public void RenderCheckboxWithId()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                string icon = "mdi-star";
                props.Add(checkbox => checkbox.Id, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIdClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasIdClass);
        }

        [TestMethod]
        public void RenderCheckboxWithIndeterminateIcon()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                string icon = "mdi-star";
                props.Add(checkbox => checkbox.IndeterminateIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIndeterminateIconClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasIndeterminateIconClass);
        }

        [TestMethod]
        public void RenderCheckboxWithLabel()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                string icon = "mdi-star";
                props.Add(checkbox => checkbox.Label, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLabelClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasLabelClass);
        }

        [TestMethod]
        public void RenderCheckboxWithLight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(checkbox => checkbox.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderCheckboxWithOffIcon()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                string icon = "mdi-star";
                props.Add(checkbox => checkbox.OffIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOffIconClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasOffIconClass);
        }

        [TestMethod]
        public void RenderCheckboxWithOnIcon()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                string icon = "mdi-star";
                props.Add(checkbox => checkbox.OnIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOnIconClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasOnIconClass);
        }

        [TestMethod]
        public void RenderCheckboxWithPrependIcon()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                string icon = "mdi-star";
                props.Add(checkbox => checkbox.PrependIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPrependIconClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasPrependIconClass);
        }

        [TestMethod]
        public void RenderWithLabelContent()
        {
            // Arrange & Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>(props =>
            {
                props.Add(checkbox => checkbox.LabelContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-label");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void RenderNormal()
        {
            // Arrange & Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MCheckbox>();
            var inputDiv = cut.Find("div");

            // Assert
            Assert.AreEqual(6, inputDiv.ClassList.Length);
            Assert.IsTrue(inputDiv.ClassList.Contains("m-input"));
            Assert.IsTrue(inputDiv.ClassList.Contains("theme--light"));
        }
    }
}
