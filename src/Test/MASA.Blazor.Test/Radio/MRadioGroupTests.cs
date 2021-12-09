using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Bunit;

namespace MASA.Blazor.Test.Radio
{
    [TestClass]
    public class MRadioGroupTests:TestBase
    {
        [TestMethod]
        public void RenderRadioGroupWithDense()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithRow()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.Row, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRowClass = classes.Contains("m-input--radio-group--row");
            // Assert
            Assert.IsTrue(hasRowClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithColumn()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.Column, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasColumnClass = classes.Contains("m-input--radio-group--column");
            // Assert
            Assert.IsTrue(hasColumnClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithDark()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithLight()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithDisabled()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithError()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.Error, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasErrorClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithErrorCount()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(alert => alert.ErrorCount, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorCountClass = classes.Contains("m-input--radio-group");

            // Assert
            Assert.IsTrue(hasErrorCountClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithHideDetails()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.HideDetails, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideDetailsClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasHideDetailsClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithMandatory()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.Mandatory, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMandatoryClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasMandatoryClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithPersistentHint()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.PersistentHint, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPersistentHintClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasPersistentHintClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithReadonly()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.Readonly, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReadonlyClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasReadonlyClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithSuccess()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.Success, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSuccessClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasSuccessClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithValidateOnBlur()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                props.Add(radiogroup => radiogroup.ValidateOnBlur, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValidateOnBlurClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasValidateOnBlurClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithAppendIcon()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                string icon = "mdi-star";
                props.Add(radiogroup => radiogroup.AppendIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAppendIconClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasAppendIconClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithBackgroundColor()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                string icon = "mdi-star";
                props.Add(radiogroup => radiogroup.BackgroundColor, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasBackgroundColorClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasBackgroundColorClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithHint()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                string icon = "mdi-star";
                props.Add(radiogroup => radiogroup.Hint, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHintClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasHintClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithId()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                string icon = "mdi-star";
                props.Add(radiogroup => radiogroup.Id, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasIdClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasIdClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithLabel()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                string icon = "mdi-star";
                props.Add(radiogroup => radiogroup.Label, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLabelClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasLabelClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithPrependIcon()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                string icon = "mdi-star";
                props.Add(radiogroup => radiogroup.PrependIcon, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPrependIconClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasPrependIconClass);
        }

        [TestMethod]
        public void RenderRadioGroupWithValue()
        {
            //Act
            var cut = RenderComponent<MRadioGroup<string>>(props =>
            {
                string icon = "mdi-star";
                props.Add(radiogroup => radiogroup.Value, icon);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValueClass = classes.Contains("m-input--radio-group");
            // Assert
            Assert.IsTrue(hasValueClass);
        }
    }
}
