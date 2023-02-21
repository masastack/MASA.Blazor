using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Masa.Blazor.Test.FileInput
{
    [TestClass]
    public class MFileInputTests : TestBase
    {
        [TestMethod]
        public void RenderFileInputWithAutofocus()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Autofocus, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasAutofocusClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasAutofocusClass);
        }

        [TestMethod]
        public void RenderFileInputWithChips()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Chips, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasChipsClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasChipsClass);
        }

        [TestMethod]
        public void RenderFileInputWithClearable()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Clearable, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasClearableClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasClearableClass);
        }

        [TestMethod]
        public void RenderFileInputWithCounter()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Counter, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasCounterClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasCounterClass);
        }

        [TestMethod]
        public void RenderFileInputWithDark()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Dark, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderFileInputWithDense()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Dense, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDenseClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderFileInputWithDisabled()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Disabled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasDisabledClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderFileInputWithError()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Error, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasErrorClass);
        }

        [TestMethod]
        public void RenderFileInputWithErrorCount()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.ErrorCount, 1);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasErrorCountClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasErrorCountClass);
        }

        [TestMethod]
        public void RenderFileInputWithFilled()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Filled, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFilledClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasFilledClass);
        }

        [TestMethod]
        public void RenderFileInputWithFlat()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Flat, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFlatClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasFlatClass);
        }

        [TestMethod]
        public void RenderFileInputWithFullWidth()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.FullWidth, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasFullWidthClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasFullWidthClass);
        }

        [TestMethod]
        public void RenderFileInputWithHeight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Height, 100);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHeightClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasHeightClass);
        }

        [TestMethod]
        public void RenderFileInputWithHideDetails()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.HideDetails, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideDetailsClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasHideDetailsClass);
        }

        [TestMethod]
        public void RenderFileInputWithHideInput()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.HideInput, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasHideInputClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasHideInputClass);
        }

        [TestMethod]
        public void RenderFileInputWithLight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Light, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderFileInputWithLoaderHeight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.LoaderHeight, 2);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoaderHeightClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasLoaderHeightClass);
        }

        [TestMethod]
        public void RenderFileInputWithLoading()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Loading, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasLoadingClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasLoadingClass);
        }

        [TestMethod]
        public void RenderFileInputWithMultiple()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<List<IBrowserFile>>>(props =>
            {
                props.Add(fileinput => fileinput.Multiple, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasMultipleClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasMultipleClass);
        }

        [TestMethod]
        public void RenderFileInputWithOutlined()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Outlined, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasOutlinedClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasOutlinedClass);
        }

        [TestMethod]
        public void RenderFileInputWithPersistentHint()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.PersistentHint, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPersistentHintClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasPersistentHintClass);
        }

        [TestMethod]
        public void RenderFileInputWithPersistentPlaceholder()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.PersistentPlaceholder, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasPersistentPlaceholderClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasPersistentPlaceholderClass);
        }

        [TestMethod]
        public void RenderFileInputWithReverse()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Reverse, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasReverseClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasReverseClass);
        }

        [TestMethod]
        public void RenderFileInputWithRounded()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Rounded, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasRoundedClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderFileInputWithShaped()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Shaped, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShapedClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasShapedClass);
        }

        [TestMethod]
        public void RenderFileInputWithShowSize()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.ShowSize, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasShowSizeClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasShowSizeClass);
        }

        [TestMethod]
        public void RenderFileInputWithSingleLine()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.SingleLine, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSingleLineClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasSingleLineClass);
        }

        [TestMethod]
        public void RenderFileInputWithSmallChips()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.SmallChips, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSmallChipsClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasSmallChipsClass);
        }

        [TestMethod]
        public void RenderFileInputWithSolo()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Solo, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSoloClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasSoloClass);
        }

        [TestMethod]
        public void RenderFileInputWithSoloInverted()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.SoloInverted, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSoloInvertedClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasSoloInvertedClass);
        }

        [TestMethod]
        public void RenderFileInputWithSuccess()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.Success, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasSuccessClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasSuccessClass);
        }

        [TestMethod]
        public void RenderFileInputWithTruncateLength()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.TruncateLength, 22);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasTruncateLengthClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasTruncateLengthClass);
        }

        [TestMethod]
        public void RenderFileInputWithValidateOnBlur()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MFileInput<IBrowserFile>>(props =>
            {
                props.Add(fileinput => fileinput.ValidateOnBlur, true);
            });
            var classes = cut.Instance.CssProvider.GetClass();
            var hasValidateOnBlurClass = classes.Contains("m-file-input");

            // Assert
            Assert.IsTrue(hasValidateOnBlurClass);
        }
    }
}
