using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Masa.Blazor.Test.TextField
{
    [TestClass]
    public class MTextFieldTests : TestBase
    {
        [TestMethod]
        public void RenderNormal()
        {
            // Act
            var cut = RenderComponent<MTextField<string>>();
            var inputDiv = cut.Find("div");

            // Assert
            Assert.AreEqual(4, inputDiv.ClassList.Length);
            Assert.IsTrue(inputDiv.ClassList.Contains("m-input"));
            Assert.IsTrue(inputDiv.ClassList.Contains("theme--light"));
            Assert.IsTrue(inputDiv.ClassList.Contains("m-text-field"));
            Assert.IsTrue(inputDiv.ClassList.Contains("m-text-field--is-booted"));
            Assert.IsTrue(inputDiv.ClassList.Contains("m-input"));

        }

        [DataTestMethod]
        [DataRow("red")]
        [DataRow("blue")]
        public void RenderWithColorAndFocus(string color)
        {
            // Arrange
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(r => r.Color, color);
            });
            var inputDiv = cut.Find("div");
            var inputElement = cut.Find("input");

            // Act
            inputElement.Focus();

            // Assert
            Assert.AreEqual(6, inputDiv.ClassList.Length);
            Assert.IsTrue(inputDiv.ClassList.Contains(color + "--text"));
        }

        [TestMethod]
        public void RenderTextFieldWithAutofocus()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Autofocus, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAutofocusClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasAutofocusClass);
        }

        [TestMethod]
        public void RenderTextFieldWithClearable()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Clearable, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClearableClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasClearableClass);
        }

        [TestMethod]
        public void RenderTextFieldWithCounter()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Counter, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCounterClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasCounterClass);
        }

        [TestMethod]
        public void RenderTextFieldWithDark()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderTextFieldWithDense()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderTextFieldWithDisabled()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderTextFieldWithError()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Error, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorClass = classes.Contains("m-text-field");
            // Assert
            Assert.IsTrue(hasErrorClass);
        }

        [TestMethod]
        public void RenderTextFieldWithErrorCount()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.ErrorCount, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorCountClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasErrorCountClass);
        }

        [TestMethod]
        public void RenderTextFieldWithFilled()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Filled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFilledClass = classes.Contains("m-text-field");
            // Assert
            Assert.IsTrue(hasFilledClass);
        }

        [TestMethod]
        public void RenderTextFieldWithFlat()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-text-field--solo-flat");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderTextFieldWithFullWidth()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.FullWidth, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFullWidthClass = classes.Contains("m-text-field--full-width");

            // Assert
            Assert.IsTrue(hasFullWidthClass);
        }

        [TestMethod]
        public void RenderTextFieldWithHeight()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Height, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoaderHeightClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasLoaderHeightClass);
        }

        [TestMethod]
        public void RenderTextFieldWithHideDetails()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.HideDetails, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideDetailsClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasHideDetailsClass);
        }

        [TestMethod]
        public void RenderTextFieldWithLight()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderTextFieldWithLoaderHeight()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.LoaderHeight, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoaderHeightClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasLoaderHeightClass);
        }

        [TestMethod]
        public void RenderTextFieldWithLoading()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Loading, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoadingClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasLoadingClass);
        }

        [TestMethod]
        public void RenderTextFieldWithOutlined()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderTextFieldWithPersistentHint()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.PersistentHint, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPersistentHintClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasPersistentHintClass);
        }

        [TestMethod]
        public void RenderTextFieldWithReadonly()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Readonly, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReadonlyClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasReadonlyClass);
        }

        [TestMethod]
        public void RenderTextFieldWithReverse()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Reverse, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReverseClass = classes.Contains("m-text-field--reverse");

            // Assert
            Assert.IsTrue(hasReverseClass);
        }

        [TestMethod]
        public void RenderTextFieldWithRounded()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-text-field--rounded");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderTextFieldWithShaped()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-text-field--shaped");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderTextFieldWithSingleLine()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.SingleLine, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleLineClass = classes.Contains("m-text-field--single-line");

            // Assert
            Assert.IsTrue(hasSingleLineClass);
        }

        [TestMethod]
        public void RenderTextFieldWithSolo()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Solo, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSoloClass = classes.Contains("m-text-field--solo");

            // Assert
            Assert.IsTrue(hasSoloClass);
        }

        [TestMethod]
        public void RenderTextFieldWithSoloInverted()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.SoloInverted, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSoloInvertedClass = classes.Contains("m-text-field--solo-inverted");

            // Assert
            Assert.IsTrue(hasSoloInvertedClass);
        }

        [TestMethod]
        public void RenderTextFieldWithSuccess()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.Success, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSuccessClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasSuccessClass);
        }

        [TestMethod]
        public void RenderTextFieldWithValidateOnBlur()
        {
            //Act
            var cut = RenderComponent<MTextField<string>>(props =>
            {
                props.Add(textfield => textfield.ValidateOnBlur, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValidateOnBlurClass = classes.Contains("m-text-field");

            // Assert
            Assert.IsTrue(hasValidateOnBlurClass);
        }
    }
}
